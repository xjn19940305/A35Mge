using A35Mge.Model.common;
using A35Mge.Model.CommonList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Interface
{
    public interface IDicTypeService
    {
        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Add(DicTypeDTO dto);
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Update(DicTypeDTO dto);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task Delete(int[] ids);
        /// <summary>
        /// 获取分类 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<DicTypeResponseDTO> Get(int Id);
        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedViewModel> GetList(DicTypeRequest request);
    }
}
