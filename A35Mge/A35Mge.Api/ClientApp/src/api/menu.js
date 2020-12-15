import request from '@/utils/request'
var MenuApi = {
    getMenuList () {
        var global = JSON.parse(localStorage.getItem('GLOBAL'))
        return request({
            baseURL: global.BASEURL,
            url: `api/Menu/GetMenuList`,
            method: 'get'
        })
    }
}
export default MenuApi
