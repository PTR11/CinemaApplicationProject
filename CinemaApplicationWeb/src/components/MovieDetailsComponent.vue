<template>
  <div>
    <ErrorCard :error-message="error" class="col-sm-6 mx-auto" v-if="error.length > 0"/>
    <b-card :img-src="movie.image" img-alt="Card image" img-top class="col-sm-6 p-2 mx-auto m-1 bg-warning text-dark border border-dark">
      <b-card-title style="font-size: 32px">
        {{movie.title}}
        <br>
        ({{movie.length}} perc)
      </b-card-title>
      <b-card-sub-title>

      </b-card-sub-title>
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

      <router-link :to="'/addOpinion/'+movie.id"  tag="button" class="btn btn-dark mt-5 mb-2">Add Opinion</router-link>
      <Opinions :opinions="opinions[0]"/>
    </b-card>
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
    };
  },
  created: function () {
    this.fetchMovies();
    this.fetchOpinions()
  },
  methods: {
    fetchMovies() {
      axios
          .get("http://localhost:7384/api/Movies/" + this.$route.params.id)
          .then((result) => {
            this.movie = result.data;
            var asd = "data:image/jpg;base64,"+this.movie.image;
            this.movie.image = asd;
          })
          .catch((err) => {
            console.log(err);
            this.error = "Something went wrong with fetching movie details";
          });
    },
    fetchOpinions(){
      axios
          .get("http://localhost:7384/api/Opinions/" + this.$route.params.id)
          .then((result) => {
            this.opinions.push(result.data);
          })
          .catch((err) => {
            console.log(err);
            this.error = "Something went wrong with fetching movie details";
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