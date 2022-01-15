<template>
  <div class="col-sm-7 mx-auto p-10">
    <Card v-for="movie in movies" :element="movie" :key="movie.title" :site="'Main'"/>
  </div>
</template>

<script>
  import CardComponent from "../components/CardComponent";
  export default {
    name: 'Home',

    components: {
      Card : CardComponent,
    },

    data() {
      return {
        opinions: [
        {author: "Peter", result: 4.5, description: "This is a very good movie, you will like it", movie: "Star Wars"},
        {author: "John", result: 4.5, description: "This is a very good movie, you will like it", movie: "Lego Movie"},
        {author: "George", result: 4.5, description: "This is a very good movie, you will like it", movie: "Indiana Jones"},
        {author: "Steve", result: 4.5, description: "This is a very good movie, you will like it", movie: "Matrix"},
        ],
        movies: [
          {
            title: "Star Wars",
            director: "George Lucas",
            description: "This is a good movie",
            length: 120,
            times: ["18:50","19:30"]
          },
          {
            title: "Star Wars",
            director: "George Lucas",
            description: "This is a good movie",
            length: 120,
            times: ["18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30","18:50","19:30"]
          },
          {
            title: "Star Wars",
            director: "George Lucas",
            description: "This is a good movie",
            length: 120,
            times: ["18:50","19:30"]
          }
        ],
      }
    }
  }
</script>
