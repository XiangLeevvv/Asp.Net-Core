<template>
    <VeHistogram :data="chartData" :settings="chartSettings" :extend="extend"></VeHistogram>
</template>
<script>
import VeHistogram from 'v-charts/lib/histogram.common'
export default {
    name: "Histogram",
    data () {
        this.extend={
            series: {
                label: { show: true, position: "top" }
            }
        }
        return{}
    },
    components:{
        VeHistogram
    },
    props:["chartData","chartSettings"],
}
</script>