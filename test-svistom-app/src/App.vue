<template>
  <div id="app">
    <nav>
      <router-link to="/">Главная</router-link> |
      <router-link to="/login">{{isAuth ? 'Выход':'Вход'}}</router-link>|
      <router-link to="/table">Таблица</router-link>
    </nav>
    <router-view />
  </div>
</template>

<script>
import { mapMutations, mapState } from "vuex";
export default {
  components: {},
  data() {
    return {
    };
  },
  methods: {
    ...mapMutations(["setAuth"]),
    logout() {
      sessionStorage.clear();
      this.setAuth({ prop: "isAuth", v: false });
      axios.defaults.headers.common["Authorization"] = null;
      this.$router.push("/");
    },
    computed: {
      ...mapState({
        isAuth: (state) => state.isAuth,
      }),
    },

  },
  mounted() {
  }
};
</script>
<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
