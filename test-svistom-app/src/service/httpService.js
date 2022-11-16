import axios from "axios"

export default {
    install(Vue) {
        const baseURL = process.env.VUE_APP_BASE_URL;
        Vue.prototype.$get = function(url) {
            return axios.get(`${baseURL}${url}`)
            .catch((resp)=>{
                if(resp.response.status === 401){
                    sessionStorage.removeItem('token');
                    window.location.replace(process.env.VUE_APP_LOGOUT_URL)
                }
            })
        }

        Vue.prototype.$post = function(url,data = null) {
            return axios.post(`${baseURL}${url}`,data)
            .catch((resp)=>{
                if(resp.response.status === 401){
                    sessionStorage.removeItem('token');
                    window.location.replace(process.env.VUE_APP_LOGOUT_URL)
                }
            })
        }

        Vue.prototype.$put = function(url,data = null) {
            return axios.put(`${baseURL}${url}`,data)
            .catch((resp)=>{
                if(resp.response.status === 401){
                    sessionStorage.removeItem('token');
                    window.location.replace(process.env.VUE_APP_LOGOUT_URL)
                }
            })
        }
    }
}