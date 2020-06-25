<style scoped>
.unfollow {
  background-color: maroon;
  border:darkred;
}
.font{
    font-weight:bold;
    /* font-size: 20px; */
}


</style>
<template>
  <Button
    class="font"
    type="primary"
    shape="circle"
    v-bind:disabled="typeof(isFollowing) == 'object' ? true : false "
    v-on:click="click_func()"
    v-bind:class="my_class"
    v-if="isShow"
  ><span style="font-size:12px;">{{content}}</span></Button>
</template>

<script>
import cookie from '../../utils/cookie'
import axios from '../../utils/axios'
export default {
  data() {
    return {
    };
  },
  props: {
    isFollowing: {
      default: null
    },
    followerCount: {
      type: Number,
      default: 0
    },
    visitor: {
      type: Number,
      default: 0
    }
  },
  watch:{
    isFollowing(newVal){

    }
  },
  methods: {
    unfollowClick() {
      this.$emit("update:isFollowing", false);
      this.$emit("update:followerCount", this.followerCount - 1);
      console.log("取消关注");
      var data = {
        userID: this.userID
      }
      this.cancelFollowingTo(this.visitor, data).then(response => {
        if (response.data.message != "success") {
          this.$emit("update:isFollowing", true);
          this.$emit("update:followerCount", this.followerCount + 1);
        }else{
          this.$emit("finish_update",false);
        }
      });

      console.log("unfollowClicked");
    },
    followClick() {
      this.$emit("update:isFollowing", true);
      this.$emit("update:followerCount", this.followerCount + 1);
      console.log("follow谁：", this.visitor);
      var data = {
        userID: this.userID
      }
      axios.followSb(this.visitor, data).then(response => {
        console.log("follow结果", response);
        if (response.data.message != "success") {
          this.$emit("update:isFollowing", false);
          this.$emit("update:followerCount", this.followerCount - 1);
        }else{
          this.$emit("finish_update",true);
        }
      });
      console.log("followClicked");
    },
    click_func() {
      if (this.isFollowing) {
        this.unfollowClick();
      } else {
        this.followClick();
      }
    }
  },
  computed: {
    userID: function(){
      return cookie.getCookie("userID")
    },
    is_following() {
      return this.isFollowing;
    },
    content() {
      return this.isFollowing ? "Unfollow" : "Follow";
    },
    my_class(){
        return this.isFollowing ? "unfollow" : "follow";
    },
    isShow(){
      if(cookie.getCookie('userID')&&this.visitor&&cookie.getCookie('userID').toString()!=this.visitor.toString()){
        return true;
      }else{
        return false;
      }
    }
  }
};
</script>

