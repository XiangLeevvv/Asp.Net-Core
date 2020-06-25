<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>系统全局区SGA调整</h1></center>
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
    name: 'Experiment2_8',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-8",
            openNames:["2"],
            chartData:{
                columns: ['PL/SQL个数', '共享SQL区重用率','数据字典缓冲区命中率'],
                rows: [
                    { 'PL/SQL个数': 'PL/SQL个数 14', '共享SQL区重用率': 0.995904322,'数据字典缓冲区命中率':0.951264321},
                    { 'PL/SQL个数': '30', '共享SQL区重用率': 0.99603434, '数据字典缓冲区命中率':0.950234532},
                    { 'PL/SQL个数': '60', '共享SQL区重用率': 0.984092382, '数据字典缓冲区命中率':0.9134545},     
                    { 'PL/SQL个数': '120', '共享SQL区重用率': 0.984034040, '数据字典缓冲区命中率':0.882334566},     
                    { 'PL/SQL个数': '150', '共享SQL区重用率': 0.98342526, '数据字典缓冲区命中率':0.830851096},                 
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