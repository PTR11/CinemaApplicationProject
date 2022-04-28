<template>
  <div>
    <ErrorCard v-if="errorMessage || errors.length > 0" :error-message="errorMessage" :errors-list="errors" class="col-sm-6 mx-auto"/>
    <div class="card col-sm-6 m-2 mx-auto m-1 p-3 border-1 border-dark bg-warning text-dark">

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
    </div>
  </div>

</template>

<script>

    import axios from "axios";
    import {mapState} from "vuex";
    import ErrorcardComponent from "@/components/ErrorcardComponent";
    export default {
        name:"LoginComponent",
        components: {
          ErrorCard : ErrorcardComponent
        },
        data() {
            return {
              loggedInUser: {
                UserName: "",
                Password: ""
              },
              errors:[],
              errorMessage:"",
            }
        },
        computed:
          mapState({
            user: (state) => state.user
          }),
        methods:{
          loginUser(){

            axios
                .post(process.env.VUE_APP_API_ADDRESS+"/api/Users/login/", this.loggedInUser, {withCredentials: true})
                .then((result) => {
                    if(result.status === 200){
                      this.$router.push({name: 'Home', path:"/"})
                      this.$store.dispatch("setUser",result.data);
                    }
                }).catch((err) =>{
                  this.errors = [];
                  this.errorMessage = "";
                  if(err.response.data.errors){
                    this.errorMessage = "Something went wrong";
                    for(const error in err.response.data.errors){
                      console.log(error)
                      err.response.data.errors[error].forEach((element) =>{
                        this.errors.push(element)
                      });
                    }
                  }
                  if(err.response.data.loginError){
                    this.errorMessage = err.response.data.loginError[0]
                  }

                  console.log(err.response);
                });
          }
        }
    }
</script>