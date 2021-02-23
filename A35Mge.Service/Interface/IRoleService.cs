using A35Mge.Model.common;
using A35Mge.Model.Permission.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 获得所有角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedViewModel> GetAllRole(RoleRequestDTO dto);
        /// <summary>
        /// 根据ID获得单个角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RoleDTO> Get(int Id);
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Add(RoleDTO dto);
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Update(RoleDTO dto);
        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task Delete(int[] ids);
        /// <summary>
        /// 根据角色ID获得角色绑定的菜单的ID
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Task<string[]> GetMenuIdsFromRoleId(int RoleId);
        /// <summary>
        /// 保存角色授权的菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SaveRoleMenu(RoleMenuDTO request);
    }
}
