<template>
    <div class="card col-sm-5 m-2 mx-auto m-1 p-3 border-1 border-dark bg-warning text-dark">
            <h3>Sign In</h3>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" v-model="user.UserName" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" v-model="user.Password" class="form-control form-control-lg" />
            </div>

            <button @click="loginUser" class="btn btn-dark btn-lg border border-1 border-dark btn-block border-rounded">Sign In</button>

            <p class="forgot-password text-right mt-2 mb-4">
                <router-link to="/forgot-password">Forgot password ?</router-link>
            </p>
    </div>
</template>

<script>
    import axios from "axios";

    export default {
        data() {
            return {
              user: {
                UserName: "",
                Password: ""
              },
              loggedInUser:{}
            }
        },
        methods:{
          setUser(id){
            axios
                .get("http://localhost:7384/api/Users/guest/"+id)
                .then((result) => {
                  this.loggedInUser = result.data;
                });
          },
          loginUser(){
            const headers = {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*',
              'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept, Authorization',
              'Access-Control-Allow-Methods': "GET, POST, PUT, DELETE, OPTIONS",
              'Access-Control-Allow-Credentials': true
            };
            axios.interceptors.response.use((response) => {
              console.log(response);
              if(response.data.statusCode === 302){
                if(response.data.headers[0].value[0]){
                  alert(response.data.headers[1].value[0]);
                  //window.location.href = response.data.headers[0].value[0];
                }
              }else if(response.data.statusCode === 400){
                alert(response.data.headers[0].value[0]);
                window.location.reload();
              }
            });

            axios
                .post("http://localhost:7384/api/Users/login/", this.user, {headers: headers})
                .then((result) => {
                  this.programs = result.data;
                });
          }
        }
    }
</script>