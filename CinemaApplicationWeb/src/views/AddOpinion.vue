<template>
  <div class="card col-sm-5 m-2 mx-auto m-2 p-3 border-1 border-dark bg-warning text-dark" >
    <div v-if="user">
        <h3 class="">Opinion</h3>

        <div class="form-group">
          <label>User Name</label>
          <input type="text" v-model="user.UserName" class="form-control form-control-lg"/>
        </div>


        <div class="form-check">
          <input type="checkbox" class="form-check-input" id="exampleCheck1" @change="isAnonim">
          <label class="form-check-label" for="exampleCheck1">Would you like to add anonymous comment? </label>
        </div>
        <br>

        <div class="form-group">
          <label>Comment message</label>
          <input type="email" v-model="description" class="form-control form-control-lg" />
        </div>

        <div class="form-group">
          <label>Rating</label>
          <b-form-rating variant="warning" class="border border-dark form-control form-control-lg" v-model="rating"></b-form-rating>
        </div>

        <button @click="addOpinion()" class="btn btn-dark btn-lg btn-block">Rate</button>
      </div>
      <div class="mx-auto justify-content-center text-danger" v-else>
        <h3 >You need to login to add opinion</h3>
      </div>
    </div>


</template>

<script>
import axios from "axios";
import {mapState} from "vuex";

export default {
  name:"OpinionAdder",
  data() {
    return {
      rating: 0,
      description: "",
      anonymus: false
    };
  },
  computed:
      mapState({
        user: (state) => state.user,
      }),
  methods: {
    isAnonim(){
      if(!this.anonymus){
        this.user.UserName="Anonymus";
        document.getElementById("userName").value = 'Anonymus';
        document.getElementById("userName").disabled = true;
        this.anonymus = !this.anonymus;
      }else{
        this.user.UserName="";
        document.getElementById("userName").value = '';
        document.getElementById("userName").disabled = false;
        this.anonymus = !this.anonymus;
      }
    },
    addOpinion(){
      let response = {
        GuestId: this.user?.id,
        MovieId: this.$route.params.id,
        Anonymus: this.anonymus,
        Description: this.description
      };

      axios
          .post("http://localhost:7384/api/Opinions/", response)
          .then((result) => {
            console.log(result);
          });
    }
  },
};
</script>

<style scoped>
.input-element {
    width: 300px !important;
    margin: auto !important;
}
</style>
