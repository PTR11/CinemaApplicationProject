<template>
    <div class="card col-sm-5 m-2 mx-auto m-2 p-3 border-1 border-dark bg-warning text-dark">
            <h3 class="">Sign Up</h3>

            <div class="form-group">
                <label>User Name</label>
                <b-form-input type="text" v-model="user.UserName" class="form-control form-control-lg"/>
                <InputError :errors="Errors.UserName"/>
            </div>

            <div class="form-group">
              <label>Full Name</label>
              <input type="text" v-model="user.Name" class="form-control form-control-lg"/>
              <InputError :errors="Errors.Name"/>
            </div>

            <div class="form-group">
                <label>Email address</label>
                <input type="email" v-model="user.Email" class="form-control form-control-lg" />
                <InputError :errors="Errors.Email"/>
            </div>

            <div class="form-group">
                <label>Address</label>
                <input type="text" v-model="user.Address" class="form-control form-control-lg" />
                <InputError :errors="Errors.Address"/>
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" v-model="user.Password" class="form-control form-control-lg" />
                <InputError :errors="Errors.Password"/>
            </div>

            <div class="form-group">
              <label>Credit card number</label>
              <input type="text" v-model="user.CreditCardNumber" class="form-control form-control-lg" />
              <InputError :errors="Errors.CreditCardNumber"/>
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
    import InputErrorComponent from "@/components/InputErrorComponent";
    import headers from "@/headers";
    export default {
        name:"SignupComponent",
        components:{
          InputError: InputErrorComponent
        },
        data() {
            return {
              Errors:{
                UserName: [],
                Name: [],
                Email: [],
                Address: [],
                Password: [],
                CreditCardNumber: []
              },
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
              this.clearErrors();
              axios
                  .post("http://localhost:7384/api/Users/register/", this.user, {headers: headers})
                  .then((result) => {
                    console.log(result)
                    this.programs = result.data;
                  }).catch(err => {
                    this.AddErrors(err.response.data.errors)
                  });
          },
          AddErrors(errors){
            Object.keys(errors).forEach((errorName) => {
              switch (errorName){
                case "UserName":
                  errors.UserName.forEach((error) => {this.Errors.UserName.push(error)});
                  break;
                case "Name":
                  errors.Name.forEach((error) => {this.Errors.Name.push(error)});
                  break;
                case "Email":
                  errors.Email.forEach((error) => {this.Errors.Email.push(error)});
                  break;
                case "Address":
                  errors.Address.forEach((error) => {this.Errors.Address.push(error)});
                  break;
                case "Password":
                  errors.Password.forEach((error) => {this.Errors.Password.push(error)});
                  break;
                case "CreditCardNumber":
                  errors.CreditCardNumber.forEach((error) => {this.Errors.CreditCardNumber.push(error)});
                  break;
              }
            });
          },
          clearErrors(){
              this.Errors = {
                UserName: [],
                Name: [],
                Email: [],
                Address: [],
                Password: [],
                CreditCardNumber: []
            }
          }
      }
    }
</script>