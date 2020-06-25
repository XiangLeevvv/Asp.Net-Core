<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>账号存储类型</h1></center>
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
    name: 'Experiment2_6_1',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-6-1",
            openNames:["2","2-6"],
            chartData:{
                columns: ['类型', '查询时间','存储大小'],
                rows: [
                    { '类型': 'CHAR', '查询时间': 63,'存储大小':1218},
                    { '类型': 'NUMBER', '查询时间': 25, '存储大小':1039},          
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