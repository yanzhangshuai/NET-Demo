export  class Helper{

    static awaitWrapper<T = any>(promise: Promise<T>) {
        return promise.then((data: T) => [null, data]).catch((err: any) => [err, null])
    }


    static formatGetJson(uri: string, data: { [T: string]: string } = {}) {
        let keys = Object.keys(data)
        if (!keys.length) return uri
        let joiner = uri.lastIndexOf('?') == -1 ? '?' : '&'
        return (
            uri +
            joiner +
            keys
                .map(item => (typeof data[item] !== 'undefined' ? `${item}=${data[item]}` : ''))
                .filter(item => item !== '')
                .join('&')
        )
    }
    
    static snakeNameWithObject(object: any, encode: boolean = false) {
        if (Array.isArray(object)) {
            object.forEach((item, index) => {
                object[index] = this.snakeNameWithObject(item, encode)
            })
        } else if (object !== null && typeof object == 'object' && !Array.isArray(object)) {
            object = Object.assign({}, object)

            for (let key in object) {
                if (typeof object[key] == 'object') {
                    object[key] = this.snakeNameWithObject(object[key], encode)
                }

                let newKey = encode
                    ? key.replace(/([A-Z])/g, '_$1').toLowerCase()
                    : key.replace(/_(\w)/g, ($0, $1) => {
                        return $1.toUpperCase()
                    })

                if (newKey !== key) {
                    object[newKey] = object[key]
                    delete object[key]
                }
            }
        } else if (typeof object === 'string') {
            return encode
                ? object.replace(/([A-Z])/g, '_$1').toLowerCase()
                : object.replace(/_(\w)/g, ($0, $1) => {
                    return $1.toUpperCase()
                })
        }

        return object
    }
}