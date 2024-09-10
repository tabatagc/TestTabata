using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Entities.Relational;
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Repository.Relational;
using CallCenterAgentManager.Domain.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

public class RelationalStrategy<TEntity, TId> : BaseStrategy<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    private readonly IRepositoryBase<TEntity, TId> _repository;
    private readonly IQueueRepository _queueRepository;

    public RelationalStrategy(IRepositoryFactory repositoryFactory, IQueueRepository queueRepository)
        : base(repositoryFactory)
    {
        _repository = repositoryFactory.GetRepository<TEntity, TId>();
        _queueRepository = queueRepository;
    }

    public override TEntity GetById(TId id)
    {
        if (id is Guid guidId)
        {
            return _repository.GetById(guidId);
        }
        throw new InvalidOperationException("ID should be a Guid for RelationalStrategy.");
    }

    public override bool UpdateAgentState(TId agentId, UpdateAgentStateRequest request)
    {
        var agentRepository = _repositoryFactory.GetRepository<Agent, Guid>();
        var agent = agentRepository.GetById((Guid)(object)agentId);

        if (agent == null)
            throw new Exception("Agent not found.");

        ApplyBusinessRules(agent, request);
        agentRepository.Update(agent);

        return true;
    }

    public override EventResponse ProcessEvent(CallCenterEventRequest request)
    {
        var agentRepository = _repositoryFactory.GetRepository<Agent, Guid>();
        var eventRepository = _repositoryFactory.GetRepository<Event, Guid>();

        var agent = agentRepository.GetById(request.AgentId);
        if (agent == null)
            throw new Exception("Agent not found.");

        ApplyBusinessRules(agent, new UpdateAgentStateRequest { Action = request.Action, TimestampUtc = request.TimestampUtc });
        agentRepository.Update(agent);

        var newEvent = new Event
        {
            AgentId = request.AgentId,
            Action = request.Action,
            TimestampUtc = request.TimestampUtc,
            Queues = new List<Queue>()
        };

        eventRepository.Add(newEvent);

        return new EventResponse
        {
            EventId = newEvent.Id,
            AgentId = request.AgentId,
            Action = newEvent.Action,
            TimestampUtc = newEvent.TimestampUtc,
            QueueIds = request.QueueIds
        };
    }

    public override IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
    {
        var queues = _queueRepository.GetQueuesByAgentId(agentId);

        return queues.Select(q => new QueueResponse
        {
            QueueId = q.Id,
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
