import request from '@/utils/request'
var languageApi = {
    getLanglist (appId) {
        var global = JSON.parse(localStorage.getItem('GLOBAL'))
        return request({
            baseURL: global.BASEURL,
            url: `api/Language/GetLanguageList`,
            method: 'get'
        })
    },
    getTranslate (code) {
        var global = JSON.parse(localStorage.getItem('GLOBAL'))
        return request({
            baseURL: global.BASEURL,
            url: `api/Language/GetTranslateFromLanguage/${code}`,
            method: 'get'
        })
    }
}
export default languageApi
