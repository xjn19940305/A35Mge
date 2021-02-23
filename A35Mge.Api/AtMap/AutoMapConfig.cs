using A35Mge.Database.Entities;
using A35Mge.Model;
using A35Mge.Model.LanguageDTO;
using A35Mge.Model.Permission;
using A35Mge.Model.Permission.Role;
using A35Mge.Model.Permission.User;
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

            CreateMap<Menu, MenuRequestDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<LanguageType, LanguageTypeDTO>()
                .ForMember(x => x.Code, b => b.MapFrom(x => x.LanguageCode))
                .ForMember(x => x.Description, b => b.MapFrom(x => x.Description))
                .ForMember(x => x.Id, b => b.MapFrom(x => x.LanguageTypeId))
                .ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
