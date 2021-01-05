using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.common;
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



        #region 语种
        public async Task<List<LanguageTypeDTO>> GetLanguageTypeList(LanguageTypeDTO dto)
        {
            var list = a35MgeDbContext.LanguageType.OrderBy(x => x.Sort).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(dto.Code))
                list = list.Where(x => x.LanguageCode.Contains(dto.Code));
            if (!string.IsNullOrWhiteSpace(dto.Description))
                list = list.Where(x => x.Description.Contains(dto.Description));
            return await list.Select(x => mapper.Map<LanguageTypeDTO>(x))
                .ToListAsync();
        }
        public async Task AddLanguageType(LanguageTypeDTO language)
        {
            var entity = mapper.Map<LanguageType>(language);
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task UpdateLanguageType(LanguageTypeDTO language)
        {
            var data = await a35MgeDbContext.LanguageType.FirstOrDefaultAsync(x => x.LanguageTypeId == language.Id);
            if (data == null)
                throw new Exception("修改的语种信息不存在!");
            var entity = mapper.Map(language, data);
            entity.ModifyDate = DateTime.Now;
            a35MgeDbContext.Update(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task DeleteLanType(int Id)
        {
            var data = await a35MgeDbContext.LanguageType.FirstOrDefaultAsync(x => x.LanguageTypeId == Id);
            if (data == null)
                throw new Exception("要删除的语种信息不存在!");
            a35MgeDbContext.Remove(data);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task<LanguageTypeDTO> Get(int Id)
        {
            var data = mapper.Map<LanguageTypeDTO>(await a35MgeDbContext.LanguageType.FirstOrDefaultAsync(x => x.LanguageTypeId == Id));
            return data;
        }
        #endregion

        #region 翻译内容

        public async Task<PagedViewModel> GetAllTranslate(TranslateRequest request)
        {
            var model = new PagedViewModel();
            var data = a35MgeDbContext.Translate.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Code))
                data = data.Where(x => x.TranslateCode.Contains(request.Code));
            if (!string.IsNullOrWhiteSpace(request.Value))
                data = data.Where(x => x.TranslateContent.Contains(request.Value));
            model.totalElements = data.Count();
            data = data.Skip((request.page - 1) * request.pageSize).Take(request.pageSize);
            model.Data = await data.Select(x => mapper.Map<LanDTO>(x)).ToListAsync();
            return model;
        }
        public async Task<List<LanDTO>> GetTranslateFromLanguage(string code)
        {
            var data = await a35MgeDbContext.LanguageType
                 .Include(x => x.TranslateList)
                 .FirstOrDefaultAsync(x => x.LanguageCode.Equals(code, StringComparison.OrdinalIgnoreCase));
            return mapper.ProjectTo<LanDTO>(data.TranslateList.AsQueryable()).ToList();
        }
        public async Task AddTranslate(LanDTO translate)
        {
            var entity = mapper.Map<Translate>(translate);
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task UpdateTranslate(LanDTO translate)
        {
            var data = await a35MgeDbContext.Translate
                 .FirstOrDefaultAsync(x => x.TranslateId == translate.TranslateId && x.TranslateCode == translate.TranslateCode);
            var entity = mapper.Map(translate, data);
            entity.TranslateContent = translate.TranslateContent;
            entity.ModifyDate = DateTime.Now;
            a35MgeDbContext.Update(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task DeleteTanslate(int[] Ids)
        {
            var data = await a35MgeDbContext.Translate.Where(x => Ids.Contains(x.TranslateId)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<LanDTO> GetTranslateFromId(int Id)
        {
            var Data = await a35MgeDbContext.Translate.FirstOrDefaultAsync(x => x.TranslateId == Id);
            return mapper.Map<LanDTO>(Data);
        }
        #endregion
    }
}
