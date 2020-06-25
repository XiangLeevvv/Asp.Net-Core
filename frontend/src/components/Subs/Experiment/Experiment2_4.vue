<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>SQL预编译</h1></center>
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
    name: 'Experiment2_4',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-4",
            openNames:["2"],
            chartData:{
                columns: ['操作', '一次','一百次'],
                rows: [
                    { '操作': '无预编译', '一次': 7,'一百次':434},
                    { '操作': '预编译', '一次': 28,'一百次':218},
                ]
            },
            chartSettings:{
                // showLine:['时间']
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