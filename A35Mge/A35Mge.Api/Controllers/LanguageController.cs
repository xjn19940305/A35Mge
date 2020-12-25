using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model;
using A35Mge.Model.LanguageDTO;
using A35Mge.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService languageService;

        public LanguageController(
            ILanguageService languageService
            )
        {
            this.languageService = languageService;
        }

        #region 语种

        /// <summary>
        /// 获取所有的语种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLanguageList([FromQuery] LanguageTypeDTO request)
        {
            var data = await languageService.GetLanguageTypeList(request);
            return Ok(data);
        }
        /// <summary>
        /// 根据语种ID获取语种详细信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLanType([FromQuery] int Id)
        {
            var data = await languageService.Get(Id);
            return Ok(data);
        }
        /// <summary>
        /// 新增语种
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddLanguageType([FromBody] LanguageTypeDTO dto)
        {
            await languageService.AddLanguageType(dto);
            return Ok();
        }
        /// <summary>
        /// 修改语种
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLanguageType([FromBody] LanguageTypeDTO dto)
        {
            await languageService.UpdateLanguageType(dto);
            return Ok();
        }
        /// <summary>
        /// 删除语种信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteLanType([FromQuery] int Id)
        {
            await languageService.DeleteLanType(Id);
            return Ok();
        }
        #endregion

        #region 翻译

        /// <summary>
        /// 根据语种code获取对应翻译内容
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <returns></returns>
        [HttpGet("{LanguageCode}")]
        public async Task<IActionResult> GetTranslateFromLanguage([FromRoute] string LanguageCode)
        {
            var data = await languageService.GetTranslateFromLanguage(LanguageCode);
            return Ok(data);
        }
        /// <summary>
        /// 获取所有的翻译内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTranslate([FromQuery] TranslateRequest request)
        {
            var data = await languageService.GetAllTranslate(request);
            return Ok(data);
        }
        /// <summary>
        /// 根据ID获取具体的翻译内容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTranslateFromId([FromQuery] int Id)
        {
            var data = await languageService.GetTranslateFromId(Id);
            return Ok(data);
        }

        /// <summary>
        /// 新增翻译内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTranslate([FromBody] LanDTO dto)
        {
            await languageService.AddTranslate(dto);
            return Ok();
        }

        /// <summary>
        /// 修改翻译内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTranslate([FromBody] LanDTO dto)
        {
            await languageService.UpdateTranslate(dto);
            return Ok();
        }
        /// <summary>
        /// 批量删除翻译内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTranslate([FromBody] int[] ids)
        {
            await languageService.DeleteTanslate(ids);
            return Ok();
        }
        #endregion
    }
}
