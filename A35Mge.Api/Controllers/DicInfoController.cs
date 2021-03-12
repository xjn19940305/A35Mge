using A35Mge.Model.CommonList;
using A35Mge.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "manager")]
    [Authorize]
    public class DicInfoController : ControllerBase
    {
        private readonly IDicTypeService service;

        public DicInfoController(
            IDicTypeService service
            )
        {
            this.service = service;
        }
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            var data = await service.Get(Id);
            return Ok(data);
        }
        /// <summary>
        /// 获取字典分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] DicTypeRequest request)
        {
            var data = await service.GetList(request);
            return Ok(data);
        }

        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DicTypeDTO dto)
        {
            await service.Add(dto);
            return Ok();
        }
        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DicTypeDTO dto)
        {
            await service.Update(dto);
            return Ok();
        }
        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            await service.Delete(ids);
            return Ok();
        }
    }
}
