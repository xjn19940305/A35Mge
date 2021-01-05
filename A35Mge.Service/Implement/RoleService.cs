using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.common;
using A35Mge.Model.Permission.Role;
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
    public class RoleService : IRoleService
    {
        private readonly IMapper mapper;
        private readonly A35MgeDbContext a35MgeDbContext;

        public RoleService(
            IMapper mapper,
            A35MgeDbContext a35MgeDbContext
            )
        {
            this.mapper = mapper;
            this.a35MgeDbContext = a35MgeDbContext;
        }
        public async Task Add(RoleDTO dto)
        {
            var role = mapper.Map<Role>(dto);
            await a35MgeDbContext.AddAsync(role);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task Delete(int[] ids)
        {
            var list = await a35MgeDbContext.Role.Where(x => ids.Contains(x.Id)).ToListAsync();
            a35MgeDbContext.RemoveRange(list);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<RoleDTO> Get(int Id)
        {
            var data = await a35MgeDbContext.Role.FirstOrDefaultAsync(x => x.Id == Id);
            return mapper.Map<RoleDTO>(data);
        }

        public async Task<PagedViewModel> GetAllRole(RoleRequestDTO dto)
        {
            var model = new PagedViewModel();
            var data = a35MgeDbContext.Role.OrderBy(x => x.Sort).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(dto.RoleName))
                data = data.Where(x => x.RoleName.Contains(dto.RoleName));
            model.totalElements = data.Count();
            data = data.Skip((dto.page - 1) * dto.pageSize).Take(dto.pageSize);
            model.Data = await data.Select(x => mapper.Map<RoleDTO>(x)).ToListAsync();
            return model;
        }

        public async Task<string[]> GetMenuIdsFromRoleId(int RoleId)
        {
            var data = await a35MgeDbContext.RoleMenus.Where(x => x.RoleId == RoleId).Select(x => x.MenuId).ToListAsync();
            return data.ToArray();
        }

        public async Task SaveRoleMenu(RoleMenuDTO request)
        {
            var data = await a35MgeDbContext.RoleMenus.Where(x => request.RoleId == x.RoleId && request.MenuIds.Contains(x.MenuId)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            foreach(var item in request.MenuIds)
            {
                await a35MgeDbContext.RoleMenus.AddAsync(new RoleMenu { MenuId = item, RoleId = request.RoleId });
            }
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task Update(RoleDTO dto)
        {
            var entity = await a35MgeDbContext.Role.FirstOrDefaultAsync(x => x.Id == dto.Id);
            var data = mapper.Map(dto, entity);
            data.ModifyDate = DateTime.Now;
            a35MgeDbContext.Update(data);
            await a35MgeDbContext.SaveChangesAsync();
        }
    }
}
