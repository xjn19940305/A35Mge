using A35Mge.Database.Business;
using A35Mge.Model.CommonList;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Service.AutoMapProFile
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<DicTypeDTO, DicType>().ReverseMap();
            CreateMap<DicTypeResponseDTO, DicType>().ReverseMap();
            CreateMap<DicListDTO, DicList>().ReverseMap();
            CreateMap<DicListResponseDTO, DicList>().ReverseMap();
        }
    }
}
