<style scoped>
.twi-text{
    font-size: 20px;
    word-break:break-all;
    width:100%;
    overflow:auto;
}
.at-text{
    color: rgb(167, 90, 45);
}
.at-text:hover{
    cursor: pointer;
}
.topic-text{
    color:rgb(83, 142, 170);
}
.topic-text:hover{
    cursor: pointer;
}

</style>

<template>
<div>
    <p v-html="htmlText" class="twi-text">
        {{htmlText}}
    </p>
</div>
</template>

<script>
/*
        <span v-if="text.type=='normal'" class="normal-text">
            {{text.content}}
        </span>
        <span v-if="text.type=='at'" class="at-text" @click="doAtToUserHome(text.id)">
            {{text.content}}
        </span>
        <span v-if="text.type=='topic'" class="topic-text" @click="doTopicToTopic(text.id)">
            {{text.content}}
        </span>
*/
export default {
    props:{
        fullText:String,
        ats:Array,
        topics:Array,
    },
    computed:{
        htmlText: {
            get:function () {
                return this.parseTwitter(this.fullText);
            },
            set: function (newValue) {
            }
        }
    },
    methods:{
        parseTwitter(twitter_content){
            // ats[i] {atName, atIds} topics[i] {topicId, topicName}
            twitter_content;
            // var topics = this.topics;
            // var ats = this.ats;
            // //console.log("解析前", topics, ats)
            // for(let i = 0; i < ats.length; i++){
            //     var re = new RegExp(ats[i].atName, "g");
            //     var atNameTripped = ats[i].atName;
            //     twitter_content = twitter_content.replace(re, ' <a href="/Zoom?visitor_id='+ ats[i].atIds + '" > ' + atNameTripped + ' </a> ');
            // }
            // for(let i = 0; i < topics.length; i++){
            //     var re = new RegExp(topics[i].topicName, "g");
            //     var topicNameTripped = topics[i].topicName.split("#")[1];
            //     twitter_content = twitter_content.replace(re, ' <a href="/Topic?topic_id='+ topics[i].topicId + '&topic_name=' + topicNameTripped + '" > #' + topicNameTripped + '# </a> ');
            // }
            return twitter_content;
        },
        doAtToUserHome(text){
            
        },
        // doTopicToTopic(text){
        //     this.$router.push({path:'/Topic', query: { topic_id:text.id }});
        // },
        solveText(){
            this.htmlText=this.parseTwitter(this.fullText);
            //console.log(this.parseTwitter(this.fullText))
            //alert(this.parseTwitter(this.fullText));
   
            //alert(this.fullText);
        }
    },
    created(){
        //console.log("fulltext",this.fullText);
        //console.log("this.ats",this.ats);
        this.solveText();
    },
    
}
        
</script>