<template>
  <div class="col-sm-7 mx-auto">
    <Category />
    <Card v-for="movie in movies" :element="movie" :key="movie.title"/>
  </div>
</template>

<script>


import CardComponent from "@/components/CardComponent";
import CategoryAndSearchComponent from "@/components/CategoryAndSearchComponent";
import axios from "axios";
import { mapState } from "vuex";

export default {
  name:"Movie",
  data() {
    return {
      cat: "",
      loading: false,
      selection: 1,
      movies: [],
    };
  },
  computed:
    mapState({
        selectedCategory: (state) => state.selectedCategory,
        searchText: (state) => state.searchText
    }),
  watch:{
    selectedCategory(newValue){
      if(newValue.normalize() === ""){
        this.fetchMovies();
      }else{
        this.fetchMoviesByCategory();
      }
    },
    searchText(newValue){
      if(newValue.normalize() === ""){
        this.fetchMovies();
      }else{
        this.fetchMoviesByTitlePart();
      }
    }

  },
  components: {
    Category: CategoryAndSearchComponent,
    Card: CardComponent
  },
  created() {
    this.fetchMovies();
  },
  methods:{
    fetchMoviesByTitlePart() {
      axios
          .get("http://localhost:7384/api/Movies/title/"+this.searchText)
          .then((result) => {
            this.movies = result.data;
          });

    },
    fetchMoviesByCategory() {
      axios
          .get("http://localhost:7384/api/Movies/category/"+this.selectedCategory)
          .then((result) => {
            this.movies = result.data;
          });
    },
    fetchMovies() {
      axios
          .get("http://localhost:7384/api/Movies/")
          .then((result) => {
            this.movies = result.data;
          });
    }
  }
};
</script>
<style>


</style>