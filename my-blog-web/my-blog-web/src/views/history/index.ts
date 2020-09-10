import { Options, Vue } from "vue-class-component";
import {Http} from "@/api/http";
import {Singleton} from "@/utils/singleton";
@Options({
    components: {
        data:{
        }
    }
})
export default class History extends Vue {
     msg ='我是history'
    async created(){
      const data = await Singleton.make(Http).get('Advertisement/List')
        console.log('data',data)
    }
   
}