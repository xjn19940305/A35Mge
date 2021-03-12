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
    public class DicInfoDetailController : ControllerBase
    {
        private readonly IDicListService service;

        public DicInfoDetailController(
            IDicListService service
            )
        {
            this.service = service;
        }
        /// <summary>
        /// 获取字典明细信息
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
        /// 获取字典明细分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] DicListRequest request)
        {
            var data = await service.GetList(request);
            return Ok(data);
        }

        /// <summary>
        /// 新增字典明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DicListDTO dto)
        {
            await service.Add(dto);
            return Ok();
        }
        /// <summary>
        /// 更新字典明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DicListDTO dto)
        {
            await service.Update(dto);
            return Ok();
        }
        /// <summary>
        /// 删除字典明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int[] ids)
        {
            await service.Delete(ids);
            return Ok();
        }
        /// <summary>
        /// 根据字典名称获得列表
        /// </summary>
        /// <param name="DicName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListFromType([FromQuery] string DicName)
        {
            return Ok(await service.GetListFromType(DicName));
        }
    }
}
