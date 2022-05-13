<template>
  <div>
    <div v-if="loading" class="text-center">
      <b-spinner style="width: 3rem; height: 3rem;" variant="warning" label="Text Centered"></b-spinner>
    </div>
    <div v-else>
      <ErrorCard :error-message="error" class="col-sm-6 mx-auto" v-if="error.length > 0"/>
      <b-card v-else :img-src="movie.image" img-height="500" img-width="300"  img-alt="Card image" img-top class="col-sm-6 p-2 mx-auto m-1 bg-warning text-dark border border-dark">
        <b-card-title style="font-size: 32px">
          {{movie.title}}
          <br>
          ({{movie.length}} perc)
        </b-card-title>
        <br>
        <b-card-text style="font-size: 20px">
          Director: {{movie.director}}
        </b-card-text>
        <br>
        <b-card-text style="font-size: 20px">
          Actors:
          <ul>
            <li v-for="actor in movie.actors" :key="actor.name">
              {{actor.name}}
            </li>
          </ul>
        </b-card-text>
        <br>
        <b-card-text style="font-size: 20px">
          Leírás: <br>
          {{movie.description}}
        </b-card-text>
        <br>
        <b-embed
            v-if="movie.trailer"
            type="iframe"
            aspect="16by9"
            :src="movie.trailer"
            allowfullscreen
        ></b-embed>

        <router-link :to="'/addOpinion/'+movie.id"  tag="button" class="btn btn-dark mt-5 mb-2">Add Opinion</router-link>
        <Opinions :opinions="opinions[0]"/>
      </b-card>
    </div>

  </div>

</template>
<script>
import OpinionsComponent from "@/components/OpinionsComponent";
import axios from "axios";
import ErrorcardComponent from "@/components/ErrorcardComponent";
export default {
  name: 'MovieDetails',
  components:{
    Opinions:  OpinionsComponent,
    ErrorCard: ErrorcardComponent
  },
  data() {
    return {
      error: "",
      movie: {},
      opinions:[],
      loading: true
    };
  },
  created: function () {
    this.fetchMovies();
    this.fetchOpinions()
  },
  methods: {
    fetchMovies() {
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Movies/" + this.$route.params.id)
          .then((result) => {
            this.movie = result.data;
            if(result.data === undefined){
              this.error = "Something went wront with fetching";
            }
            console.log(this.movie)
            var asd = "data:image/jpg;base64,"+this.movie.image;
            this.movie.image = asd;
            this.loading = false;
          })
          .catch((err) => {
            console.log(err);
            this.error = "Something went wrong with fetching movie details.";
            this.loading = false;
          });
    },
    fetchOpinions(){
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Opinions/" + this.$route.params.id)
          .then((result) => {
            this.opinions.push(result.data);
          })
          .catch(() => {
            this.error = "Something went wrong with fetching movie opinions.";
            window.scrollTo(0,0);
          });
    }
  },
};
</script>
<style scoped>
.element {
  vertical-align: middle;
  color: white;
  width: 200px;
  height: 45px;
  margin: 0px;
  padding: 0px;
  text-decoration: none !important;
  background-color: none !important;
}
</style>