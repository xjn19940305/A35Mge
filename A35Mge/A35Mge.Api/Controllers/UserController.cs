using A35Mge.Model.common;
using A35Mge.Model.Permission.User;
using A35Mge.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "manager")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(
            IUserService userService
            )
        {
            this.userService = userService;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "front")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            var data = await userService.Get(Id);
            return Ok(data);
        }
        /// <summary>
        /// 获取分页用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] UserRequest request)
        {
            var data = await userService.GetList(request);
            return Ok(data);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserDTO user)
        {
            await userService.Add(user);
            return Ok();
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            await userService.Update(user);
            return Ok();
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            await userService.Delete(ids);
            return Ok();
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ResetPassword([FromQuery] int UserId)
        {
            await userService.ResetPwd(UserId);
            return Ok();
        }
        /// <summary>
        /// 根据用户ID获得绑定的角色ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoleIdsFromUser([FromQuery] int UserId)
        {
            var data = await userService.GetRoleIds(UserId);
            return Ok(data);
        }
        /// <summary>
        /// 用户绑定角色保存接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> SaveUserRoles([FromBody] UserRoleRequest request)
        {
            await userService.SaveUserRole(request);
            return Ok();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var data = await userService.Login(loginDTO);
            return Ok(data);
        }
    }
}
