<template>
    <div class="card col-sm-5 m-2 mx-auto m-1 p-3 border-1 border-dark bg-warning text-dark">
            <h3>Sign In</h3>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" v-model="loggedInUser.UserName" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" v-model="loggedInUser.Password" class="form-control form-control-lg" />
            </div>

            <button @click="loginUser" class="btn btn-dark btn-lg border border-1 border-dark btn-block border-rounded">Sign In</button>

            <p class="forgot-password text-right mt-2 mb-4">
                <router-link to="/forgot-password">Forgot password ?</router-link>
            </p>
    </div>
</template>

<script>

    import axios from "axios";
    import {mapState} from "vuex";
    export default {
        name:"LoginComponent",
        data() {
            return {
              loggedInUser: {
                UserName: "",
                Password: ""
              }
            }
        },
        computed:
          mapState({
            user: (state) => state.user
          }),
        methods:{
          loginUser(){

            axios
                .post("http://localhost:7384/api/Users/login/", this.loggedInUser, {withCredentials: true})
                .then((result) => {
                    console.log(result.headers);
                    this.$store.dispatch("setUser",result.data);
                    if(result.status === 200){
                      this.$router.push({name: 'Home', path:"/"})
                      this.$store.dispatch("setUser",result.data);

                    }
                });
          }
        }
    }
</script>