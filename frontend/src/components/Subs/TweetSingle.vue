<style scoped>
.twi-left {
  float: left;
  /* width: 10%; */
  margin-left: 3%;
  margin-top: 10px;
  /* height: 80px; */
}
.user-avatar-div {
  width: 60px;
  height: 60px;
  border-radius: 30px;
  overflow: hidden;
  box-shadow: #e9eaf1 0px 0px 4px;
}
.user-avatar-div:hover {
  box-shadow: #d1d3e5 0px 0px 8px;
}
.user-avatar {
  width: 60px;
}
.user-message {
  margin-top: 8px;
  width: 60px;
}

.twi-right {
  float: left;
  width: 84%;
  margin-left: 5px;
}
.twi-right-top-div {
  display: inline-block;
  width: 100%;
  background-color: white;
  margin-top: 10px;
  /* margin-bottom: 10px; */
  margin-left: 0px;
  margin-right: 0px;
}
.user-name {
  font-weight: bold;
  font-size: 16px;
  /* height:40px; */
}

.twi-text-block {
  margin-top: 10px;
  /* margin-bottom: 10px; */
  display: inline-block;
}
.twi-text-block:hover {
}
.twi-text {
  margin-bottom: 5px;
  width: 100%;
}
.img-handler {
  /* margin-bottom: 20px; */
}

.buttom-buttons {
  display: inline-block;
  background-color: white;
  width: 100%;
}
.comment-div {
  float: left;
  height: 50px;
  width: 20%;
  /*padding-left:50%;*/
  text-align: center;
}
.comment-div:hover {
  cursor: pointer;
}
/*.share-button{*/
/*    float: left;*/
/*    width:20%;*/
/*}*/
.likes-div {
  float: left;
  width: 20%;
  height: 50px;
  text-align: center;
}
.likes-div {
  cursor: pointer;
}

</style>



<template>
  <div>
    <div>
      <div class="twi-left">
        <router-link :to="{ path: '/Zoom', query: { visitor_id: item.message_sender_user_id }}">
          <Avatar size="large" v-bind:src="userAvt"></Avatar>
        </router-link>
      </div>

      <div class="twi-right">
        <div class="twi-right-top-div">
          <div id="name-time-die"  style="float:left;">
            <router-link :to="{ path: '/Zoom', query: { visitor_id: item.message_sender_user_id }}">
              <p class="user-name">{{userName}}</p>
              <p class="time">
                {{item.message_create_time}}
                <!-- <Icon type="ios-flame" size="18" style="color: #ff9900"></Icon>
                {{item.message_heat}} -->
              </p>
            </router-link>
          </div>
          <div id="follow-mes-butt-div" style="float: right;">
            <FollowButton
              @finish_update="send_update($event)"
              v-bind:isFollowing.sync="followByUser"
              v-bind:visitor="Number(item.message_sender_user_id)"
              style="float:right;"
            ></FollowButton>
          </div>
        </div>
        <div class="twi-text-block">
          <twitextblock
            class="twi-text"
            v-bind:fullText="item.message_content"
          ></twitextblock>
        </div>
      </div>
      <div class="buttom-buttons">
        <div class="comment-div" @click="showComment()">
          <VueStar animate="animated rubberBand" color="#F05654">
            <a slot="icon">
              <Icon v-if="commented" type="ios-chatboxes" size="24"></Icon>
              <Icon v-else type="ios-chatboxes-outline" size="24"></Icon>
              <span>{{commentsNum}}</span>
            </a>
          </VueStar>
        </div>

        <div class="likes-div" @click="doLike()">
          <VueStar animate="animated rubberBand" color="#F05654">
            <a slot="icon">
              <Icon type="ios-heart" size="24" v-if="likeByUser" style="color: #ed4014"></Icon>
              <Icon type="ios-heart-outline" size="24" v-else></Icon>
              <span>{{item.message_like_num}}</span>
            </a>
          </VueStar>
        </div>
      </div>
      <commentblock
        class="comment-block"
        @sendComm="doSendComment"
        v-bind:ifShowComment="ifShowComment"
        :comments="comments"
      ></commentblock>
    </div>
  </div>
</template>

<script>
import axios from "../../utils/axios";
import cookie from '../../utils/cookie'
import ImageHandler from "./ImageHandler";
import CommentBlock from "./CommentBlock";
import TwiTextBlock from "./TwiTextBlock";
import VueStar from "vue-star";
import FollowButoon from "./FollowButoon";
export default {
  name: "twitter-item",
  props: {
    item: Object,
    isFollowing: Boolean
  },
  data() {
    return {
      showMenu: false,
      ifShowComment: false,
      comments: [],
      likeByUser: false,
      followByUser: null,
      commentsNum: 0,
      commented: false,
      userAvt:"static/timg.jpg",
      userName:"user"
    };
  },
  methods: {
    ifBeMyTwi() {
      if (this.item.message_sender_user_id == cookie.getCookie("userID")) {
        return true;
      } else {
        return false;
      }
    },
    doShowMenu() {
      this.showMenu = !this.showMenu;
    },
    //展示评论或者取消展示评论
    showComment() {
      if (this.comments.length == 0) {
        //调用接口请求数据
        this.getComment();
      }
      this.ifShowComment = !this.ifShowComment;
    },
    doLike() {
      //console.log("like_message_id:", this.item.message_id);
      if (this.likeByUser == false) {
        this.likeByUser = true;
        this.item.message_like_num++;
        var data = {
          userID: this.userID
        }
        axios.like(this.item.message_id, data).then(Response => {
          if (Response.data.message == "success") {
          }
          //失败了就返回来
          else {
            this.likeByUser = false;
            this.item.message_like_num--;
            alert("点赞失败");
          }
        });
      } else if (this.likeByUser == true) {
        this.likeByUser = false;
        this.item.message_like_num--;
        var data = {
          userID : this.userID
        }
        axios.cancelLike(this.item.message_id, data).then(Response => {
          if (Response.data.message == "success") {
          }
          //失败了就返回来
          else {
            this.item.message_like_num++;
            this.item.likeByUser = true;
            alert("失败");
          }
        });
      }
      this.$emit("likeTwi");
    },
    getComment() {
      let data = {
        startFrom: this.comments.length,
        limitation: 10
      };
      axios.queryComment(this.item.message_id, data).then(Response => {
        console.log("评论返回" + Response.data)
        this.comments = Response.data.data;
      });
    },
    doSendComment(content) {
      let data = {
        comment_content: content,
        userID: this.userID
      };
      axios.addComment(this.item.message_id, data).then(Response => {
        if (Response.data.message == "success") {
          this.commentsNum += 1;
          this.commented = true;
          axios.getUserPublicInfo(cookie.getCookie("userID")).then(Response => {
            let timeObj = new Date();
            if (Response.data.message == "success") {
              let commTemp = {
                  nickname: Response.data.data.nickname,
                  comment_content: content,
                  comment_create_time: "just now"
              };
              this.comments.unshift(commTemp);
            } else {
              this.$Message.error("你需要输入些什么。");
            }
          });
        } else {
          this.$Message.error("你需要输入些什么。");
        }
      });
    },
    set_follow_status(status) {
      this.followByUser = status;
    },
    send_update(val) {
      console.log("sadasda",val)
      this.$emit("change_follow", val);
    }
  },
  created() {
    // this.collectByUser = this.item.collectByUser;
    this.likeByUser = this.item.likeByUser;
    this.followByUser = this.item.followByUser;
    this.commentsNum = this.item.message_comment_num;
    //求证是否点赞收藏关注
    var data1 = {
      user_id: cookie.getCookie("userID"),
      message_id: this.item.message_id
    }
    console.log(data1)
    axios.checkUserLikesMessage(data1).then(Response => {
      this.likeByUser = Response.data.data.like;
    });
    // axios.checkUserCollectMessage(
    //   cookie.getCookie("userID"),
    //   this.item.message_id
    // ).then(Response => {
    //   this.collectByUser = Response.data.data.favor;
    // });
    var data={
      userID: this.userID
    }
    axios.if_following_by_me(this.item.message_sender_user_id, data).then(Response => {
      this.followByUser = Response.data.data.if_following;
    });
    //取用户数据
        //获取以上的数据，这里由于可能是第二次拿数据，因此i+twiCount才是当前要处理的推的索引
    axios.getUserPublicInfo(this.item.message_sender_user_id).then(
      Response => {
        this.userName = Response.data.data.nickname;
        // this.userAvt = Response.data.data.avatar_url;
      }
    );

    //如果是转发的就取原推特条
    // if (this.item.message_transpond_message_id > 0) {
    //   axios.queryMessage(this.item.message_transpond_message_id).then(
    //     Response => {
    //       if (Response.data.message == "success") {
    //         this.item.rawItem = Response.data.data;
    //         axios.getUserPublicInfo(
    //           this.item.rawItem.message_sender_user_id
    //         ).then(Response => {
    //           this.rawItemUserName = Response.data.data.nickname;
    //           this.rawItemUserAvt = Response.data.data.avatar_url;
    //           //console.log("转发的推特",this.item.rawItem);
    //         });
    //       } else {
    //         alert("请求被转发推特失败");
    //       }
    //     }
    //   );
    // }
  },
  computed: {
    userID: function(){
      return cookie.getCookie("userID")
    }
  },
  watch: {
    followByUser(nval, oval) {},
    isFollowing: {
      deep: true,
      handler(nval) {
          this.followByUser = nval;
      }
    }
  },
  beforeMount() {},
  components: {
    imagehandler: ImageHandler,
    commentblock: CommentBlock,
    twitextblock: TwiTextBlock,
    VueStar,
    FollowButton: FollowButoon
  }
};
</script>
