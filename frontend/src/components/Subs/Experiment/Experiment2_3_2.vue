<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>多个索引实验</h1></center>
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
    name: 'Experiment2_3_2',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"2-3-2",
            openNames:["2","2-3"],
            chartData:{
                columns: ['索引', '时间'],
                rows: [
                    { '索引': 'aB树&oB树', '时间': 32},
                    { '索引': 'aB树&o位图', '时间': 36},
                    { '索引': 'aB树&o散列', '时间': 36},
                    { '索引': 'a位图&oB树', '时间': 39},
                    { '索引': 'a位图&o位图', '时间': 38},
                    { '索引': 'a位图&o散列', '时间': 36},
                    { '索引': 'a散列&oB树', '时间': 43},
                    { '索引': 'a散列&o位图', '时间': 44},
                    { '索引': 'a散列&o散列', '时间': 40}
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