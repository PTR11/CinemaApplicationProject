<template>
    <div class="card col-sm-5 m-2 mx-auto m-2 p-3 border-1 border-dark bg-warning text-dark">
            <h3 class="">Sign Up</h3>

            <div class="form-group">
                <label>User Name</label>
                <input type="text" v-model="user.UserName" class="form-control form-control-lg"/>
            </div>

            <div class="form-group">
              <label>Full Name</label>
              <input type="text" v-model="user.Name" class="form-control form-control-lg"/>
            </div>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" v-model="user.Email" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
              <label>Address</label>
              <input type="text" v-model="user.Address" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" v-model="user.Password" class="form-control form-control-lg" />
            </div>

            <div class="form-group">
              <label>Credit card number</label>
              <input type="text" v-model="user.CreditCardNumber" class="form-control form-control-lg" />
            </div>

            <button @click="signupUser" class="btn btn-dark btn-lg btn-block">Sign Up</button>

            <p class="forgot-password text-right pt-2">
                Already registered 
                <router-link :to="{name: 'Login'}">sign in?</router-link>
            </p>
    </div>
</template>

<script>
    import axios from "axios";

    export default {
        data() {
            return {
              user:{
                UserName: "",
                Name: "",
                Email: "",
                Address: "",
                Password: "",
                CreditCardNumber: "",
              }
            }
        },
      methods:{
          signupUser(){
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
                  window.location.href = response.data.headers[0].value[0];
                }
              }else if(response.data.statusCode === 400){
                alert(response.data.headers[0].value[0]);
                window.location.reload(true);
              }
            });

            axios
                .post("http://localhost:7384/api/Users/register/", this.user, {headers: headers})
                .then((result) => {
                  console.log(result)
                  this.programs = result.data;
                });
          }
      }
    }
</script>