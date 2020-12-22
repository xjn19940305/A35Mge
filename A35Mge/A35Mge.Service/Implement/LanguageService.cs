using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.LanguageDTO;
using A35Mge.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Implement
{
    public class LanguageService : ILanguageService
    {
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;

        public LanguageService(
             A35MgeDbContext a35MgeDbContext,
             IMapper mapper
            )
        {
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
        }
        public async Task AddLanguageType(LanguageType language)
        {
            await a35MgeDbContext.AddAsync(language);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task AddTranslate(Translate translate)
        {
            await a35MgeDbContext.AddAsync(translate);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<List<LanguageTypeDTO>> GetLanguageTypeList()
        {
            return await a35MgeDbContext.LanguageType.Select(x => mapper.Map<LanguageTypeDTO>(x))
               .ToListAsync();
        }

        public async Task<List<LanDTO>> GetTranslateFromLanguage(string code)
        {
            var data = await a35MgeDbContext.LanguageType
                 .Include(x => x.TranslateList)
                 .FirstOrDefaultAsync(x => x.LanguageCode.Equals(code, StringComparison.OrdinalIgnoreCase));
            return mapper.ProjectTo<LanDTO>(data.TranslateList.AsQueryable()).ToList();
        }

        public async Task UpdateTranslate(Translate translate)
        {
            var data = await a35MgeDbContext.Translate
                 .FirstOrDefaultAsync(x => x.TranslateId == translate.TranslateId && x.TranslateCode == translate.TranslateCode);
            data.TranslateContent = translate.TranslateContent;
            data.ModifyDate = DateTime.Now;
            await a35MgeDbContext.SaveChangesAsync();
        }
    }
}
