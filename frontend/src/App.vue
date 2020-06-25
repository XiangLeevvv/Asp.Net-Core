<template>
  <div id="app" >
    <section v-if="$route.meta.keepAlive">
      <nav class="navBar" style="z-index: 1">
        <Menu mode="horizontal" :theme="theme1" active-name="home" style="padding-left:10%;padding-right: 40px;" @on-select="onSelect">
          <MenuItem name="home" router-link="{path: '/'}" style="width:9%" key="Home">
            <div class="over"><Icon type="ios-home-outline" class="menuIcon"></Icon>
            <span class="menuWords">主页</span></div>
          </MenuItem>
          <MenuItem name="personal" router-link :to="{ path: '/Zoom', query: { visitor_id: userID}}" style="width:11%" key="personal">
            <div class="over"><Icon type="ios-at-outline" class="menuIcon"></Icon>
            <span class="menuWords">个人空间</span></div>
          </MenuItem>
          <MenuItem name="explore" router-link="{path: '/Explore'}" style="width:10%" key="Explore">
            <div class="over"><Icon type="ios-eye-outline" class="menuIcon"></Icon>
            <span class="menuWords">发现</span></div>
          </MenuItem>
          <MenuItem name="experiment" router-link="{path: '/experiment'}" style="width:10%" key="Experiment">
            <div class="over"><Icon type="ios-help-outline" class="menuIcon"></Icon>
            <span class="menuWords">实验</span></div>
          </MenuItem>
          <Button shape="circle" @click="handleSearch" icon="ios-search" style="border:0; margin-left:25%;"></Button>
          <Input v-model = "model13" @keyup.enter.native="handleSearch" placeholder="在所有内容中查找" style="width: 25%;"></Input>
          <!-- <Button type="primary" style="float:right;margin-top:15px;" @click = "signOut">LOG OUT</Button> -->
        </Menu>
      </nav>
    </section>
    <router-view/>
  </div>
</template>

<script>
  import axios from './utils/axios'
  import cookie from './utils/cookie'
  export default {
    name: 'App',
    data(){
      return{
        mentionedCount:0,
        theme1:"light",
        model13: '',
        loading1: false,
        list: [],
        userID: 0
      }
    }
    ,
    mounted(){
      this.userID = cookie.getCookie("userID")
      },

    methods:{
      isRead(){
        this.mentionedCount = 0;
        console.log("读了")
      },

      handleSearch()
      {
        this.$router.push({
          path:'/searchResult',
          query:{
            searchKey:this.model13
          }
        })
        this.$router.go(0)
      },

      onSelect (d){
        this.$router.push({path:"/"+ d})
      },
      async signOut(){
        try {
          console.log("startLogOut")
          axios.logOut().then(Response=>{
            console.log(Response);
            if(Response.data.code==200 && Response.data.message=="success")
            {
              //成功
              //this.errHint="Success!";
              this.$Message.success('登出成功!')
              this.$router.push("/index");
              cookie.delCookie('userID')
              return
            }
            else{
              this.$Message.error("连接服务器出错")
            }
          });
        } catch (e) {
          return {
            result: false,
            errMsg: "Can't connect with server"
          };
        }
      },
      // getAt(){
      //   try{
      //     console.log("艾特")
      //       this.queryUnreadAt().then((response)=>{
      //         console.log("艾特數量", response)
      //         if(response.data.code==200&&response.data.message=="success"){
      //           console.log("設置mentionedCount")
      //           this.mentionedCount=response.data.data
      //         }
      //       })
      //   }
      //   catch (e) {
      //     console.log("cannot connect to server!")
      //   }
      // }
    },
  }
</script>

<style>
  #app{
    font-family: "Helvetica Neue",Helvetica,"PingFang SC","Hiragino Sans GB","Microsoft YaHei","微软雅黑",Arial,sans-serif;
  }
  .navBar{
    height: 60px;
    position: fixed;
    top:0;
    left:0;
    width: 100%;
    border-bottom: 1px solid #e6ecf0;
  }
  /*.ivu-input:hover{*/
  /*  border-color: #4000FF;*/
  /*}*/
  .ivu-btn{
    font-weight: 500;
    border-radius: 0;
    font-size:medium;
  }
  .ivu-btn-primary{
    color:#FDFDFE;
    background-color: #ff136b;
  }
  .ivu-btn-primary:hover{
    background-color: #0C0C0C;
  }
  .ivu-btn, .ivu-btn:active, .ivu-btn:focus {
    border:none;
    box-shadow: none;
  }
  .menuIcon{
    font-size: 20px;
    margin-top: 18px;
    margin-right: 0;
    float:left;
  }
  .menuWords{
    float: left;
    font-size: 15px;
    margin-left: 5%;
    display: inline;
  }
  .over{
    width:120%;
    display: inline-block;
    height: 100%;
    overflow: hidden;
  }
</style>
