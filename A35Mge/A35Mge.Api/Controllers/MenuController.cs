using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.Permission;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;

        public MenuController(
             A35MgeDbContext a35MgeDbContext,
             IMapper mapper
            )
        {
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取所有的菜单列表包含按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MenuDTO>> GetMenuList()
        {
            var list = await a35MgeDbContext.Menu.OrderBy(x => x.Sort).ToListAsync();
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
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMenu(Menu entity)
        {
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
            return Ok("添加成功");
        }
    }
}
