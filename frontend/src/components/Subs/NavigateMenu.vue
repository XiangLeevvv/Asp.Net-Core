<template>
    <Sider :style="{position: 'fixed', height: '100vh', left: 0, overflow: 'auto'}">
        <Menu theme="dark" width="auto" :active-name="activeName" :open-names="openNames" accordion>
            <MenuItem name="0" to="/home">返回主页</MenuItem>
            <Submenu name="1">
                <template slot="title">
                    性能比较实验
                </template>
                <MenuItem name="1-1" to="/experiment1_1">查询功能</MenuItem>
                <MenuItem name="1-2" to="/experiment1_2">插入功能</MenuItem>
            </Submenu>
            <Submenu name="2">
                <template slot="title">
                    性能提升实验
                </template>
                <MenuItem name="2-1" to="/experiment2_1">物化视图</MenuItem>
                <Submenu name="2-2">
                    <template slot="title">
                        SQL语句优化
                    </template>
                    <MenuItem name="2-2-1" to="/experiment2_2_1">调整WHERE子句</MenuItem>
                    <MenuItem name="2-2-2" to="/experiment2_2_2">不使用‘*’</MenuItem>
                    <MenuItem name="2-2-3" to="/experiment2_2_3">用EXISTS</MenuItem>
                    <MenuItem name="2-2-4" to="/experiment2_2_4">使用Where</MenuItem>
                </Submenu>
                <Submenu name="2-3">
                    <template slot="title">
                        索引
                    </template>
                    <Submenu name="2-3-1">
                        <template slot="title">单一索引</template>
                        <MenuItem name="2-3-1-1" to="/experiment2_3_1_1">确定值</MenuItem>
                        <MenuItem name="2-3-1-2" to="/experiment2_3_1_2">有序值</MenuItem>
                    </Submenu>
                    <MenuItem name="2-3-2" to="/experiment2_3_2">多个索引</MenuItem>
                    <!-- <MenuItem name="2-3-3" to="/experiment2_3_3">并行索引</MenuItem> -->
                    <MenuItem name="2-3-4" to="/experiment2_3_4">维护索引</MenuItem>
                </Submenu>
                <MenuItem name="2-4" to="/experiment2_4">使用预编译</MenuItem>
                <MenuItem name="2-9" to="/experiment2_9">存储过程</MenuItem>
                <MenuItem name="2-5" to="/experiment2_5">数据库连接模式</MenuItem>
                <Submenu name="2-6">
                    <template slot="title">数据类型</template>
                    <MenuItem name="2-6-1" to="/experiment2_6_1">账号存储类型的影响</MenuItem>
                    <MenuItem name="2-6-2" to="/experiment2_6_2">贴子内容</MenuItem>
                </Submenu>
                <MenuItem name="2-7" to="/experiment2_7">改变block_size</MenuItem>
                <MenuItem name="2-8" to="/experiment2_8">系统全局区SGA调整</MenuItem>
            </Submenu>
            
        </Menu>
    </Sider>
</template>

<script>
export default {
    name: 'NavigateMenu',
    data(){
        return{
            // activeName:"",
            // openNames:[]
        }
    },
    props:["activeName","openNames"]
}
</script>