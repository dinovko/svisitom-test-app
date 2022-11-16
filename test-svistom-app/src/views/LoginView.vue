<template>
  <div>
    <el-form ref="form" :model="form" label-width="120px">
      <el-form-item label="Логин">
        <el-input v-model="form.username"></el-input>
      </el-form-item>
      <el-form-item label="Пароль">
        <el-input placeholder="Please input password" v-model="form.password" show-password></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="login">Войти</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import axios from 'axios'
import { mapMutations, mapState } from 'vuex'
export default {
  data(){
    return {
      form:{
        username:'',
        password:''
      },
    }
  },
  mounted(){
    if(this.isAuth) {
      this.$router.push('/');
    }
  },
  methods:{
    ...mapMutations(['setAuth']),
    login(){
      axios.post(`token`,this.form).then((resp)=>{
        sessionStorage.setItem('token',resp.data.token);
        this.setAuth({prop:'isAuth',v:true});
        axios.defaults.headers.common["Authorization"] = "Bearer " + resp.data.token;
        this.$router.push('/');
      })
      .catch((resp)=>{
        console.info(resp)
      })
    }
  },
  computed:{
    ...mapState({
      isAuth:state=>state.isAuth
    })
  }
}
</script>