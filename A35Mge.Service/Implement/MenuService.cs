using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.Permission;
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
    public class MenuService : IMenuService
    {
        private readonly A35MgeDbContext a35Mgedbcontext;
        private readonly IMapper mapper;

        public MenuService(
            A35MgeDbContext a35Mgedbcontext,
            IMapper mapper
            )
        {
            this.a35Mgedbcontext = a35Mgedbcontext;
            this.mapper = mapper;
        }
        public async Task Add(MenuRequestDTO menu)
        {
            var entity = mapper.Map<Menu>(menu);
            entity.MenuId = Guid.NewGuid().ToString();
            entity.keepAlive = true;
            entity.Target = string.Empty;
            await a35Mgedbcontext.AddAsync(entity);
            await a35Mgedbcontext.SaveChangesAsync();
        }

        public async Task Delete(string menuId)
        {
            var data = await a35Mgedbcontext.Menu.FirstOrDefaultAsync(x => x.MenuId == menuId);
            a35Mgedbcontext.Remove(data);
            await a35Mgedbcontext.SaveChangesAsync();
        }

        public async Task<MenuRequestDTO> Get(string MenuId)
        {
            var data = mapper.Map<MenuRequestDTO>(await a35Mgedbcontext.Menu.FirstOrDefaultAsync(x => x.MenuId == MenuId));
            return data;
        }
        public async Task<List<MenuDTO>> GetAuthMenuList(string[] MenuIds)
        {
            var list = await a35Mgedbcontext.Menu
                .Where(x => MenuIds.Contains(x.MenuId))
                .OrderBy(x => x.Sort).ToListAsync();
            var menuList = new List<MenuDTO>();
            foreach (var item in list)
            {
                var entity = mapper.Map<Menu, MenuDTO>(item);
                var entityMeta = mapper.Map<Menu, MetaModel>(item);
                entity.meta = entityMeta;
                menuList.Add(entity);
            }
            return menuList;
        }
        public async Task<List<MenuDTO>> GetMenuList()
        {
            var list = await a35Mgedbcontext.Menu.OrderBy(x => x.Sort).ToListAsync();
            var menuList = new List<MenuDTO>();
            foreach (var item in list)
            {
                var entity = mapper.Map<Menu, MenuDTO>(item);
                var entityMeta = mapper.Map<Menu, MetaModel>(item);
                entity.meta = entityMeta;
                menuList.Add(entity);
            }
            return menuList;
        }

        public async Task Update(MenuRequestDTO menu)
        {
            var data = await a35Mgedbcontext.Menu.FirstOrDefaultAsync(x => x.MenuId == menu.MenuId);
            if (data == null)
                throw new Exception("修改的菜单不存在!");
            var entity = mapper.Map(menu, data);
            entity.ModifyDate = DateTime.Now;
            a35Mgedbcontext.Update(entity);
            await a35Mgedbcontext.SaveChangesAsync();
        }
    }
}
