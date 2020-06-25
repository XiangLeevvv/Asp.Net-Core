<template>
  <div class="fs-split">
    <loadingAnimate v-show="loading" class="center-fix"></loadingAnimate>
    <div class="split-image">
      <img src="static/WELOG.png">
    </div>
    <div class="split-content">
      <div class="split-content-vertically-center">
        <div class="split-intro">
          欢迎来到 Welog!
        </div>
        <div class="split-detail">
          <p>我们在这里分享和记录我们的生活！祝您拥有美好的一天！</p>
        </div>
        <Button @click="login = true" size="large" type="text">登录</Button>
        <Drawer
          title="LOGIN"
          v-model="login"
          width="720"
          :mask-closable="false"
          :styles="styles"
        >
          <div class="login-input">
            <div class="label">
              用户名
            </div>
            <input type="text" placeholder="用户名" v-model="usernameL" @keyup.enter="loginEventHandeler" maxlength="10"></input>
            <div class="label">
              密码
            </div>
            <input type="password" placeholder="密码" v-model="passwordL" @keyup.enter="loginEventHandeler" maxlength="20"></input>
          </div>
          <div class="drawer-footer">
            <Button size="large" style="margin-right: 8px" @click="login = false" type="text">取消</Button>
            <Button @click="loginEventHandeler" size="large" type="primary">登录</Button>
          </div>
        </Drawer>
        <Button @click="register = true" size="large" type="primary">注册</Button>
        <Drawer
          title="REGISTER"
          v-model="register"
          width="720"
          :mask-closable="false"
          :styles="styles"
        >
          <div class="login-input">
            <div class="label">
              用户名
            </div>
            <input type="text" placeholder="用户名" v-model="usernameR" @keyup.enter="registEventHandler" maxlength="10"></input>
            <div class="label">
              密码
            </div>
            <input type="password" placeholder="密码" v-model="passwordR" @keyup.enter="registEventHandler" maxlength="20"></input>
            <div class="label">
              确认密码
            </div>
            <input type="password" placeholder="确认密码" v-model="confirmPassword" @keyup.enter="registEventHandler" maxlength="20"></input>
          </div>
          <div class="drawer-footer">
            <Button size="large" style="margin-right: 10px" @click="register = false" type="text">取消</Button>
            <Button @click="registEventHandler" size="large" type="primary">注册</Button>
          </div>
        </Drawer>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from '../utils/axios'
  import cookie from '../utils/cookie'
  import loadingAnimate from "./animate/loading"
    export default {
        name: "Start",
        data () {
        return {
          login: false,
          register:false,
          loading:false,
          usernameL:null,
          passwordL:null,
          usernameR:null,
          passwordR:null,
          confirmPassword:null,
          styles: {
            height: 'calc(100% - 55px)',
            overflow: 'auto',
            paddingLeft:'50px',
            position: 'static',
            display:'flex',
            alignItems:'center'
          }
        }
      },
      components:{
        loadingAnimate
      },
      methods: {
        async loginEventHandeler () {
          this.loading=true;
          try {
            console.log("start")
            let data = {
              name: this.usernameL,
              password: this.passwordL
            }
            axios.signIn(data).then(Response=>{
              console.log(Response);
              console.log(Response.data.code)
              console.log(Response.data.message)
              if(Response.data.code==200 && Response.data.message=="login success")
              {
                // this.checkLogin().then(Response=>{
                //   console.log('aa',Response)
                // })
                console.log("OK")
                this.loading=false
                this.$Message.success('登陆成功!')
                var i = Response.data.data.user_id
                cookie.setCookie("userID", i)
                console.log(document.cookie)
                this.$router.push("/home");
              }
              else if(Response.data.code==200 && Response.data.message=="wrong username or password")
              {
                this.loading=false
                this.$Message.error('用户名或密码错误。')
              }
              else if(Response.data.code==200 && Response.data.message=="username exists")
              {
                this.loading=false
                this.$Message.error('你的用户名已经被使用。')
              }
              else{
                this.loading=false
                this.$Message.error("连接服务器出错。")
              }
            });
          } catch (e) {
            this.loading=false
            return {
              result: false,
              errMsg: "Can't connect with server"
            };
          }
        },
        async registEventHandler() {
          console.log("regest Event start")
          this.loading=true;
          var regUsername=/^[a-zA-Z0-9\u4e00-\u9fa5]{1,10}$/
          var regPassword=/^[0-9a-zA-Z_#@!\?\-\\]{6,20}$/
          if(!regUsername.test(this.usernameR))
          {
            this.loading=false;
            this.$Message.error("用户名不合法。")
            return;
          }
          else if(!regPassword.test(this.passwordR))
          {
            this.loading=false;
            this.$Message.error("密码应大于6个字符，且不含特殊符号。");
            return;
          }
          else if(this.passwordR!==this.confirmPassword)
          {
            this.loading=false;
            this.$Message.error("两次输入的密码不匹配。");
            return;
          }
          try {
            console.log("start")
            let data = {
              name: this.usernameR,
              password: this.passwordR
            }
            console.log(data);
            axios.register(data).then(Response=>{
              console.log(Response.data);
              if(Response.data.code==200 && Response.data.message=="success")
              {
                this.$Message.success('注册成功!')
                this.loading=false
                this.register=false
                this.login=true
              }
              else if(Response.data.code==200 && Response.data.message=="Your name is used")
              {
                this.loading=false
                this.$Message.error("你的名字已被使用了。")
              }
              else{
                this.loading=false
                this.$Message.error("连接服务器出错。")
              }
            });
          } catch (e) {
            this.loading=false;
            return {
              result: false,
              errMsg: "Can't connect with server"
            };
          }
        }
      }
    }
</script>

<style>
.center-fix{
	position: fixed;/*固定位置*/
	z-index:99;/*设置优先级显示，保证不会被覆盖*/
  margin:auto;
  left:0;
  right:0;
  top:0;
  bottom:0;
}
  .fs-split {
    width: 100vw;
    height: 100vh;
    display: flex;
  }
  .fs-split .split-image{
    width:50%;
    padding-top: 15%;
    text-align: center;
  }
  .fs-split .split-image img{
    width: 93%;
    transition: transform .3s linear;
  }
  .fs-split .split-image img:hover{
    transform: scale(1.1);
  }
  .fs-split .split-content{
    width:50%;
    height: 100%;
    display:flex;
  }
  .fs-split .split-content .split-content-vertically-center {
    padding: 80px;
    margin-top: auto;
    margin-bottom: auto;
  }
  .split-intro{
    font-weight:bold;
    font-size: 38px;
    line-height: 50px;
    letter-spacing: 1px;
    margin-bottom: 30px;
    color:#0C0C0C;
    transition: color .4s linear;
  }
  .split-detail{
    padding: 40px 0 50px 0;
  }
  .split-detail p{
    font-size: 20px;
    color:#0C0C0C;
  }
  .split-intro:hover{
    color: #4000FF;
  }
  .ivu-btn{
    font-weight: 600;
    letter-spacing: 1px;
  }
  .ivu-btn-text{
    color:#ff136b;
  }
  .ivu-btn-primary{
    color:#FDFDFE;
    background-color: #4000FF;
  }
  .ivu-btn-text:hover{
    color:#0C0C0C;
    background-color: transparent;
  }
  .ivu-btn-primary:hover{
    background-color: #0C0C0C;
  }
  .ivu-btn-large {
    font-size: 20px;
    border-radius: 0px;
    margin-right: 20px;
  }
  .ivu-btn, .ivu-btn:active, .ivu-btn:focus {
    border:none;
    box-shadow: none;
  }
  .ivu-drawer-header{
    background-color: #0C0C0C;
  }
  .ivu-drawer-header{
    padding: 8px 16px;
  }
  .ivu-drawer-header-inner{
    color:#fff;
    height:30px;
    line-height: 30px;
    font-size: 22px;
    font-weight:500;
    letter-spacing: 1px;
  }
  .ivu-drawer-close{
    top:0px;
  }
  .ivu-drawer-content{
    width: 80%;
    right:0;
  }
  .ivu-drawer-close .ivu-icon-ios-close{
    /* color:#ff136b; */
    /* background-color: white; */
    font-size: 45px;
    /* font-weight: 600; */
  }
  .login-input input {
    width:25vw;
    padding: 4px;
    border-top:none;
    border-right: none;
    border-left:none;
    border-bottom: 3px solid #e6ecf0;
    font-size: 18px;
    line-height: 1.5;
    margin-bottom: 20px;
  }
  .login-input .label{
    font-size: 23px;
    font-weight:500;
    letter-spacing: 1px;
    color:#0C0C0C;
    margin-bottom: 10px;
  }
  .login-input input:focus {
    color: #0C0C0C;
    outline: none;
    transition: border-bottom-color .5s linear;
    border-bottom-color: #ff136b;
  }
  .drawer-footer{
    width: 100%;
    position: absolute;
    bottom: 0;
    left: 0;
    border-top: 2px solid #e6ecf0;
    padding: 8px 0px;
    text-align: right;
    background: #fff;
  }
</style>
