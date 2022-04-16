<template>
  <div class="col-sm-7 mx-auto p-10">
    <ErrorCard v-if="!movies.length" :error-message="'No movies found today'"/>
    <Card v-else v-for="movie in movies" :element="movie" :key="movie.id" :site="'Main'" />

  </div>
</template>

<script>
  import CardComponent from "../components/CardComponent";
  import ErrorcardComponent from "@/components/ErrorcardComponent.vue";
  import axios from "axios";
  export default {
    name: 'Home',

    components: {
      Card : CardComponent,
      ErrorCard : ErrorcardComponent
    },

    data() {
      return {
        opinions: [
        {author: "Peter", result: 4.5, description: "This is a very good movie, you will like it", movie: "Star Wars"},
        {author: "John", result: 4.5, description: "This is a very good movie, you will like it", movie: "Lego Movie"},
        {author: "George", result: 4.5, description: "This is a very good movie, you will like it", movie: "Indiana Jones"},
        {author: "Steve", result: 4.5, description: "This is a very good movie, you will like it", movie: "Matrix"},
        ],
        movies: [],
      }
    },
    created() {
      this.fetchShows();
    },
    methods:{
      fetchShows(){
        axios
            .get("http://localhost:7384/api/Movies/today")
            .then((result) => {
              this.movies = result.data;
              console.log(this.movies);
              this.movies.forEach((m) => {
                var asd = "data:image/jpg;base64,"+m.image;
                m.image = asd;
              })
            });
      }
    }
  }
</script>
