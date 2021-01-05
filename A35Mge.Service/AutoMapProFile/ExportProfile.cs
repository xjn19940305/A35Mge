using A35Mge.Database.Entities;
using A35Mge.Model.ExportModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Service.AutoMapProFile
{
    public class ExportProfile : Profile
    {
        public ExportProfile()
        {
            CreateMap<MenuExport, Menu>().ReverseMap();
            CreateMap<LanExport, LanguageType>().ReverseMap();
            CreateMap<TranslateExport, Translate>().ReverseMap();
        }
    }
}
