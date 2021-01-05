using A35Mge.Model.Permission.Role;
using A35Mge.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "manager")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(
            IRoleService roleService
            )
        {
            this.roleService = roleService;
        }

        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            var data = await roleService.Get(Id);
            return Ok(data);
        }
        /// <summary>
        /// 获取角色数据分页显示
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] RoleRequestDTO dto)
        {
            var data = await roleService.GetAllRole(dto);
            return Ok(data);
        }
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleDTO role)
        {
            await roleService.Add(role);
            return Ok();
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleDTO role)
        {
            await roleService.Update(role);
            return Ok();
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            await roleService.Delete(ids);
            return Ok();
        }
        /// <summary>
        /// 根据角色id获得绑定的菜单ID
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet("{RoleId}")]
        public async Task<IActionResult> GetMenusIds([FromRoute] int RoleId)
        {
            var data = await roleService.GetMenuIdsFromRoleId(RoleId);
            return Ok(data);
        }
        /// <summary>
        /// 保存角色授权的菜单按钮
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Save([FromBody] RoleMenuDTO request)
        {
            await roleService.SaveRoleMenu(request);
            return Ok();
        }
    }
}
