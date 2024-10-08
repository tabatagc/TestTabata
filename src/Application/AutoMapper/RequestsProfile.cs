﻿using AutoMapper;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.AutoMapper.Mappings
{
    public class RequestsProfile : Profile
    {
        public RequestsProfile()
        {
            CreateMap<AgentCreateRequest, AgentBase<Guid>>()
                .Include<AgentCreateRequest, Domain.Entities.Relational.Agent>();

            CreateMap<AgentCreateRequest, AgentBase<string>>()
                .Include<AgentCreateRequest, Domain.Entities.Document.Agent>();

            CreateMap<AgentCreateRequest, Domain.Entities.Relational.Agent>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => "AVAILABLE"))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AgentCreateRequest, Domain.Entities.Document.Agent>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => "AVAILABLE"))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AgentUpdateRequest, Domain.Entities.Relational.Agent>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<AgentUpdateRequest, Domain.Entities.Document.Agent>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CallCenterEventRequest, Domain.Entities.Relational.Event>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc));

            CreateMap<CallCenterEventRequest, Domain.Entities.Document.Event>()
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.TimestampUtc, opt => opt.MapFrom(src => src.TimestampUtc));

            CreateMap<QueueCreateRequest, Domain.Entities.Relational.Queue>()
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));

            CreateMap<QueueCreateRequest, Domain.Entities.Document.Queue>()
                .ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName));
        }
    }
}
