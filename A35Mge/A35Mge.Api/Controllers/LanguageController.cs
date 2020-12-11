﻿using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model;
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
        private readonly A35MgeDbContext a35MgeDbContext;

        public LanguageController(A35MgeDbContext a35MgeDbContext)
        {
            this.a35MgeDbContext = a35MgeDbContext;
        }

        /// <summary>
        /// 新增语种
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResultModel), StatusCodes.Status400BadRequest), AllowAnonymous]
        public async Task<IActionResult> AddLanguageType([FromBody] LanguageType entity)
        {
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
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
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
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
            var data = await a35MgeDbContext.LanguageType
               .ToListAsync();
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
            var data = await a35MgeDbContext.LanguageType
                .Include(x => x.TranslateList)
                .FirstOrDefaultAsync(x => x.LanguageCode.Equals(LanguageCode, StringComparison.OrdinalIgnoreCase));
            return Ok(data.TranslateList);
        }
    }
}