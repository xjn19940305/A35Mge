using A35Mge.Database;
using A35Mge.Database.Business;
using A35Mge.Model.common;
using A35Mge.Model.CommonList;
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
    public class DicTypeService : IDicTypeService
    {
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;

        public DicTypeService(
             A35MgeDbContext a35MgeDbContext,
             IMapper mapper
            )
        {
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
        }
        public async Task Add(DicTypeDTO dto)
        {
            var entity = mapper.Map<DicType>(dto);
            entity.CreateDate = DateTime.UtcNow;
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task Update(DicTypeDTO dto)
        {
            var entity = await a35MgeDbContext.dicType.FirstOrDefaultAsync(x => x.Id == dto.Id);
            var data = mapper.Map(dto, entity);
            data.ModifyDate = DateTime.UtcNow;
            a35MgeDbContext.Update(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task Delete(int[] ids)
        {
            var list = new List<DicType>();
            var data = await a35MgeDbContext.dicType.Where(x => ids.Contains(x.Id)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<DicTypeResponseDTO> Get(int Id)
        {
            var data = mapper.Map<DicTypeResponseDTO>(await a35MgeDbContext.dicType.FirstOrDefaultAsync(x => x.Id == Id));
            return data;
        }

        public async Task<PagedViewModel> GetList(DicTypeRequest request)
        {
            var list = a35MgeDbContext.dicType
                    .OrderByDescending(x => x.CreateDate)
                    .AsNoTracking();
            var model = new PagedViewModel
            {
                totalElements = list.Count()
            };
            if (!string.IsNullOrWhiteSpace(request.Name))
                list = list.Where(x => x.Name.Contains(request.Name));
            if (!string.IsNullOrWhiteSpace(request.Type))
                list = list.Where(x => x.Type.Contains(request.Type));
            var dt = await list
                    .Skip((request.page - 1) * request.pageSize).Take(request.pageSize)
                    //.Include(x => x.Category)
                    .Select(x => mapper.Map<DicTypeResponseDTO>(x))
                    .ToListAsync();
            model.Data = dt;
            return model;
        }

    }
}
