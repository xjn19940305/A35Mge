using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.Permission;
using A35Mge.Service.Interface;
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
        private readonly IMenuService menuService;

        public MenuController(
             IMenuService menuService
            )
        {
            this.menuService = menuService;
        }
        /// <summary>
        /// 获取所有的菜单列表包含按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MenuDTO>> GetMenuList()
        {
            var menuList = await menuService.GetMenuList();
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
            await menuService.Add(entity);
            return Ok("添加成功");
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Menu entity)
        {
            await menuService.Update(entity);
            return Ok("修改成功");
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       [HttpGet]
        public async Task<IActionResult> Delete (string Id)
        {
            await menuService.Delete(Id);
            return Ok("删除成功");
        }
    }
}
