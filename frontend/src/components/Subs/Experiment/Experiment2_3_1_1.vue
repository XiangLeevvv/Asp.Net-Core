<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>单一索引实验</h1></center>
                </div>
                <h2>USER_ID建立索引</h2>
                <div class="graph">
                    <Histogram
                    :chartData="chartData"
                    :chartSettings="chartSettings">CONTENT
                    </Histogram>
                </div>
                <h2>CONTENT建立索引</h2>
                <div class="graph">
                    <Histogram
                    :chartData="chartData1"
                    :chartSettings="chartSettings1">
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
    name: 'Experiment2_3_1_1',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-3-1-1",
            openNames:["2","2-3","2-3-1"],
            chartData:{
                columns: ['索引', '时间'],
                rows: [
                    { '索引': '散列索引', '时间': 8},
                    { '索引': 'B树索引', '时间': 9},
                    { '索引': '无索引', '时间': 285},
                ]
            },
            chartSettings:{},
            chartData1:{
                columns: ['索引', '时间'],
                rows: [
                    { '索引': '散列索引', '时间': 2696},
                    { '索引': 'B树索引', '时间': 2603},
                    { '索引': '无索引', '时间': 2742},
                ]
            },
            chartSettings1:{}
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