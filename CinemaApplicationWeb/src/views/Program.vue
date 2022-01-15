<template>
  <Program/>
</template>

<script>
import ProgramListComponent from "../components/ProgramListComponent";
export default {
  name: "Home",
  components: {
    Program: ProgramListComponent,
  },
  data() {
    return {
      selectedMovie: "",
      movies: [

      ],
      opinions: [
        {
          author: "John",
          result: 4,
          title: "Star Wars",
          description: "It was a very good movie, recommended for everyone",
          date: "2021. 03. 15",
        },
        {
          author: "Phil",
          result: 1,
          title: "Lego Movie",
          description:
              "I thought it's a document movie about the Lego company, but in the cinema, I should realise it's not.",
          date: "2021. 06. 32",
        },
        {
          author: "George",
          result: 5,
          title: "Star Wars",
          description:
              "It was awesome. I love Star Wars. May the force be with you",
          date: "2021. 04. 03",
        },
        {
          author: "John",
          result: 4,
          title: "Star Wars",
          description: "It was a very good movie, recommended for everyone",
          date: "2021. 03. 15",
        },
        {
          author: "Phil",
          result: 1,
          title: "Lego Movie",
          description:
              "I thought it's a document movie about the Lego company, but in the cinema, I should realise it's not.",
          date: "2021. 06. 32",
        },
        {
          author: "George",
          result: 5,
          title: "Star Wars",
          decription:
              "It was awesome. I love Star Wars. May the force be with you",
          date: "2021. 04. 03",
        },
        {
          author: "John",
          result: 4,
          title: "Star Wars",
          description: "It was a very good movie, recommended for everyone",
          date: "2021. 03. 15",
        },
        {
          author: "Phil",
          result: 1,
          title: "Lego Movie",
          description:
              "I thought it's a document movie about the Lego company, but in the cinema, I should realise it's not.",
          date: "2021. 06. 32",
        },
        {
          author: "George",
          result: 5,
          title: "Star Wars",
          description:
              "It was awesome. I love Star Wars. May the force be with you",
          date: "2021. 04. 03",
        },
        {
          author: "John",
          result: 4,
          title: "Star Wars",
          decription: "It was a very good movie, recommended for everyone",
          date: "2021. 03. 15",
        },
        {
          author: "Phil",
          result: 1,
          title: "Lego Movie",
          description:
              "I thought it's a document movie about the Lego company, but in the cinema, I should realise it's not.",
          date: "2021. 06. 32",
        },
        {
          author: "George",
          result: 5,
          title: "Star Wars",
          description:
              "It was awesome. I love Star Wars. May the force be with you",
          date: "2021. 04. 03",
        },
      ],
    };
  },
  created(){
    this.fetchMovieNames();
  },
  computed: {
    filterProductsByCategory: function() {
      return this.opinions.filter(
          (opinion) => opinion.movie === this.selectedMovie
      );
    },
  },
  methods: {
    fetchMovieNames: function() {
      this.movies = this.opinions.map(e => e.title);
    },

    onlyUnique: function(value, index, self) {
      return self.indexOf(value) === index;
    },
  },
};
</script>