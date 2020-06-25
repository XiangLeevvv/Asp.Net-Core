<template>
  <div class="toFollow-container">
    <router-link class="user-container" :to="{ path: '/Zoom', query: { visitor_id: user_info.user_id }}">
      <div class="user-inner-container">
      <div class="av-container">
        <Avatar  size=large style="margin-bottom: 10px" class="av" src="static/timg.jpg"></Avatar>
      </div>
      <div class="name-container">
        <div style="font-size: 16px;font-weight: bold; ">{{user_info.nickname}}</div>
        <div style="font-size: 10px;color: grey;margin-top: 6px">@ {{user_info.nickname}}</div>
      </div>
      <img class="confirm-container" style="width: 20px;height: 20px" v-bind:src="confirm_url"/>
      </div>
    </router-link>

  </div>
</template>
<script>
import ImageHandler from "./ImageHandler";
import axios from '../../utils/axios'
export default {
  name: "User-Item",
  components: {ImageHandler},
  data() {
    return {
        user_info: {
        user_id: -1,
        nickname: "",
        register_time: "",
        self_introduction: "",
        followers_num: 0,
        follows_num: 0,
        avatar_url: "static/timg.jpg",
        messages_num: 0,
        collection_num: 0,
      },
      confirm_url:"/static/confirmed.png"
    };
  },
  props: {
    p_user_id:{type:Number,default:null},
    p_user_info:{type:Object,default:null},
    p_follow_info:{type:Object,default:null}
  },
  methods: {
    get_info: function(user_id) {
      console.log(user_id)
      axios.getUserPublicInfo(user_id).then(Response => {
        if (Response.data.message == "success") {
          this.load_info(Response.data.data);
          console.log("user_id:"+user_id);
          console.log(Response.data);
        }
      });
    },
    load_info: function(info) {
      console.log(info)
      this.user_info = info;
    },
    load_follow_info(info){
      console.log("followInfo", info)
      this.user_info.user_id=info.user_id;
      this.user_info.nickname=info.user_nickname;
      this.user_info.avatar_url="static/timg.jpg";
    }
  },
  mounted(){
    if(this.p_user_id){
      this.get_info(this.p_user_id);
    };
    if(this.p_user_info){
      this.load_info(this.p_user_info);
    };
    if(this.p_follow_info){
      this.load_follow_info(this.p_follow_info);
    }
  },
  watch:{
    p_user_info(nval,oval){
      this.load_info(nval);
    },
    p_user_id(nval,oval){
      this.get_info(nval);
    },
    p_follow_info(nval,oval){
      this.load_follow_info(nval);
    }
  }



};
</script>
<style scoped>
.user-container{
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
}

  .toFollow-container{
    margin-top: 10px;
    margin-bottom: 10px;
    margin-left: 0px;
    width: 300px;
    
  }
  .user-inner-container{
    width: 90%;
    margin-left: 20px;
    margin-bottom: 20px;
  }
  .av-container{
    width: 18%;
    float: left;

  }

  .name-container{
    float: left;

  }
  .name-container div{
    margin-left: 10px;
    margin-top: 2px;
  }

  .name-container div:hover{
    text-decoration: underline;
    margin-left: 10px;
    margin-top: 2px;

  }
  .confirm-container{
    margin-top: 2px;
    margin-left: 5px;
    float: left;
  }



</style>
