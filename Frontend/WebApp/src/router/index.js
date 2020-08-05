import Vue from 'vue'
import Router from 'vue-router'
import ManufacturerList from '@/components/ManufacturerList';
import ModelList from '@/components/ModelList';

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: ManufacturerList
    },
    {
      path: '/manufacturers',
      name: 'ManufacturerList',
      component: ManufacturerList
    },
    {
      path: '/manufacturers/:id',
      name: 'ModelList',
      component: ModelList
    }
  ]
})
