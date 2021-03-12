import request from '@/utils/request'
var DicListApi = {
    getList (params) {
        var pm = Object.keys(params).map(function (key) {
            return encodeURIComponent(key) + '=' + encodeURIComponent(params[key])
        }).join('&')
        return request({
            url: `api/DicInfoDetail/GetList?${pm}`,
            method: 'get'
        })
    },
    Add (data) {
        return request({
            url: `api/DicInfoDetail/Add`,
            method: 'post',
            data
        })
    },
    Update (data) {
        return request({
            url: `api/DicInfoDetail/Update`,
            method: 'put',
            data
        })
    },
    Delete (data) {
        return request({
            url: `api/DicInfoDetail/Delete`,
            method: 'delete',
            data
        })
    },
    Get (Id) {
        return request({
            url: `api/DicInfoDetail/Get/${Id}`,
            method: 'get'
        })
    }
}
export default DicListApi
