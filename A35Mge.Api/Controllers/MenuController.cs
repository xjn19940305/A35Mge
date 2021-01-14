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
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "manager")]
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
        /// 根据权限获取所有的菜单列表包含按钮
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuDTO>> GetAuthMenuList([FromBody] string[] menuIds)
        {
            var menuList = await menuService.GetAuthMenuList(menuIds);
            return menuList;
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="DTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMenu([FromBody] MenuRequestDTO DTO)
        {
            await menuService.Add(DTO);
            return Ok();
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="DTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MenuRequestDTO DTO)
        {
            await menuService.Update(DTO);
            return Ok();
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string Id)
        {
            await menuService.Delete(Id);
            return Ok();
        }
        /// <summary>
        /// 获取某个菜单
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string MenuId)
        {
            var res = await menuService.Get(MenuId);
            return Ok(res);
        }

    }
}
