<style scoped>
.tweet-items {
  text-align: left;
  margin-top: 10px;
  width: 100%;
}
.load-more {
   font-size: 15px;
  font-weight: bold;
  color:#4000ff;
  text-align: center;
  margin-bottom: 20px;
  width: 100%;
}

.load-more:hover {
  cursor: pointer;
}
.no-more {
  width: 100%;
  text-align: center;
  margin-bottom: 20px;
  border-radius: 10px;
  font-weight: bold;
  color: #b9b9b9;
}
</style>




<template>
  <div style="width:100%">
    <div  v-for="item in items" v-bind:key="item.message_id">
      <twiitem
        v-bind:item="item"
        class="tweet-items"
        @likeTwi="doLike(item)"
        @follow="doFollow(item)"
        v-bind:isFollowing="isFollowing[item.message_sender_user_id]"
        @change_follow="change_follow($event,item)"
      ></twiitem>
      <Divider/>
    </div>
    <div v-if="ableShowMore" class="load-more" @click="loadMore()">加载更多...<spin v-if="spinShow"><Icon type="ios-loading" size=18 class="demo-spin-icon-load"></Icon>
                <div>请稍等...</div></spin></div>
    <div v-else class="no-more">没有更多了</div>
  </div>
</template>


<script>
import axios from "../../utils/axios";
import cookie from "../../utils/cookie"
import TwiItem from "./TweetSingle";
export default {
  name: "twitter-items",
  props: {
    type: "",
    //如果是topic就传入topic_id，search就search key，自己主页用户id，别人主页也是用户id
    info: ""
  },
  data() {
    return {
      items: [],
      twiDatas: [],
      userDatas: "",
      ableShowMore: true,
      isFollowing: new Object(),
      spinShow: false,
      burl: "http://localhost:14968/"
    };
  },
  computed:{
    userID: function(){
      return cookie.getCookie("userID")
    }
  },
  methods: {
    doLike(item) {
      item.likeByUser = !item.likeByUser;
    },
    doFollow(item) {
      item.followByUser = !item.followByUser;
    },

    loadMore() {
      //测试时把整个函数替换成/**/里的内容
      /*
            this.twiDatas=['{"message_transpond_message_id":4,"message_is_transpond":1,"message_sender_user_id":2,"message_id":1,"message_create_time":"2019-10-3","message_content":"啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg"],"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":0,"message_sender_user_id":3,"message_id":2,"message_create_time":"2019-3-2","message_content":"哦噢噢噢噢哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8086/userimg/1.jpg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":1,"message_sender_user_id":3,"message_id":3,"message_create_time":"2019-3-2","message_content":"哦","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8086/userimg/1.jpg","http://106.14.3.200:8090/bgimg.jpeg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":0,"message_sender_user_id":3,"message_id":4,"message_create_time":"2019-3-2","message_content":"我要发4张图片","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            ];
            this.generateData();
            */
      this.downloadData();
    },
    //下载数据
    downloadData() {
      this.spinShow = true;
      if (this.type == "explore") {
        axios.queryNewestMessage(
          this.items.length + 1,
          10
        ).then(Response => {
          this.$emit("stop_loading");
          this.twiDatas = Response.data.data;
          //console.log(this.twiDatas);
          this.generateData();
          this.spinShow = false;
        });
      } else if (this.type == "topic") {
        //console.log("id", this.info);
        if (this.info) {
          axios.queryMessagesContains(this.info, this.items.length + 1, 10).then(
            //this.queryFollowMessage(this.items.length + 1, 10).then(
            Response => {
              this.$emit("stop_loading");
              this.twiDatas = Response.data.data;
              //console.log(this.twiDatas);
              this.generateData();
              this.spinShow = false;
            }
          );
        }
      } else if (this.type == "home") {
        axios.queryFollowMessage(
          this.items.length + 1,
          10,
          this.userID
        ).then(Response => {

          this.$emit("stop_loading");
          console.log("结束获取")
          this.twiDatas = Response.data.data;
          this.generateData();
          this.spinShow = false;
        });
      } else if (this.type == "userhome") {
        if (this.info) {
          axios.queryMessagesOf(this.info, this.items.length + 1, 10).then(
            Response => {
              this.$emit("stop_loading");
              this.twiDatas = Response.data.data;
              this.generateData();
              this.spinShow = false;
            }
          );
        }
      } else if (this.type == "search") {
        if (this.info) {
          axios.search(this.info, this.items.length + 1, 10).then(Response => {
            this.$emit("stop_loading");
            this.twiDatas = Response.data.data;
            this.generateData();
            this.spinShow = false;
          });
        }
      }
    },
    //返回排序规则函数的函数
    rule(key) {
      return function(a, b) {
        // sort 默认接受a,b两个参数表示数组中的值
        var value1 = a[key];
        var value2 = b[key];
        if (value1 < value2) {
          return 1;
        } else {
          return -1;
        }
      };
    },
    //下载数据后解析数据
    generateData() {
      //如果没有数据或者没有数据了
      if (!this.twiDatas || this.twiDatas.length == 0) {
        this.ableShowMore = false;
        return;
      }
      //对twidatas根据时间排序
      console.log('看来吉安弗兰',this.twiDatas)
      this.twiDatas.sort(this.rule("message_create_time"));
      console.log(this.twiDatas)
      //取得当前保存的推特总数
      let twiCount = this.items.length;
      for (let i = 0; i < this.twiDatas.length; i++) {
        //还有一些属性需要自己去获取，包括是否被....以及用户的....
        let itemTemp = this.twiDatas[i];
        itemTemp.ifShowComment = false;
        itemTemp.comments = [];
        itemTemp.likeByUser = false;
        itemTemp.followByUser = null;
        itemTemp.comments = [];
        //可以先解析已有内容
        this.isFollowing[itemTemp.message_sender_user_id] = null;
            this.items.push(itemTemp);
      }
      //完成加入后清空twiDatas，必须有，否则验证出错
      this.twiDatas = [];
      //console.log("asdads",this.items[0]);
    },
    change_follow(event, item) {
      var k = JSON.parse(JSON.stringify(this.isFollowing));
      k[item.message_sender_user_id] = event;
      this.isFollowing = k;
      console.log(this.isFollowing)
      this.$emit("change_following", event, item.message_sender_user_id);
    },
    change_follow2(val, id) {
      if (typeof(this.isFollowing[id])!="undefined") {
          var k = JSON.parse(JSON.stringify(this.isFollowing));
          k[id] = val;
          this.isFollowing = k;
          console.log(k)

      }
    }
  },
  created() {
    //测试时把整个函数替换成/**/里的内容
    /*
        this.twiDatas=['{"message_transpond_message_id":4,"message_is_transpond":1,"message_sender_user_id":2,"message_id":1,"message_create_time":"2019-10-3","message_content":"啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg"],"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":0,"message_sender_user_id":3,"message_id":2,"message_create_time":"2019-3-2","message_content":"哦噢噢噢噢哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦哦","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8086/userimg/1.jpg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":1,"message_sender_user_id":3,"message_id":3,"message_create_time":"2019-3-2","message_content":"哦","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8086/userimg/1.jpg","http://106.14.3.200:8090/bgimg.jpeg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            '{"message_transpond_message_id":4,"message_is_transpond":0,"message_sender_user_id":3,"message_id":4,"message_create_time":"2019-3-2","message_content":"我要发4张图片","message_image_urls":["http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg","http://106.14.3.200:8090/bgimg.jpeg"],"collectnum":5,"message_comment_num":4,"message_transpond_num":34,"message_agree_num":60}',
                            ];
        this.generateData();
        */
    this.downloadData();
    var array = new Array(this.items);
    if (array.length < 10) {
      //this.ableShowMore = false
    }
  },
  watch: {
    info: function(nval, oval) {
      this.downloadData();
    }
  },
  components: {
    twiitem: TwiItem
  },
};
</script>
