<style scoped>

.show-comment-div{
    float:left;
    width: 100%;
    margin:0px;
    padding-top: 20px;
    background-color: white;
    max-height: 600px;
    overflow: auto;
}
.send-comm-div{
  height: 40px;
    width:100%;
    /* margin-bottom: 20px; */
  background-color: #1DA1F2;
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  align-items: center;
}
.send-comm-input{
  /* margin-left: 55px; */
    width:80%;
    background-color: white;
}
.comm-avt-div{
    float: left;
    margin-left: 40px;
    margin-top: 15px;
    background-color: white;
    overflow: hidden;
}
.comm-useravt{
}
.comm-content-div{
    float: left;
    width:70%;
    margin-top:10px;
    margin-bottom: 10px;
    margin-left: 20px;
    background-color:white;
}
.comm-name-div{
    font-size: 16px;
    font-weight: bold
}
.comm-text-div
{
    word-break:break-all;
    font-size:14px;
    margin-bottom: 10px;
}
.comm-time-div
{
    font-size:10px;
    color: dimgrey;
    font-weight: bold
}

.item-divider{
    text-align: center;
    color: beige;
}

.no-more {
  margin-top:20px;
  width: 100%;
  text-align: center;
  margin-bottom: 20px;
  border-radius: 10px;
  font-weight: bold;
  color: #b9b9b9;
}

</style>

<template>
<div>
    <div class="show-comment-div" v-show="ifShowComment">
        <div class="send-comm-div">
            <!--<input type="text" style="width: 60%;margin-left:10%;margin-right:8%; margin-top:10px;" v-model="commTextToSend"/>-->
            <Input :rows="1" :maxlength="140" style="width: 60%;margin-left:10%;margin-right:8%;"  v-model="commTextToSend" type="textarea" placeholder="Enter something..."/>
            <Button type="primary" @click="sendComment()" style="background-color: white ;color: black; z-index:10;">发送</Button>
        </div>
        <div v-for="comm in comments">
            <div class="comm-avt-div">
                <Avatar src="static/timg.jpg" class="comm-useravt"></Avatar>
            </div>
            <div class="comm-content-div">
                <div class="comm-name-div">
                    {{comm.nickname}}
                </div>
                <div class="comm-text-div">
                    {{comm.comment_content}}
                </div>
                <div class="comm-time-div" >
                    {{comm.comment_create_time}}
                </div>
                
            </div>
          <Divider style="margin:0;"/>
        </div>

        <div v-if="comments.length==0" class="no-more" >
            目前还没有评论哦
        </div>

    </div>
</div>
</template>

<script>
export default {
  props:{
        ifShowComment:false,
        comments:{
            type:Array,
            default:[],
        }
    },
    data(){
        return {
            commTextToSend:""
        }
    },
    methods:{
        sendComment(){
            this.$emit("sendComm",this.commTextToSend);
            this.commTextToSend="";
        },
    },
}
        
</script>
