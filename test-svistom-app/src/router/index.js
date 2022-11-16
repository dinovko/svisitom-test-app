import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "../views/HomeView.vue";
import store from "../store";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    meta: { authReq: false },
  },
  {
    path: "/login",
    name: "login",
    component: () => import("../views/LoginView.vue"),
    meta: { authReq: false },
  },
  {
    path: "/table",
    name: "table",
    component: () => import("../views/AboutView.vue"),
    meta: { authReq: true },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  const { authReq } = to.meta;
  const { isAuth } = store.state;
  if (authReq && isAuth) {
    next();
  } else if (authReq && !isAuth) {
    sessionStorage.clear();
    localStorage.clear();
    window.location.replace(process.env.VUE_APP_LOGOUT_URL)
  } else if (!authReq) {
    next();
  }
});

export default router;
