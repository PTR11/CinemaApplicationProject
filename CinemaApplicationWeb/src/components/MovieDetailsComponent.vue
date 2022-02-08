<template>

  <b-card img-src="https://placekitten.com/1000/300" img-alt="Card image" img-top class="col-sm-6 p-2 mx-auto m-1 bg-warning text-dark">
    <b-card-title style="font-size: 40px">
      {{movie.title}} ({{movie.length}} perc)
    </b-card-title>
    <b-card-sub-title>

    </b-card-sub-title>
    <br>
    <b-card-text style="font-size: 20px">
      Rendező: {{movie.director}}
    </b-card-text>
    <br>
    <b-card-text style="font-size: 20px">
      Szereplők:
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

    <router-link :to="'/addOpinion/'+movie.id" tag="button" class="btn btn-dark mt-5 mb-2">Add Opinion</router-link>
    <Opinions :opinions="opinions"/>
  </b-card>

</template>
<script>
import OpinionsComponent from "@/components/OpinionsComponent";
import axios from "axios";
export default {
  name: 'MovieDetails',
  components:{
    Opinions:  OpinionsComponent
  },
  data() {
    return {
      movie: {},
      opinions:[],
    };
  },
  created: function () {
    this.fetchMovies();
  },
  methods: {
    fetchMovies() {
      axios
          .get("http://localhost:7384/api/Movies/" + this.$route.params.id)
          .then((result) => {
            this.movie = result.data;
          })
          .catch();
    },
    fetchOpinions(){
      axios
          .get("http://localhost:7384/api/Opinions/" + this.$route.params.id)
          .then((result) => {
            this.opinions.push(result.data);
          })
          .catch();
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