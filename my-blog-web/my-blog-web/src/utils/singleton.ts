interface IInstance<T> {
    new (...params: any[]): T
}
export class Singleton {
    static instances: Map<IInstance<any>, any> = new Map()

    static make<T>(instance: IInstance<T>): T {
        this.instances.has(instance) || this.instances.set(instance, new instance())
        return this.instances.get(instance)
    }
}
