<template>
<div id="root-div">
  <div id="topAnchor"></div>
      <loadingAnimate v-if="loading" class="center-fix"/>
      <UserInfo></UserInfo>
        <div id="middle-container" >
            <tweets @stop_loading="stop_loading" type="explore"></tweets>
        </div>
    <backToTop></backToTop>
</div>
</template>


<script>
import Tweets from "./Subs/Tweets"
import UserInfo from "./Subs/userInfo"
import backToTop from "./Subs/BackToTop"
import loadingAnimation from "./animate/loading"
import axios from '../utils/axios'
import cookie from '../utils/cookie'

export default {
    name:'explore',
    components:{
      UserInfo,
      "tweets":Tweets,backToTop,loadingAnimation
    },
    data(){
        return {
          loading:true
        }
    },
    methods:{
      stop_loading(){
        this.loading = false;
      }
    },
    created(){
    },
    beforeMount() {
    },
    // beforeRouteEnter(to,from,next){
    //   next(vm=>{
    //     if(!vm.cookie.getCookie("userID"))
    //     {
    //       console.log("请先登录")
    //       vm.$router.push("index")
    //     }
    //   })
    // }
}
</script>


<style scope>
#root-div{
  position: fixed;
  height: 100%;
  width: 100%;
  /*background-color:#F5F5F5;*/
  overflow: auto;
}
#middle-container{
    width: 40%;
    background-color: white;
    margin-left: auto;
    margin-right: auto;
    margin-top: 70px
  }
  .center-fix{
	position: fixed;/*固定位置*/
	z-index:99;/*设置优先级显示，保证不会被覆盖*/
  margin:auto;
 left:0;
 right:0;
 top:0;
 bottom:0;
}
</style>

