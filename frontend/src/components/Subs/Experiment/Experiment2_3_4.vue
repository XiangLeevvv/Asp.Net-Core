<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>维护索引实验</h1></center>

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
    name: 'Experiment2_3_4',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-3-4",
            openNames:["2","2-3"],
            chartData:{
                columns: ['索引', '代价', '业务操作时间'],
                rows: [
                    { '索引': '不维护索引', '代价': 0,'业务操作时间':63},
                    { '索引': '索引重建rebuild', '代价': 312720, '业务操作时间':10},
                    { '索引': '索引合并coalesce', '代价': 201448, '业务操作时间':10}                
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