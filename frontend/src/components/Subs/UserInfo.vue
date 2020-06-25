<template>
  <div id="left-container">
    <ElContainer id="left-container1" style="background-color:#f6f6f6;">
      <router-link :to="{ path: '/Zoom', query: { visitor_id: userID}}">
      <div style="float:left;margin-left:20px; margin-top:50px;">
      <Avatar
        src="static/timg.jpg"
        shape="circle"
        on-error
        size="large"
        style="height:60px; width:60px; border-radius:50%;"
      />
      </div>
      <div style="float:left; margin-top:70px;margin-left:10px;">
      <span style="font-weight:bold;font-size:20px;color:#0C0C0C">{{userName}}</span>
      </div>
      </router-link>
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
    </ElContainer>
  </div>
</template>

<style>
#left-container {
  position: fixed;
  width: 16%;
  background-color: white;
  left: 12%;
  top: 70px;
}
#left-container1 {
  /*background-color: white;*/
}
#left-container2 {
  /*background-color: white;*/
  margin-top: 10px;
}
.header-left-align {
  font-weight: bold;
  font-size: 20px;
  text-align: left;
  padding-top: 15px;
  padding-left: 15px;
}
#trends-container {
  line-height: 17px;
  margin-bottom: 10px;
  text-align: left;
}
#trends-name {
  font-weight: bold;
  color: #1da1f2;
  font-size: 15px;
  padding-left: 15px;
}
#tweets-times {
  color: #657786;
  font-size: 12px;
  padding-left: 15px;
}
.load-more:hover {
  cursor: pointer;
}
.no-more {
  width: 100%;
  color: #cccccc;
}
</style>

<script>
import axios from '../../utils/axios'
import cookie from '../../utils/cookie'
export default {
  name: "userInfo",
  // props:{
  //   inject_topics: {
  //     required: false,
  //     default: null,
  //   }
  // },
  data() {
    return {
      userName:"userName",
      userID:"0"
    };
  },
  mounted() {
    this.userID = cookie.getCookie("userID")
    console.log("登录：", this.userID)
    console.log(this.userID)
    axios.getUserPublicInfo(this.userID).then(Response=>{
    console.log(Response)
    if(Response.data.code==200 && Response.data.message=="success")
      {
        this.userName = Response.data.data.nickname
        console.log(this.userName)
      }
      else{
        console.log("fail")
        this.userName="userName"
      }
    })
  },
};
</script>
