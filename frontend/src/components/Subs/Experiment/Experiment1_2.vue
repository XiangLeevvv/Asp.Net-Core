<template>
    <div class="layout">
        <NavigateMenu
        :active-name="activeName"
        :open-names="openNames">
        </NavigateMenu>
        <Layout :style="{marginLeft: '200px'}">
            <Content style="background-color:white">
                <div class="pre">
                    <center><h1>插入实验</h1></center>
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
    name: 'Experiment1_2',
    components:{
        Histogram,
        NavigateMenu
    },
    data(){
        return{
            activeName:"1-2",
            openNames:["1"],
            chartData:{
                columns: ['数据量级', 'Oracle','TimesTen'],
                rows: [
                    { '数据量级': '千级', 'Oracle': 25, 'TimesTen':420},
                    { '数据量级': '万级', 'Oracle': 313, 'TimesTen':3835 },
                    { '数据量级': '十万级', 'Oracle': 3593, 'TimesTen':32000 },
                    { '数据量级': '百万级', 'Oracle': 48377, 'TimesTen':341000 },
                    { '数据量级': '千万级', 'Oracle': 534000, 'TimesTen':3346000 }
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