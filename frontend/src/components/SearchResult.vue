<style scoped>
ul li {
  list-style-type: none;
}
#root-div {
  position: fixed;
  height: 100%;
  width: 100%;
  background-color: rgb(230, 236, 240);
  overflow: auto;
}
#middle-container{
  width: 40%;
  background-color: white;
  margin-left: auto;
  margin-right: auto;
  margin-top: 70px
}
.infor-avatar {
  margin-top: 10px;
  margin-left: 15px;
  margin-bottom: 5px;
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

<template>
  <div id="root-div">
    <div id="topAnchor"></div>
    <loadingAnimate  v-if="loading" class="center-fix"/>

    <div id="middle-container">
      <tweets @stop_loading="stop_loading" type="search" v-bind:info="searchKey"></tweets>
    </div>

    <ElContainer id="right-container">
    </ElContainer>
    <backToTop></backToTop>
  </div>
</template>
<script>
import Tweets from "./Subs/Tweets"
import loadingAnimate from "./animate/loading"
import backToTop from "./Subs/BackToTop"
import axios from '../utils/axios'
import cookie from '../utils/cookie'
export default {
  name: "SearchResult",
  components:{
    "tweets": Tweets,
    loadingAnimate,backToTop
  },
  data() {
    return {
      flag: false,
      loading:true,
      searchKey : this.$route.query.searchKey,
      sites: [{ name: "Runoob" }, { name: "Google" }, { name: "Taobao" }],
      // inject_topics: [],
      // inject_users: [],
    };
  },
  mounted(){
    // console.log("搜索码为,", this.searchKey)
    axios.search(this.searchKey, 0, 10).then(response=>{
          console.log("测试搜索結果", response);
          this.inject_topics = response.data.data.topics;
          this.inject_users = response.data.data.users;
          var users = []
          this.inject_users.forEach(each=>{
            users.push({
              avatar_url: each.avatar_url,
              user_id: each.user_id,
              followers_num: each.followers_num,
              user_nickname: each.nickname
            });
          });
    //       console.log("更改後", users);
    //       this.inject_users = users;
    //       console.log("搜索用戶信息", this.inject_users);
          this.flag = true;
    });
  },
  
  methods:{
    stop_loading(){
      this.loading = false;
    }
  },
  watch:{

  },
  // beforeRouteEnter(to,from,next){
  //     next(vm=>{
  //       if(!vm.cookie.getCookie("userID"))
  //       {
  //         console.log("请先登录")
  //         vm.$router.push("index")
  //       }
  //     })
  //   }
};
</script>
