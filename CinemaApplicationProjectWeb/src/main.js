import 'bootstrap-css-only/css/bootstrap.min.css'
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import vuetify from './plugins/vuetify'
import Vuetify from 'vuetify/lib/framework';
import 'bootstrap/dist/css/bootstrap.css';
import store from './store'
import VueCookies from 'vue-cookies'
Vue.use(Vuetify);
Vue.use(VueCookies);
Vue.use(BootstrapVue)
Vue.use(BootstrapVueIcons)
Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
