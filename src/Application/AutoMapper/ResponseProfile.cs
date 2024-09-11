using AutoMapper;
using CallCenterAgentManager.Domain.DTO.Response;

namespace CallCenterAgentManager.Application.AutoMapper.Mappings
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<Domain.Entities.Relational.Agent, AgentResponse>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CurrentState, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastActivityTimestampUtc, opt => opt.MapFrom(src => src.LastActivityTimestampUtc));

            CreateMap<Domain.Entities.Document.Agent, AgentResponse>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CurrentState, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.LastActivityTimestampUtc, opt => opt.MapFrom(src => src.LastActivityTimestampUtc));

            CreateMap<Domain.Entities.Relational.Event, EventResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc));

            CreateMap<Domain.Entities.Document.Event, EventResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc));

            CreateMap<Domain.Entities.Relational.Queue, QueueResponse>()
                .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));

            CreateMap<Domain.Entities.Document.Queue, QueueResponse>()
                .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));
        }
    }
}
