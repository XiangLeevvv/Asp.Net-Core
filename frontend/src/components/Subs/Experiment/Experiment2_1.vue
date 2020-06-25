<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>物化视图实验</h1></center>
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
    name: 'Experiment2_1',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-1",
            openNames:["2"],
            chartData:{
                columns: ['TimesTen修改次数', 'force', 'complete refresh', 'fast refresh'],
                rows: [
                    { 'TimesTen修改次数': '10', 'force': 120, 'complete refresh': 105, 'fast refresh': 34},
                    { 'TimesTen修改次数': '100', 'force': 131, 'complete refresh': 98, 'fast refresh': 44},
                    { 'TimesTen修改次数': '1000', 'force': 103, 'complete refresh': 91, 'fast refresh': 53}
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