using A35Mge.Database.Entities;
using A35Mge.Model.LanguageDTO;
using A35Mge.Model.Permission;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api.AtMap
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            //双向映射
            CreateMap<Translate, LanDTO>().ReverseMap();
            CreateMap<Menu, MetaModel>()
            .ForMember(x => x.title, b => b.MapFrom(x => x.name))
            .ReverseMap();
            CreateMap<Menu, MenuDTO>()
            .ForMember(x => x.id, b => b.MapFrom(x => x.MenuId))
            .ReverseMap();
        }
    }
}
