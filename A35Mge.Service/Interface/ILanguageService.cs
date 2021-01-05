using A35Mge.Database.Entities;
using A35Mge.Model.common;
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
        Task<List<LanguageTypeDTO>> GetLanguageTypeList(LanguageTypeDTO dto);
        /// <summary>
        /// 根据Id获取语种信息
        /// </summary>
        Task<LanguageTypeDTO> Get(int Id);
        /// <summary>
        /// 新增语种
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        Task AddLanguageType(LanguageTypeDTO language);
        /// <summary>
        /// 修改语种
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        Task UpdateLanguageType(LanguageTypeDTO language);
        /// <summary>
        /// 删除语种信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteLanType(int Id);
        /// <summary>
        /// 根据语种code获取对应翻译内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<List<LanDTO>> GetTranslateFromLanguage(string code);
        /// <summary>
        /// 获取所有的翻译内容
        /// </summary>
        /// <returns></returns>
        Task<PagedViewModel> GetAllTranslate(TranslateRequest request);

        /// <summary>
        /// 新增翻译内容
        /// </summary>
        /// <param name="translate"></param>
        /// <returns></returns>
        Task AddTranslate(LanDTO translate);
        /// <summary>
        /// 修改翻译内容
        /// </summary>
        /// <param name="translate"></param>
        /// <returns></returns>
        Task UpdateTranslate(LanDTO translate);
        /// <summary>
        /// 批量删除翻译内容
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        Task DeleteTanslate(int[] Ids);
        /// <summary>
        /// 根据ID获取具体的翻译内容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<LanDTO> GetTranslateFromId(int Id);
    }
}
