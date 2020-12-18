using A35Mge.Database.Entities;
using A35Mge.Model;
using A35Mge.Model.LanguageDTO;
using A35Mge.Model.Permission;
using AutoMapper;

namespace A35Mge.Api.AtMap
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            //双向映射
            CreateMap<JobSchedule, JobScheduleDTO>().ReverseMap();
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
