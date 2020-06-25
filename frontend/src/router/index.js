import Vue from 'vue'
import router from 'vue-router'
import Home from '@/components/Home'
import Start from '@/components/Start'
import zoom from '@/components/Zoom'
import Explore from '@/components/Explore'
import SearchResult from '@/components/SearchResult'
import Experiment from '@/components/Experiment'
import Experiment1_1 from '@/components/Subs/Experiment/Experiment1_1'
import Experiment1_2 from '@/components/Subs/Experiment/Experiment1_2'
import Experiment2_1 from '@/components/Subs/Experiment/Experiment2_1'
import Experiment2_2_1 from '@/components/Subs/Experiment/Experiment2_2_1'
import Experiment2_2_2 from '@/components/Subs/Experiment/Experiment2_2_2'
import Experiment2_2_3 from '@/components/Subs/Experiment/Experiment2_2_3'
import Experiment2_2_4 from '@/components/Subs/Experiment/Experiment2_2_4'
import Experiment2_3_1_1 from '@/components/Subs/Experiment/Experiment2_3_1_1'
import Experiment2_3_1_2 from '@/components/Subs/Experiment/Experiment2_3_1_2'
import Experiment2_3_2 from '@/components/Subs/Experiment/Experiment2_3_2'
import Experiment2_3_3 from '@/components/Subs/Experiment/Experiment2_3_3'
import Experiment2_3_4 from '@/components/Subs/Experiment/Experiment2_3_4'
import Experiment2_4 from '@/components/Subs/Experiment/Experiment2_4'
import Experiment2_5 from '@/components/Subs/Experiment/Experiment2_5'
import Experiment2_6_1 from '@/components/Subs/Experiment/Experiment2_6_1'
import Experiment2_6_2 from '@/components/Subs/Experiment/Experiment2_6_2'
import Experiment2_7 from '@/components/Subs/Experiment/Experiment2_7'
import Experiment2_8 from '@/components/Subs/Experiment/Experiment2_8'
import Experiment2_9 from '@/components/Subs/Experiment/Experiment2_9'
// import Topic from '@/components/Topic'
Vue.use(router)

export default new router({
  mode: 'history',
  routes: [
    {
      path: '/home',
      name: 'Home',
      component: Home,
      meta: {
        keepAlive: true
      }
    },
    {
      path: '/index', alias:'/',
      name: 'Start',
      component: Start,
      meta: {
        keepAlive: false
      }
    },
    {
      path:'/zoom',
      name:'zoom',
      component:zoom,
      meta:{
        keepAlive:true
      }
    },
    {
      path:'/explore',
      name:'Explore',
      component:Explore,
      meta:{
        keepAlive:true
      }
    },
    {
      path:'/searchResult',
      name:'SearchResult',
      component:SearchResult,
      meta:{
        keepAlive:true
      }
    },
    {
      path: '/experiment',
      name: 'Experiment',
      component: Experiment,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment1_1',
      name: 'Experiment1_1',
      component: Experiment1_1,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment1_2',
      name: 'Experiment1_2',
      component: Experiment1_2,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_1',
      name: 'Experiment2_1',
      component: Experiment2_1,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_2_1',
      name: 'Experiment2_2_1',
      component: Experiment2_2_1,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_2_2',
      name: 'Experiment2_2_2',
      component: Experiment2_2_2,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_2_3',
      name: 'Experiment2_2_3',
      component: Experiment2_2_3,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_2_4',
      name: 'Experiment2_2_4',
      component: Experiment2_2_4,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_3_1_1',
      name: 'Experiment2_3_1_1',
      component: Experiment2_3_1_1,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_3_1_2',
      name: 'Experiment2_3_1_2',
      component: Experiment2_3_1_2,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_3_2',
      name: 'Experiment2_3_2',
      component: Experiment2_3_2,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_3_3',
      name: 'Experiment2_3_3',
      component: Experiment2_3_3,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_3_4',
      name: 'Experiment2_3_4',
      component: Experiment2_3_4,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_4',
      name: 'Experiment2_4',
      component: Experiment2_4,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_5',
      name: 'Experiment2_5',
      component: Experiment2_5,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_6_1',
      name: 'Experiment2_6_1',
      component: Experiment2_6_1,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_6_2',
      name: 'Experiment2_6_2',
      component: Experiment2_6_2,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_7',
      name: 'Experiment2_7',
      component: Experiment2_7,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_8',
      name: 'Experiment2_8',
      component: Experiment2_8,
      meta: {
        keepAlive: false
      }
    },
    {
      path: '/experiment2_9',
      name: 'Experiment2_9',
      component: Experiment2_9,
      meta: {
        keepAlive: false
      }
    },
  ]
})

