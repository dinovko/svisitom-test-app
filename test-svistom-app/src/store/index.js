import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isAuth: false,
    userName: null,
  },
  getters: {},
  mutations: {
    setAuth(state,payload){
      state[payload.prop] = payload.v;
    }
  },
  actions: {},
  modules: {},
});
