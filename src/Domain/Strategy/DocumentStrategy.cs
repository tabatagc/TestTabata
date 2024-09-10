using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Entities.Document;
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Repository.Document;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.Domain.Strategy
{
    public class DocumentStrategy<TEntity, TId> : BaseStrategy<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly IRepositoryBase<TEntity, TId> _repository;
        private readonly IQueueRepository _queueRepository;

        public DocumentStrategy(IRepositoryFactory repositoryFactory, IQueueRepository queueRepository)
              : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetRepository<TEntity, TId>();
            _queueRepository = queueRepository;
        }

        public override TEntity GetById(TId id)
        {
            return _repository.GetById(id);
        }

        public override bool UpdateAgentState(Guid agentId, UpdateAgentStateRequest request)
        {
            var stringId = agentId.ToString();
            var agentRepository = _repositoryFactory.GetRepository<Agent, string>();
            var agent = agentRepository.GetById(stringId);

            if (agent == null)
                throw new Exception("Agent not found.");

            ApplyBusinessRules(agent, request);
            agentRepository.Update(agent);

            return true;
        }

        public override EventResponse ProcessEvent(CallCenterEventRequest request)
        {
            var agentRepository = _repositoryFactory.GetRepository<Agent, string>();
            var eventRepository = _repositoryFactory.GetRepository<Event, string>();

            var agent = agentRepository.GetById(request.AgentId.ToString());
            if (agent == null)
                throw new Exception("Agent not found.");

            ApplyBusinessRules(agent, new UpdateAgentStateRequest { Action = request.Action, TimestampUtc = request.TimestampUtc });
            agentRepository.Update(agent);

            var newEvent = new Event
            {
                AgentId = request.AgentId.ToString(),
                Action = request.Action,
                TimestampUtc = request.TimestampUtc,
                QueueIds = request.QueueIds.Select(id => id.ToString()).ToList()
            };

            eventRepository.Add(newEvent);

            return new EventResponse
            {
                EventId = Guid.Parse(newEvent.Id),
                AgentId = request.AgentId,
                Action = newEvent.Action,
                TimestampUtc = newEvent.TimestampUtc,
                QueueIds = request.QueueIds
            };
        }

        public override IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
        {
            var stringId = agentId.ToString();
            var queues = _queueRepository.GetQueuesByAgentId(stringId);

            return queues.Select(q => new QueueResponse
            {
                QueueId = Guid.Parse(q.Id),
                QueueName = q.QueueName
            });
        }

        private void ApplyBusinessRules(Agent agent, UpdateAgentStateRequest request)
        {
            if (request.Action == "CALL_STARTED")
            {
                agent.State = "ON_CALL";
            }
            else if (request.Action == "START_DO_NOT_DISTURB" &&
                     request.TimestampUtc.TimeOfDay >= TimeSpan.FromHours(11) &&
                     request.TimestampUtc.TimeOfDay <= TimeSpan.FromHours(13))
            {
                agent.State = "ON_LUNCH";
            }

            agent.LastActivityTimestampUtc = request.TimestampUtc;
        }
    }
}