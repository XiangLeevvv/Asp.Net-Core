<style>
#root-div {
  position: fixed;
  height: 100%;
  width: 100%;
  overflow: scroll;
}
.center-fix{
	position: fixed;
	z-index:99;
  margin:auto;
  left:0;
  right:0;
  top:0;
  bottom:0;
}

.ProfileImg {
  margin-top: 7%;
  margin-left: 45%;
}

.avartarRight{
  height:100px;
  float: left;
}

.name{
  font-size: 16px;
  margin-left: 16%;
  margin-top: 5px;
  color:#010101;
}

.numbers{
  margin-top: 4%;
}
.ivu-tabs-nav .ivu-tabs-tab:hover {
  color: #4000FF;
}

</style>
<template>
  <div id="root-div">
    <div id="topAnchor"></div>
    <loadingAnimate v-if="loading" class="center-fix" />
    <div class="ProfileImg">
        <Avatar style="width:100px;height:100px;border-radius:50%;float:left;" src="static/timg.jpg"></Avatar>
        <FollowButton
              v-bind:followerCount.sync="followerCount"
              v-bind:isFollowing.sync="isFollowing"
              @finish_update="follow_visitor($event)"
              v-bind:visitor="Number(visitor)"
            ></FollowButton>
        <div class="avatarRight">
          <div class="name">
            {{nickname}}
          </div>
          <div class="numbers">
            <Button type="text" style="font-size:16px;">Welog<br>{{postsCount}}</Button>
            <Button type="text" style="font-size:16px;">我关注的人<br>{{followingCount}}</Button>
            <Button type="text" style="font-size:16px;">关注我的人<br>{{followerCount}}</Button>
          </div>
        </div>
    </div>
    <Tabs type="line" style="margin-top:4%;margin-left:auto;margin-right:auto;width:80%;">
        <TabPane label="Welog">
          <tweets
              @stop_loading="stop_loading"
              :ref="'twe1'"
              v-on:change_following="change_follow(arguments)"
              type="userhome"
              v-bind:info="visitor"
            ></tweets>
        </TabPane>
        <TabPane label="我关注的人">
          <div v-for="user in followingList" v-bind:key="user.user_id">
              <userForZoom
                v-bind:p_user_id="user.user_id"
                :ref="'following'+user.user_id"
                @change_my_follow="change_my_follow(arguments)"
              ></userForZoom>
            </div>
        </TabPane>
        <TabPane label="关注我的人">
          <div v-for="user in followersList" v-bind:key="user.user_id">
              <userForZoom
                v-bind:p_user_id="user.user_id"
                :ref="'follower'+user.user_id"
                @change_my_follow="change_my_follow(arguments)"
              ></userForZoom>
          </div>
        </TabPane>
    </Tabs>
    <backToTop></backToTop>
  </div>
</template>

<script>
import loadingAnimate from "./animate/loading";
import Tweets from "./Subs/Tweets.vue";
import User from "./Subs/User";
import UserForZoom from "./Subs/UserForZoom";
import FollowButton from "./Subs/FollowButoon"
import backToTop from "./Subs/BackToTop"
import axios from '../utils/axios'
import cookie from '../utils/cookie'


export default {
  name: "Zoom",

  data() {
    return {
      loading: true,
      num: 0,
      visitor: 0,
      user: 0,
      confirm_url: "static/confirmed.png",
      user_info: {},
      avatar: null,
      nickname: "NickName",
      personBkgImg: "/static/background.png",
      postsCount: 0,
      followerCount: 0,
      followingCount: 0,
      isFollowing: null,
      personAccount: null,
      joinTime: null,
      showName: "tweetsShow",
      bindTabItemStyle: "",
      status: [
        "tweetsShow",
        "followingShow",
        "followersShow",
      ],
      navStatus: {
        tweetsShow: true,
        followingShow: false,
        followersShow: false,
      },
      selfIntroduction: "The man is lazy,leaving nothing.",
      toFollowList: [],
      followingList: [],
      followersList: [],
      user_info: null,
      my_info: null
    };
  },
  computed:{
      userID: function(){
        return cookie.getCookie("userID")
      }
  },
  components: {
    loadingAnimate,
    Tweets,
    userForZoom: UserForZoom,
    User,
    FollowButton,
    backToTop,
  },
  created() {

    this.visitor = Number(this.$route.query.visitor_id);
    this.user = cookie.getCookie("userID");
    console.log("user", this.user);
    try {
      var _this = this;
      axios.getUserPublicInfo(this.visitor).then(response => {
        _this.user_info = response.data.data;
        _this.nickname = response.data.data.nickname;
        console.log(this.nickname);
        _this.avatar = "static/timg.jpg";
        _this.postsCount = response.data.data.messages_num;
        _this.followerCount = response.data.data.followers_num;
        _this.followingCount = response.data.data.follows_num;
        // _this.selfIntroduction = response.data.data.self_introction;
        // _this.joinTime = response.data.data.register_time;
      });
      var data = {
        userID: this.userID
      }
      var p1 = this.if_following_by_me(this.visitor, data);
      var p2 = this.queryFollowingFor(this.visitor, 1, 10);
      var p3 = this.queryFollowersFor(this.visitor, 1, 10);

      Promise.all([p1, p2, p3]).then(res => {
        console.log("完成数据加载", res);
        _this.isFollowing = res[0].data.data.if_following;
        _this.followingList = res[1].data.data;
        _this.followersList = res[2].data.data;
        console.log("这个人的followersList", _this.followersList);
      });
      axios.getUserPublicInfo(this.user).then(response => {
        this.my_info = response.data.data;
      });
    } catch (e) {
      return {
        result: false,
        errMsg: "Can't connect with server"
      };
    }
  },
  mounted: function getUser() {
    // this.loading = true;
    this.visitor = Number(this.$route.query.visitor_id);
    this.user = cookie.getCookie("userID");
    console.log("user", this.user);
  },
  methods: {
    setFalseStatus() {
      this.navStatus.followersShow = false;
      this.navStatus.followingShow = false;
      this.navStatus.tweetsShow = false;
    },
    stop_loading(){
      this.loading = false;
    },
    tweetsClicked() {
      console.log("tweetsClicked");
      this.setFalseStatus();
      this.navStatus.tweetsShow = true;
      this.showName = "tweetsShow";
      console.log(this.navStatus);
    },

    followingClicked() {
      console.log("followingClicked");
      this.setFalseStatus();
      this.navStatus.followingShow = true;
      this.showName = "followingShow";
      console.log(this.navStatus);
    },

    followersClicked() {
      console.log("followersClicked");
      this.setFalseStatus();
      this.showName = "followersShow";
      this.navStatus[this.showName] = true;
      console.log(this.navStatus.followersShow);
    },
    change_follow(event) {
      console.log("change_follow", event[1]);
      if (this.isFollowing != event[0] && this.visitor == event[1]) {
        this.isFollowing = event[0];
        if (event[0]) {
          this.followerCount++;
        } else {
          this.followerCount--;
        }
      }
      this.change_my_follow(event);
    },
    change_my_follow(event) {
      console.log(event);
      for (var i = 0; i < this.followingList.length; ++i) {
        if (this.followingList[i].user_id == event[1]) {
          this.$refs["following" + event[1]][0].change_follow(event[0]);
          break;
        }
      }
      for (var i = 0; i < this.followersList.length; ++i) {
        if (this.followersList[i].user_id == event[1]) {
          this.$refs["follower" + event[1]][0].change_follow(event[0]);
          break;
        }
      }

      if (this.$refs.twe1) {
        this.$refs.twe1.change_follow2(event[0], event[1]);
      }
      if (this.$refs.twe2) {
        this.$refs.twe2.change_follow2(event[0], event[1]);
      }
      if (this.visitor == cookie.getCookie("userID")) {
        var k = [];
        for (var i = 0; i < this.followingList.length; ++i) {
          if (this.followingList[i].user_id.toString() != event[1].toString()) {
            k.push(this.followingList[i]);
          }
        }
        if (event[0] && event[1]!=-1) {
          var temp = new Object();
          temp.user_id = event[1];
          k.push(temp);
        }
        this.followingList = k;
        this.followingCount = k.length;
      }
    },
    follow_visitor(val){
      var k = [];
      for (var i = 0; i < this.followersList.length; ++i) {
        if (this.followersList[i].user_id != this.user) {
          k.push(this.followersList[i]);
        }
      }
      if (val) {
        var temp = new Object();
        temp.user_id = this.my_info.user_id;
        temp.avatar_url = "static/timg.jpg";
        temp.nickname = this.my_info.nickname;
        k.push(temp);
      }
      this.followersList = k;
      var temp=new Array();
      temp[0]=val;
      temp[1]=this.visitor;
      this.change_follow(temp);
    },
    getRightClass(typeName){
      if(this.navStatus[typeName] == true){
        if(this.visitor == this.user)
          return "TabItem-four-Show"
        else
          return "TabItem-three-Show"
      }else{
        if(this.visitor == this.user)
          return "TabItem-four"
        else
          return "TabItem-three"
      }
    }
  },
  watch: {
    "$route.params.PersonAccount": "initUserID",
    isFollowing(val) {
      /*if (this.$refs.twe1) {
        this.$refs.twe1.change_follow2(val, this.visitor);
      }
      if (this.$refs.twe2) {
        this.$refs.twe2.change_follow2(val, this.visitor);
      }*/

    }
  },
  // beforeRouteEnter(to,from,next){
  //     next(vm=>{
  //       if(!vm.getCookie("userID"))
  //       {
  //         console.log("请先登录")
  //         vm.$router.push("index")
  //       }
  //     })
  //   }
};
</script>


