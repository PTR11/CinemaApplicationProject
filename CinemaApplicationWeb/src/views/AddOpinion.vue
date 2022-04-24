<template>
  <div v-if="user">
    <ErrorCard :error-message="error"  v-if="error.length > 0" class="col-sm-6 mx-auto"/>
    <div class="card col-sm-5 m-2 mx-auto m-2 p-3 border-1 border-dark bg-warning text-dark" >

        <h3 class="">Opinion</h3>

        <div class="form-check">
          <input type="checkbox" class="form-check-input" id="exampleCheck1" v-model="anonymus">
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

    </div>
    <div class="mx-auto col-sm-6" v-else>
      <ErrorCard error-message="You need to login to add your opinion"/>
    </div>

</template>

<script>
import axios from "axios";
import {mapState} from "vuex";
import ErrorcardComponent from "@/components/ErrorcardComponent";

export default {
  name:"OpinionAdder",
  components:{
    ErrorCard: ErrorcardComponent
  },
  data() {
    return {
      rating: 0,
      description: "",
      anonymus: false,
      error: "",
    };
  },
  computed:
      mapState({
        user: (state) => state.user,
      }),
  methods: {
    addOpinion(){
      let response = {
        GuestId: this.user?.id,
        MovieId: this.$route.params.id,
        Anonymus: this.anonymus,
        Description: this.description,
        Ranking: this.rating
      };

      axios
          .post("http://localhost:7384/api/Opinions/", response, {withCredentials: true})
          .then((result) => {
            if(result.status === 200){
              this.$router.push({name: 'Movie', path:"/"+this.$route.params.id})
            }
          }).catch(() => {
            this.error = "You have to add comment message";
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
