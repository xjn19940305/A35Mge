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

        /// <summary>
        /// 新增语种
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> AddLanguageType([FromBody] LanguageType entity)
        {
            await languageService.AddLanguageType(entity);
            return Ok();
        }
        /// <summary>
        /// 新增翻译内容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> AddTranslate([FromBody] Translate entity)
        {
            await languageService.AddTranslate(entity);
            return Ok();
        }
        /// <summary>
        /// 修改翻译内容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> UpdateTranslate([FromBody] Translate entity)
        {
            await languageService.UpdateTranslate(entity);
            return Ok();
        }

        /// <summary>
        /// 获取所有的语种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> GetLanguageList()
        {
            var data = await languageService.GetLanguageTypeList();
            return Ok(data);
        }

        /// <summary>
        /// 根据语种code获取对应翻译内容
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <returns></returns>
        [HttpGet("{LanguageCode}")]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> GetTranslateFromLanguage([FromRoute] string LanguageCode)
        {
            var data = await languageService.GetTranslateFromLanguage(LanguageCode);
            return Ok(data);
        }
    }
}
