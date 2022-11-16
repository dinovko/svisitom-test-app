import Vue from "vue";
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from "axios";

import App from "./App.vue";
import router from "./router";
import store from "./store";
import RestPlugin from "./service/httpService";

Vue.config.productionTip = false;
Vue.use(ElementUI);
Vue.use(RestPlugin);
axios.defaults.baseURL = `${process.env.VUE_APP_BASE_URL}`;

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
