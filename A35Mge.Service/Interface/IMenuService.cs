using A35Mge.Database.Entities;
using A35Mge.Model.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface IMenuService
    {
        /// <summary>
        /// 获取所有的菜单列表包含按钮
        /// </summary>
        /// <returns></returns>
        public Task<List<MenuDTO>> GetMenuList();
        /// <summary>
        /// 根据权限获得菜单
        /// </summary>
        /// <returns></returns>
        public Task<List<MenuDTO>> GetAuthMenuList(string[] MenuIds);
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Task Add(MenuRequestDTO menu);
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Task Update(MenuRequestDTO menu);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Task Delete(string menuId);
        /// <summary>
        /// 根据菜单ID获取详细的菜单信息
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        public Task<MenuRequestDTO> Get(string MenuId);
    }
}
