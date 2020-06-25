<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>SQL语句优化：使用EXISTS替换DISTINCT</h1></center>
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
    name: 'Experiment2_2_3',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-2-3",
            openNames:["2","2-2"],
            chartData:{
                columns: ['操作', '时间'],
                rows: [
                    { '操作': 'SQL语句改进前', '时间': 1923},
                    { '操作': 'SQL语句改进后', '时间': 5}
                ]
            },
            chartSettings:{
                showLine:['时间']
            }
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