<template>
  <div class="toFollow-container" v-show="user_info.nickname!=''">
    <div class="user-container">
      <div @click="flushCom" class="user-inner-container">
        <router-link :to="{ path: '/Zoom', query: { visitor_id: user_info.user_id }}">
          <div class="av-container">
            <Avatar size="large" style="margin:auto" class="av" v-bind:src="user_info.avatar_url"></Avatar>
          </div>
          <div class="name-container">
            <div style="font-size: 20px;font-weight: bold; ">{{user_info.nickname}}</div>
            <div style="font-size: 10px;color: grey;margin-top: 0px;float:left;">@{{user_info.nickname}}</div>
          </div>
          <img class="confirm-container" style="width: 20px;height: 20px" v-bind:src="confirm_url" />
        </router-link>
      </div>
      <FollowButton
        class="user-follow-button"
        v-bind:isFollowing.sync="isFollowing"
        v-bind:visitor="user_info.user_id"
        @finish_update="send_update($event)"
      />
    </div>
  </div>
</template>
<script>
import ImageHandler from "./ImageHandler";
import cookie from "../../utils/cookie"
import FollowButton from "./FollowButoon";
import axios from '../../utils/axios'
export default {
  name: "UserForZoom",
  data() {
    return {
      isFollowing: false,
      user_info: {
        user_id: -1,
        nickname: "",
        register_time: "",
        self_introduction: "",
        followers_num: 0,
        follows_num: 0,
        avatar_url: "static/timg.jpg",
        messages_num: 0,
        collection_num: 0
      },
      confirm_url: "/static/confirmed.png"
    };
  },
  computed:{
      userID: function(){
        return cookie.getCookie("userID")
      }
  },
  components: { ImageHandler, FollowButton },
  props: {
    p_user_id: { type: Number, default: null },
    p_user_info: { type: Object, default: null },
    p_follow_info: { type: Object, default: null }
  },
  methods: {
    flushCom()
    {
      this.$router.go(0)
    },
    get_info: function(user_id) {
      console.log(user_id);
      var data = {
        userID: this.userID
      }
      axios.if_following_by_me(user_id, data).then(Response => {
        this.isFollowing = Response.data.data.if_following;
      });
      this.getUserPublicInfo(user_id).then(Response => {
        if (Response.data.message == "success") {
          this.load_info(Response.data.data);
          console.log("user_id:" + user_id);
          console.log(Response.data);
        }
      });
    },
    load_info: function(info) {
      console.log(info);
      this.user_info = info;
    },
    load_follow_info(info) {
      console.log(info);
      this.user_info.user_id = info.user_id;
      this.user_info.nickname = info.nickName;
      this.user_info.avatar_url = "static/timg.jpg";
    },
    change_follow(val){
      if(this.isFollowing!=val){
        this.isFollowing=val;
        this.$emit("change_my_follow",val,this.user_info.user_id);
      }
    },
    send_update(val){
      this.$emit("change_my_follow",val,this.user_info.user_id);
    }
  },
  mounted() {
    if (this.p_user_id) {
      this.get_info(this.p_user_id);
    }
    if (this.p_user_info) {
      this.load_info(this.p_user_info);
    }
    if (this.p_follow_info) {
      this.load_follow_info(this.p_follow_info);
    }
  },
  watch: {
    p_user_info(nval, oval) {
      this.load_info(nval);
    },
    p_user_id(nval, oval) {
      this.get_info(nval);
    },
    p_follow_info(nval, oval) {
      this.load_follow_info(nval);
    },
    isFollowing(nval,oval){
      
    }
  }
};
</script>
<style scoped>
.user-container {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  height: 40px;
  /* margin-top: 5%; */
  margin-bottom: 5%;
}

.user-container:hover:after {
  text-decoration: underline;
}
.toFollow-container {
  margin-top: 10px;
  margin-bottom: 10px;
  margin-left: 0px;
}
.user-inner-container {
  width: 80%;
  height: 100%;
  /* display: flex; */
  /* flex-direction: row; */
  /* justify-content: flex-start; */
  /* align-items: center; */
}
.user-follow-button {
  text-align: center;
  width: 100px;
  height: 30px;
  font-size: 2px;
  margin-right: 10px;
}
.av-container {
  /* width: 18%; */
  float: left;
  margin-left: 30px;
}
.name-container {
  margin-left: 2%;
  margin-top: 0px;
  float: left;
}
.confirm-container {
  float:left;
  margin-top: 5px;
  margin-left: 5%;
}
</style>
