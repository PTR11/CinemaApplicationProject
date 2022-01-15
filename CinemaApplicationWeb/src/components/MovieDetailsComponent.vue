<template>

  <b-card img-src="https://placekitten.com/1000/300" img-alt="Card image" img-top class="col-sm-6 p-2 mx-auto m-1 bg-warning text-light">
    <b-card-title>
      {{movie.title}} ({{movie.length}} perc)
    </b-card-title>
    <b-card-sub-title>

    </b-card-sub-title>
    <br>
    <b-card-text>
      Rendező: {{movie.director}}
    </b-card-text>
    <br>
    <b-card-text>
      Szereplők: {{movie.actors}}
    </b-card-text>
    <br>
    <b-card-text>
      Leírás: {{movie.description}}
    </b-card-text>
  </b-card>

</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      id: 0,
      movie: {
        title: "asd",
        length: "120",
        description: "faszfaszfasz faszfaszfasz faszfaszfaszfaszfaszfasz faszfaszfaszfaszfaszfasz faszfaszfasz faszfaszfaszfaszfaszfasz faszfaszfaszfaszfaszfaszfaszfaszfasz",
        director: "",
      },
      times: ["08:00", "09:30", "11:45", "19:30", "20:00", "22:05","08:00", "09:30", "11:45", "19:30", "20:00", "22:05"],
    };
  },
  created: function () {
    this.fetchMovies();
  },
  methods: {
    fetchMovies() {
      axios
          .get("http://127.0.0.1:3000/api/movies/" + this.$route.params.id)
          .then((result) => {
            this.movie = result.data.movie;
            console.log(this.movie);
          })
          .catch();

    },
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