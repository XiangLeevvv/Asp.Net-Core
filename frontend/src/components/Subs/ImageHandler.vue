<style scoped>
div{
    margin: 0;
    padding: 0;
    border:none;
    overflow: hidden;
    box-sizing:border-box;
}
img{
  margin:0;
  padding:0;
  box-sizing:border-box;
}

.img-handler-div{
    width:100%;
    box-shadow: #ecedf3 0px 0px 4px;
    display: flex;
    border-radius: 10px;
}
.img-handler-div:hover {
  box-shadow: #cacee6 0px 0px 8px;
  cursor: zoom-in;
}

.img1-1-div{
  display: flex;
}
.img1-1 {
  width: 100%;
}

.img-div-for2{
  display:flex;
}
.img2-12-div{
    width:50%;
}
.img2-1{
}
.img2-2{
}

.img-div-for3{
  display: flex;
}
.img3-1-div {
}
.img3-1 {
}
.img3-23-div {
  display:flex;
  flex-direction: column;
}
.img3-23 {
}

.img-div-for4{
    display:flex;
}
.img4-1-div {
  overflow: hidden;
}
.img4-1 {

}
.img4-234-div {
  display: flex;
    flex-direction: column;
}
.img4-234 {
}
.cover {
  position: absolute;
  left: 0;
  top: 0;
  z-index: 9995;
  background-color: rgb(210, 220, 220);
}

.big-img {
  position: fixed;
  z-index: 9999;
  align-self: center;
}
</style>

<template>

<div>
    <Modal title="Preview" v-model="visible">
      <img style="width: 100%"   v-if="previewSrc.split('.')[1] == 'jpg' " v-bind:src="previewSrc" />
      <video
        style="width: 100%"
        v-else
        v-bind:src="previewSrc"
        controls="controls"
      />
    </Modal>
    
    <div class="img-handler-div" ref="wholediv" v-bind:style=" is_video ?  {} : {height:handlerHeight+'px'}" >
        <div class="img1-1-div" v-if="imgNum==1" v-bind:style=" is_video ?  {} : {height:handlerHeight+'px'}" >
          <img
            class="img1-1"
            v-if='!is_video'
            v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}"
            v-bind:src="imgData[0]"
            @click="doShowBigImg(0)"
            alt="1-1"
          />
          <video controls="controls" class="img1-1" v-else v-bind:src="imgData[0]"  alt="1-1" />
        </div>

        <div v-else-if="imgNum==2" v-bind:style="{height:handlerHeight+'px'}" class="img-div-for2">
            <img
              class="img2-1"
              v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}" ref="img1"
              v-bind:src="imgData[0]"
              @click="doShowBigImg(0)"
              alt="2-1"
            />
          
            <img
              class="img2-2"
              
              v-bind:style="{height:sizeH[1]+'px',width:sizeW[1]+'px'}" ref="img2"
              v-bind:src="imgData[1]"
              @click="doShowBigImg(1)"
              alt="2-2"
            />
            
        </div>

        <div class="img-div-for3" v-else-if="imgNum==3" v-bind:style="{height:handlerHeight+'px'}">
          <div class="img3-1-div" v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}">
            <img
              class="img3-1"
              v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}" ref="img1"
              v-bind:src="imgData[0]"
              @click="doShowBigImg(0)"
              alt="3-1"
            />
            
          </div>
          <div class="img3-23-div">
            <img
              class="img3-2"
              
              v-bind:style="{height:sizeH[1]+'px',width:sizeW[1]+'px'}" ref="img2"
              v-bind:src="imgData[1]"
              @click="doShowBigImg(1)"
              alt="3-2"
            />
            
            <img
              class="img3-3"
              
              v-bind:style="{height:sizeH[2]+'px',width:sizeW[2]+'px'}" ref="img3"
              v-bind:src="imgData[2]"
              @click="doShowBigImg(2)"
              alt="3-3"
            />
            
          </div>
        </div>


      <div class="img-div-for4" v-else-if="imgNum==4" v-bind:style="{height:handlerHeight+'px'}">
          <div class="img4-1-div" v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}">
            <img
              class="img4-1"
              
              v-bind:style="{height:sizeH[0]+'px',width:sizeW[0]+'px'}" ref="img1"
              v-bind:src="imgData[0]"
              @click="doShowBigImg(0)"
              alt="4-1"
            />
            
          </div>
          <div class="img4-234-div">
            <img
              class="img4-234"
              
              v-bind:style="{height:sizeH[1]+'px',width:sizeW[1]+'px'}" ref="img2"
              v-bind:src="imgData[1]"
              @click="doShowBigImg(1)"
              alt="4-2"
            />
            
            <img
              class="img4-234"
              
              v-bind:style="{height:sizeH[2]+'px',width:sizeW[2]+'px'}" ref="img3"
              v-bind:src="imgData[2]"
              @click="doShowBigImg(2)"
              alt="4-3"
            />
            
            <img
              class="img4-234"
              
              v-bind:style="{height:sizeH[3]+'px',width:sizeW[3]+'px'}" ref="img4"
              v-bind:src="imgData[3]"
              @click="doShowBigImg(3)"
              alt="4-4"
            />
            
          </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    twiId: Number,
    imgData: {
      type: Array,
      default: ["."]
    }
  },
  data() {
    return {
      previewSrc: "",
      visible: false,
      showBigImage: false,
      bigImgSource: "",
      coverHeight: "",
      coverWidth: "",
      //要展示的大图的长和宽
      bigImgHeight: "",
      bigImgWidth: "",
      //开始时就计算出小图的长和宽，第一张图片的长和宽分别保存在
      sizeW: [],
      sizeH: [],
      handlerWidth:0,
      handlerHeight:0,
      imgNum:0,
      is_video: false,
    };
  },
   watch:{
        handlerHeight(val){
            console.log("handlerheight变了");
            if(this.imgNum==1){
                this.sizeW[0]= this.handlerWidth;

                this.sizeH[0]= this.handlerHeight;
            }
            if(this.imgNum==2){
                this.sizeW[0]= this.handlerWidth*0.5;
                this.sizeW[1]= this.handlerWidth*0.5;

                this.sizeH[0]= this.handlerHeight;
                this.sizeH[1]= this.handlerHeight;
            }
            if(this.imgNum==3){
                this.sizeW[0]= this.handlerWidth*0.67;
                this.sizeW[1]= this.handlerWidth*0.33;
                this.sizeW[2]= this.handlerWidth*0.33;

                this.sizeH[0]= this.handlerHeight;
                this.sizeH[1]= this.handlerHeight*0.5;
                this.sizeH[2]= this.handlerHeight*0.5;
            }
            if(this.imgNum==4){
                this.sizeW[0]= this.handlerWidth*0.75;
                this.sizeW[1]= this.handlerWidth*0.25;
                this.sizeW[2]= this.handlerWidth*0.25;
                this.sizeW[3]= this.handlerWidth*0.25;

                this.sizeH[0]= this.handlerHeight*0.99;
                this.sizeH[1]= this.handlerHeight*0.33;
                this.sizeH[2]= this.handlerHeight*0.33;
                this.sizeH[3]= this.handlerHeight*0.33;
            }
            console.log("hhhhhhhhhh",this.sizeH);
            console.log("mmmmmmmm",this.sizeW);
        }
    },
    mounted(){
        if (this.imgData&&this.imgData.length){
                this.imgNum=this.imgData.length;
                this.handlerWidth=this.$refs.wholediv.offsetWidth;
                if (this.imgData.length==1){
                    this.handlerHeight=this.handlerWidth;
                }
                if (this.imgData.length==2) {
                    this.handlerHeight=0.5*this.handlerWidth;
                }
                if (this.imgData.length==3) {
                    this.handlerHeight=0.67*this.handlerWidth;
                }
                if (this.imgData.length==4) {
                    this.handlerHeight=0.75*this.handlerWidth;
            }
            console.log("mount里",this.handlerHeight);
            console.log("推特的圖片列表", this.twiId, this.imgData);
            if(this.imgData[0].split(".")[1] == "jpg"){
              this.is_video = false;
            }else{
              this.is_video = true;
            }
        }
    },
  
  methods: {
    doShowBigImg(imgNum) {
      this.visible = true;
      var imgSrc = this.imgData[imgNum];
      this.previewSrc = imgSrc;
    },
    closeBigImg(){
      this.visible=false;
    }
  },
  
}
</script>