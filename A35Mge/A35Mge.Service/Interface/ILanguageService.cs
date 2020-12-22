using A35Mge.Database.Entities;
using A35Mge.Model.LanguageDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface ILanguageService
    {
        /// <summary>
        /// 获取所有的语种
        /// </summary>
        /// <returns></returns>
        Task<List<LanguageTypeDTO>> GetLanguageTypeList();
        /// <summary>
        /// 新增语种
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        Task AddLanguageType(LanguageType language);
        /// <summary>
        /// 根据语种code获取对应翻译内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<List<LanDTO>> GetTranslateFromLanguage(string code);

        /// <summary>
        /// 新增翻译内容
        /// </summary>
        /// <param name="translate"></param>
        /// <returns></returns>
        Task AddTranslate(Translate translate);
        /// <summary>
        /// 修改翻译内容
        /// </summary>
        /// <param name="translate"></param>
        /// <returns></returns>
        Task UpdateTranslate(Translate translate);
    }
}
