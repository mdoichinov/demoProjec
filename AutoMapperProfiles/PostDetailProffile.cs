using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoProject.AutoMapperProfiles
{
    public class PostDetailProffile : Profile
    {
        public PostDetailProffile()
        {
            CreateMap<Models.Post, DtoModels.PostDetailDto>()
                 .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                 .ForMember(dest => dest.postTitle, opt => opt.MapFrom(src => src.title))
                 .ForMember(dest => dest.PostBody, opt => opt.MapFrom(src => src.body));
        }
    }
}
