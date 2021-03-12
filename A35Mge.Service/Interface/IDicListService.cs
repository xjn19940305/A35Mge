using A35Mge.Model.common;
using A35Mge.Model.CommonList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface IDicListService
    {
        /// <summary>
        /// 新增字典明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Add(DicListDTO dto);
        /// <summary>
        /// 修改字典明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Update(DicListDTO dto);
        /// <summary>
        /// 删除字典明细
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task Delete(int[] ids);
        /// <summary>
        /// 获取字典明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<DicListResponseDTO> Get(int Id);
        /// <summary>
        /// 分页获取字典列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedViewModel> GetList(DicListRequest request);
        /// <summary>
        /// 根据字典名称获取对应的字典类型
        /// </summary>
        /// <param name="DicName"></param>
        /// <returns></returns>
        Task<List<DicListResponseDTO>> GetListFromType(string DicName);
    }
}
