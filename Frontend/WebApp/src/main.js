// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import Axios from 'axios';

Vue.config.productionTip = false

Axios.defaults.baseURL = process.env.API_ENDPOINT;

Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons)
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
