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
.PostSenderContainer {
  width:100%;
  /*border: 1px solid #e6ecf0;*/
  background-color: #f6f6f6;
  padding: 10px 12px;
  display: flex;
}

.EditerContainer {
  position: relative;
  left: 32px;
  width: 80%;
  line-height: 20px;
}

.Editer {
  position: relative;
  width: 90%;
  color: #1DA1F2;
  background: #fff;
  border: 1px solid #C6E7FB;
  border-radius: 8px;
  padding: 8px;
  word-wrap: normal;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  font-size: 14px;
  letter-spacing: 0.03em;
  cursor: text;
}
.ivu-menu-horizontal {
  width:100%;
}
.Editer:focus {
  outline: none;
  height: 100px;
  max-height: none;
  word-wrap: break-word;
  overflow-y: scroll;
  white-space: normal;
}

.Editer:empty:focus:before {
  content: attr(default-txt);
  display: block;
  color: #aab8c2;
  position: relative;
}

.PostBtn {
  margin: 5px 200px;
  border: 1px solid #1da1f2;
  color: #fff;
  background-color: #4AB3F4;
  padding: 2px 12px;
  font-size: 14px;
  border-radius: 100px;
  display: block;
}

.PostBtn.hide {
  display: none
}

ul li{
  list-style-type: none;
}
  #root-div{
    position: fixed;
    height: 100%;
    width: 100%;
    overflow: auto;
  }
  #middle-container{
    width: 40%;
    /*background-color: white;*/
    margin-left: auto;
    margin-right: auto;
    margin-top: 70px
  }
  .infor-avatar{
    margin-top: 10px;
    margin-left: 15px;
    margin-bottom: 5px
  }



  /* -------------------------------------------------------------------------------- */
  .demo-upload-list{
        display: inline-block;
        width: 60px;
        height: 60px;
        text-align: center;
        line-height: 60px;
        border: 1px solid transparent;
        border-radius: 4px;
        overflow: hidden;
        background: #fff;
        position: relative;
        box-shadow: 0 1px 1px rgba(0,0,0,0.2);
        margin-right: 4px;
    }
    .demo-upload-list img{
        width: 100%;
        height: 100%;
    }
    .demo-upload-list-cover{
        display: none;
        position: absolute;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        background: rgba(0,0,0,.6);
    }
    .demo-upload-list:hover .demo-upload-list-cover{
        display: block;
    }
    .demo-upload-list-cover i{
        color: #fff;
        font-size: 20px;
        cursor: pointer;
        margin: 0 2px;
    }
    /* ----------------------------------------------------------------- */
</style>

<template >
  <div id='root-div'><div id="topAnchor"></div>

  <loadingAnimate v-show="loading" class="center-fix"></loadingAnimate>

    <UserInfo></UserInfo>
    <div id="middle-container">
     <ElContainer  id="middle-container1" >


        <div class="PostSenderContainer">
         <Avatar src="static/timg.jpg" shape="circle" on-error="" size="large" style="width:50px;height:50px;border-radius:50%;"/>

          <div class="EditerContainer" style="margin-left:-5px;">
            <!--
              <div class="Editer" default-txt="What happens?" contenteditable @click.prevent="clickEditor" v-bind:focus="isEditerFocused" @input="editerInputEventHandler">
                What happens?
              </div>-->
            <Input :ref="'editor'" :rows="editor_content.length > 0 ? 4 : 2" v-model="editor_content" v-bind:maxlength="140" type="textarea" placeholder="输入些什么..."
            @v-bind:focus="isEditerFocused" @focus="editerFocusEventHandler"  @blur="editerBlurEventHandler" />
            <!-----TODO:AddPicture--- ----------------------------------------------->
            <div style="margin-top:5px;">
            <div v-show="editor_content.length > 0" style="float:left;" >
              <div class="demo-upload-list" v-for="item in uploadList">
                <template>
                  <img v-show="item.is_img" :src="item.url">
                    <div class="demo-upload-list-cover">
                      <Icon type="ios-eye-outline" @click.native="handleView(item)"></Icon>
                      <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
                    </div>
                  <video v-show="item.is_video"  :src="item.url"/>
                    <div class="demo-upload-list-cover">
                      <Icon type="ios-eye-outline" @click.native="handleView(item)"></Icon>
                      <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
                    </div>
                </template>
              </div>
              <!-- <Upload
                ref="upload"
                :show-upload-list="false"
                :on-success="handleSuccess"
                :format="['jpg','jpeg','png','mp4']"
                :max-size="40960"
                :on-format-error="handleFormatError"
                :on-exceeded-size="handleMaxSize"
                :before-upload="handleBeforeUpload"
                multiple
                type="drag"
                action=""
                style="display: inline-block;width:58px;">
                  <div style="width: 58px;height:58px;line-height: 58px;">
                    <Icon type="ios-camera" size="20"></Icon>
                  </div>
              </Upload> -->
              </div>
              <!-- <Modal title="Preview" v-model="visible">
                <img :src="img_preview_src" v-if="visible && is_previewing_img" style="width: 100%">
                <video controls="controls" v-else-if="visible && is_previewing_video" id="video" style="width: 100%" :src="video_preview_src">
                </video>
              </Modal> -->
            </div>

            <!-- sdadasdasdasdsad ---------------------------------------------------------------------------->
            <Button type="primary" size="normal" :disabled="!editor_content.length" v-show="editor_content.length > 0" @click="sendPostBtnClickEventHandler" @focus="editerFocusEventHandler" @blur="editerBlurEventHandler" style="float:right;margin-top:10px;margin-right:20px;">发送 WeLog</button>
          </div>

        </div>


     </ElContainer>

     <ElContainer  id="middle-container2" >
       <!--
       <div style="padding-top:10%;
       padding-left:20%;
       padding-bottom:10%;
       padding-right:20%;">
         <span style= "font-weight:bold;font-size:26px;">What? No Tweets yet?</span><br>
         <span style= "font-size:16px;font-color:#657180;">This empty timeline won’t be around for long. Start following people and you’ll see Tweets show up here.</span>
         <br><br><br>
       </div>
       -->

      <tweets @stop_loading="stop_loading" type="home" style="width:100%;">
      </tweets>

     </ElContainer>
    </div>
        <backToTop></backToTop>
  </div>
</template>
<script>
  import axios from "../utils/axios"
  import loadingAnimate from "./animate/loading"
  import Tweets from "./Subs/Tweets"
  import UserInfo from "./Subs/userInfo"
  import cookie from "../utils/cookie"
  import backToTop from "./Subs/BackToTop"
  export default {
    name:'Home',

    data(){
      return{
        is_previewing_img: false,
        is_previewing_video: false,
        sendingTwitter: false,
        editor_content:"",
        visible:false,
        img_preview_src:"",
        video_preview_src:"",
        uploadList: [],
        loading:true,
        format:['jpg','jpeg','png','mp4'],
        sites: [
          { name: 'Runoob' },
          { name: 'Google' },
          { name: 'Taobao' }
        ],
        informationList:[
        ],
        isEditerFocused: false,
        contentEl: null,
        inputContent: '',
      }
    },
    computed:{
      userID: function(){
        return cookie.getCookie("userID")
      }
    },
    components:{
      loadingAnimate,
      "tweets":Tweets,
      UserInfo,
      backToTop
    },
    mounted() {
    var userID = cookie.getCookie("userID")
    console.log("登录：", userID)
    console.log(userID)
    axios.getUserPublicInfo(userID).then(Response=>{
    console.log(Response)
    if(Response.data.code==200 && Response.data.message=="success")
      {
        console.log(this.userName)
      }
      else{
        console.log("fail")
        this.userName="userName"
      }
    })
    },
    methods:{
      stop_loading(){
        console.log("隱藏loading")
        this.loading = false;
      },
      flashCom()
      {
        this.$router.go(0);
      },
      uploadTapped(){
        console.log("调用uploadTapped");
        this.isEditerFocused = true;
      },
      handleView (item) {
        console.log(item);
          if(item.is_img){
            this.img_preview_src = item.url;
            this.is_previewing_img = true;
            this.is_previewing_video = false;
          }else if(item.is_video){
            this.video_preview_src = item.url;
            this.is_previewing_video = true;
            this.is_previewing_img = false;
          }
          this.visible = true;
      },
      handleRemove (file) {
                // const fileList = this.$refs.upload.fileList;
                // this.$refs.upload.fileList.splice(fileList.indexOf(file), 1);
      },
      handleSuccess (res, file) {
                file.url = '';
                file.name = '';
      },
      handleFormatError (file) {
                this.$Notice.warning({
                    title: 'The file format is incorrect',
                    desc: 'File format of ' + file.name + ' is incorrect, please select jpg/png/jpeg/mp4.'
                });
      },
      handleMaxSize (file) {
                this.$Notice.warning({
                    title: 'Exceeding file size limit',
                    desc: 'File  ' + file.name + ' is too large, no more than 2M.'
                });
      },
      handleBeforeUpload (file) {
                console.log("上傳了文件", file);
                if(file.type.substr(0,5) == "image"){
                  file.is_img = true;
                  file.is_video = false;
                }else if(file.type.substr(0,5) == "video"){
                  file.is_video = true;
                  file.is_img = false;
                }

                if(this.format.indexOf(file.type.split("/")[1]) == -1){
                  console.log("格式不对")
                  this.$Notice.warning({
                    title: 'The file format is incorrect',
                    desc: 'File format of ' + file.name + ' is incorrect, please select jpg/png/jpeg/mp4.'
                  });
                  return false;
                }

                var _this = this;
                // if(file.is_video){
                //   if(_this.$refs.upload.fileList.length > 1){
                //     this.$Notice.warning({
                //       title: 'Up to single video can be uploaded'
                //     });
                //     return;
                //   }
                // }

                if(file.is_img){
                  var hasVideo = false;
                  // for(let i = 0; i < this.$refs.upload.fileList.length; i++){
                  //   if(this.$refs.upload.fileList[i].is_video){
                  //     hasVideo = true;
                  //   }
                  // }
                  if(hasVideo){
                    this.$Notice.warning({
                      title: 'Video and Image cannot be uploaded together'
                    });
                    return;
                  }

                }

                // const check =  this.$refs.upload.fileList.length < 4;
                if (!check) {
                    this.$Notice.warning({
                        title: 'Up to four items can be uploaded.'
                    });
                }else{
                  if(file.is_img){
                      let reader = new FileReader()
                      reader.readAsDataURL(file) // 这里是最关键的一步，转换就在这里
                      reader.onloadend = function () {
                        console.log("圖片解析完畢")
                      file.url = this.result
                      this.img_preview_src = file.src;
                      // _this.$refs.upload.fileList.push(file);
                    }
                  }else if(file.is_video){
                      var url = null ;
                      if (window.createObjectURL!=undefined) { // basic
                        url = window.createObjectURL(file) ;
                      } else if (window.URL!=undefined) { // mozilla(firefox)
                        url = window.URL.createObjectURL(file) ;
                      } else if (window.webkitURL!=undefined) { // webkit or chrome
                        url = window.webkitURL.createObjectURL(file) ;
                      }
                      file.url = url;
                      // _this.$refs.upload.fileList.push(file);
                  }

                }
                console.log("handleBeforeUpload");
                return false;
      },
      editerFocusEventHandler (e) {
        this.isEditerFocused = true
        this.contentEl = e.target
        console.log("Focus");
        if (e.target.innerText.trim() === e.target.getAttribute('default-txt')) {
          e.target.innerText = ''
        }
      },
      editerBlurEventHandler (e) {
      this.isEditerFocused = false
      console.log("Blur");
      if (!e.target.innerText.trim()) {
        e.target.innerText = e.target.getAttribute('default-txt')
      }
    },
    editerInputEventHandler (e) {
      this.inputContent = e.target.innerText.trim()
    },

    tapTopic(topic){
      console.log("测试点击 topic_id:", topic);
      this.$router.push({path:'/Topic', query: { topic_id:topic.topic_id,topic_name:topic.topic_content }})
      //TODO 点击热点之后跳转
    },

    sendPostBtnClickEventHandler(){
      this.sendingTwitter = true;
      console.log("点击发送推特", this.editor_content, this.uploadList);
      var formData = new FormData();
      formData.append("message_content", this.editor_content);
      formData.append("userID", this.userID)
      axios.sendMessage(formData).then(response=>{
        //this.sendingTwitter = false;
        console.log(response);
        if(response.data.message == "success"){
          this.editor_content = "";
          this.uploadList = [];
        }
        this.sendingTwitter = false;
        this.$router.go(0)
      })

    },

    captureImage() {
      var video = document.getElementById("video");
      var canvas = document.createElement("canvas");
      canvas.width = video.videoWidth * scale;
      canvas.height = video.videoHeight * scale;
      canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height)
      return canvas.toDataURL("image/png");
    }
    },
    // beforeRouteEnter(to,from,next){
    //   next(vm=>{
    //     if(!vm.cookie.getCookie("userID"))
    //     {
    //       console.log("请先登录")
    //       vm.$router.push("index")
    //     }
    //   })
    //}
  }
</script>
