import {Helper} from "@/utils/helper";

const root = 'https://localhost:5001/'

import axios  from 'axios'

export class Http{
    constructor() {
        axios.interceptors.request.use(config=>{
            console.log('config',config)
            config.url = root+config.url
            return config
        })
        axios.interceptors.response.use(
            response => {
                // resolve
                let res 
                if (response.data?.data) {
                    res = response.data.data
                } else {
                    res = response.data
                }
                return Helper.snakeNameWithObject(res, false)
            },
            error => {
                // reject
                if (error.response.status === 401) {
                } else if (error.response.status === 400) {
                }
                return Promise.reject(error)
            }
        )
    }
    get<T = any>(uri: string, data: any = {}, config: object = {}) {
        return axios.get<T>(Helper.formatGetJson(uri, Helper.snakeNameWithObject(data, true)), config)
    }

    post<T = any>(uri: string, data: any = {}, config: object = {}) {
        return axios.post<T>(uri, data, config)
    }

    put<T = any>(uri: string, data: any, config: object = {}) {
        return axios.put<T>(uri, data, config)
    }

    del<T = any>(uri: string, config: object = {}) {
        return axios.delete<T>(uri, config)
    }
}

// function apiAxios(method:string,url:string,params:object):Promise<any>{
//     axios({
//         method: method,
//         url: url,
//     })
//    
// }