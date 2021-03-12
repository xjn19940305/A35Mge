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
    public class DicListService : IDicListService
    {
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;

        public DicListService(
             A35MgeDbContext a35MgeDbContext,
             IMapper mapper
            )
        {
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
        }

        public async Task Add(DicListDTO dto)
        {
            var entity = mapper.Map<DicList>(dto);
            entity.CreateDate = DateTime.UtcNow;
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task Update(DicListDTO dto)
        {
            var entity = await a35MgeDbContext.dicList.FirstOrDefaultAsync(x => x.Id == dto.Id);
            var data = mapper.Map(dto, entity);
            data.ModifyDate = DateTime.UtcNow;
            a35MgeDbContext.Update(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task Delete(int[] ids)
        {
            var list = new List<DicList>();
            var data = await a35MgeDbContext.dicList.Where(x => ids.Contains(x.Id)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<DicListResponseDTO> Get(int Id)
        {
            var data = mapper.Map<DicListResponseDTO>(await a35MgeDbContext.dicList.FirstOrDefaultAsync(x => x.Id == Id));
            return data;
        }

        public async Task<PagedViewModel> GetList(DicListRequest request)
        {
            var list = a35MgeDbContext.dicList
                          .Where(x => x.DicTypeId == request.DicTypeId)
                          .OrderByDescending(x => x.CreateDate)
                          .AsNoTracking();
            var model = new PagedViewModel
            {
                totalElements = list.Count()
            };
            if (!string.IsNullOrWhiteSpace(request.Name))
                list = list.Where(x => x.Name.Contains(request.Name));
            var dt = await list
                    .Skip((request.page - 1) * request.pageSize).Take(request.pageSize)
                    .Include(x => x.DicType)
                    .Select(x => mapper.Map<DicListResponseDTO>(x))
                    .ToListAsync();
            model.Data = dt;
            return model;
        }

        public async Task<List<DicListResponseDTO>> GetListFromType(string DicName)
        {
            var data = await a35MgeDbContext.dicList.Where(x => x.DicType.Name == DicName)
                .Select(x => mapper.Map<DicListResponseDTO>(x))
                .ToListAsync();
            return data;
        }
    }
}
