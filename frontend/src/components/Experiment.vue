<template>
    <div class="layout">
        <NavigateMenu></NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div style="margin-top:25%;margin-left:auto;margin-right:auto; width:50%">
                    <center><h1>这是实验的结果展示页面，选择左边列表的项目即可通过可视化的方式查看实验结果</h1></center>
                </div>
            </Content>
        </Layout>
    </div>
</template>

<script>
import VeHistogram from 'v-charts/lib/histogram.common'
import NavigateMenu from './Subs/NavigateMenu'
export default {
    name: 'Experiment',
    components:{
        VeHistogram,
        NavigateMenu
    }
}
</script>

<style scoped>
.layout{
    /* border: 1px solid #d7dde4; */
    background: #f5f7f9;
    position: relative;
    border-radius: 4px;
    overflow: hidden;
}
</style>