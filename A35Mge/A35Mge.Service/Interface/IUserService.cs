using A35Mge.Model.common;
using A35Mge.Model.Permission.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        Task Add(UserDTO dto);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Update(UserDTO dto);
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task Delete(int[] ids);
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedViewModel> GetList(UserRequest request);
        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserDTO> Get(int Id);
        /// <summary>
        /// 根据用户ID获取绑定的角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<int[]> GetRoleIds(int UserId);
        /// <summary>
        /// 保存用户绑定的角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SaveUserRole(UserRoleRequest request);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task ResetPwd(int UserId);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<UserDTO> Login(LoginDTO dto);
    }
}
