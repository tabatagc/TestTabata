using AutoMapper;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities.Document;
using System.Reflection.Metadata;

namespace CallCenterAgentManager.Application.AutoMapper.Mappings
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<Domain.Entities.Relational.Agent, AgentResponse>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CurrentState, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastActivityTimestampUtc, opt => opt.MapFrom(src => src.LastActivityTimestampUtc));

            CreateMap<Agent, AgentResponse>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CurrentState, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastActivityTimestampUtc, opt => opt.MapFrom(src => src.LastActivityTimestampUtc));

            CreateMap<Domain.Entities.Relational.Event, EventResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc))
                .ForMember(dest => dest.QueueIds, opt => opt.MapFrom(src => src.QueueIds));

            CreateMap<Event, EventResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc))
                .ForMember(dest => dest.QueueIds, opt => opt.MapFrom(src => src.QueueIds));

            CreateMap<Domain.Entities.Relational.Queue, QueueResponse>()
                .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));

            CreateMap<Queue, QueueResponse>()
                .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));
        }
    }
}
