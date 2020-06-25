<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>数据库连接模式</h1></center>
                </div>
                <div class="graph">
                    <Histogram
                    :chartData="chartData"
                    :chartSettings="chartSettings">
                    </Histogram>
                </div>
                <div class="after">

                </div>
            </Content>
        </Layout>
    </div>
</template>

<script>
import Histogram from "../Histogram"
import NavigateMenu from '../NavigateMenu'
export default {
    name: 'Experiment2_5',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-5",
            openNames:["2"],
            chartData:{
                columns: ['session数量', '专业服务器模式','共享服务器模式'],
                rows: [
                    { 'session数量': 'session数量100', '专业服务器模式': 5,'共享服务器模式':0},
                    { 'session数量': 'session数量1000', '专业服务器模式': 7, '共享服务器模式':40}       
                ]
            },
            chartSettings:{}
        }
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