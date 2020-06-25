<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>含顺序单一索引实验</h1></center>
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
    name: 'Experiment2_3_1_2',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-3-1-2",
            openNames:["2","2-3","2-3-1"],
            chartData:{
                columns: ['索引', '时间'],
                rows: [
                    { '索引': 'T树正序', '时间': 221},
                    { '索引': 'T树倒序', '时间': 213},
                    { '索引': '单纯排序(TimesTen)', '时间': 286},
                    { '索引': '散列正序', '时间': 301},
                    { '索引': '散列倒序', '时间': 301},
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