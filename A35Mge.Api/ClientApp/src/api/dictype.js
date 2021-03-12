import request from '@/utils/request'
var DicTypeApi = {
    getList (params) {
        var pm = Object.keys(params).map(function (key) {
            return encodeURIComponent(key) + '=' + encodeURIComponent(params[key])
        }).join('&')
        return request({
            url: `api/DicInfo/GetList?${pm}`,
            method: 'get'
        })
    },
    Add (data) {
        return request({
            url: `api/DicInfo/Add`,
            method: 'post',
            data
        })
    },
    Update (data) {
        return request({
            url: `api/DicInfo/Update`,
            method: 'put',
            data
        })
    },
    Delete (data) {
        return request({
            url: `api/DicInfo/Delete`,
            method: 'delete',
            data
        })
    },
    Get (Id) {
        return request({
            url: `api/DicInfo/Get/${Id}`,
            method: 'get'
        })
    }
}
export default DicTypeApi
