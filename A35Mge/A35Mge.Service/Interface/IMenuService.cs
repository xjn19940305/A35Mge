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
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Task Add(Menu menu);
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Task Update(Menu menu);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Task Delete(string menuId);
    }
}
