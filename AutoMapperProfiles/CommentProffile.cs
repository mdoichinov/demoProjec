using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.AutoMapperProfiles
{
    public class CommentProffile : Profile
    {
        public CommentProffile()
        {
            CreateMap<Models.Comment, DtoModels.CommentDto>()
                 .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                 .ForMember(dest => dest.authorEmail, opt => opt.MapFrom(src => src.email))
                 .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.body));
            CreateMap<DtoModels.CommentForCreationDto, Models.CommentForCreation>()
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.authorEmail))
                .ForMember(dest => dest.body, opt => opt.MapFrom(src => src.comment));
        }
    }
}
