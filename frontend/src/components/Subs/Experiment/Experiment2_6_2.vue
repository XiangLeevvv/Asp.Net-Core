<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>帖子内容存储类型</h1></center>
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
    name: 'Experiment2_6_2',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-6-2",
            openNames:["2","2-6"],
            chartData:{
                columns: ['类型', '查询时间','存储大小'],
                rows: [
                    { '类型': 'CHAR', '查询时间': 11,'存储大小':23126400},
                    { '类型': 'VARCHAR2(inline)', '查询时间': 12, '存储大小':22728448},
                    { '类型': 'VARCHAR2(out-of-line)', '查询时间': 14, '存储大小':22728448},                 
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