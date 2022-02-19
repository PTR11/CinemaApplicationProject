<template>
  <div class="col-sm-7 mx-auto p-10">
    <ErrorCard v-if="!movies.length" :error-message="'No movies found today'"/>
    <Card v-else v-for="movie in movies" :element="movie" :key="movie.title" :site="'Main'" />

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
        console.log("kukiii")
        console.log(this.$cookies.keys());
        console.log(document.cookie);
        axios
            .get("http://localhost:7384/api/Movies/today")
            .then((result) => {
              this.movies = result.data;
            });
      }
    }
  }
</script>
