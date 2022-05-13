<template>
  <div v-if="loading" class="text-center">
    <b-spinner style="width: 3rem; height: 3rem;"  variant="warning" label="Text Centered"></b-spinner>
  </div>
  <div v-else class="col-sm-7 mx-auto">
    <ErrorCard v-if="!error.length == 0" :error-message="error" />
    <div v-else>
      <Category />
      <Card v-for="movie in movies" :element="movie" :key="movie.title"/>
    </div>

  </div>
</template>

<script>

import ErrorcardComponent from "@/components/ErrorcardComponent";
import CardComponent from "@/components/CardComponent";
import CategoryAndSearchComponent from "@/components/CategoryAndSearchComponent";
import axios from "axios";
import { mapState } from "vuex";

export default {
  name:"Movie",
  data() {
    return {
      cat: "",
      loading: true,
      selection: 1,
      movies: [],
      error:""
    };
  },
  computed:
    mapState({
        selectedCategory: (state) => state.selectedCategory,
        searchText: (state) => state.searchText
    }),
  watch:{
    selectedCategory(newValue){
      console
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
    Card: CardComponent,
    ErrorCard : ErrorcardComponent
  },
  created() {
    this.fetchMovies();
  },
  methods:{
    fetchMoviesByTitlePart() {
      this.loading = true;
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Movies/title/"+this.searchText)
          .then((result) => {
            this.movies = result.data;
            this.loading = false;
            this.setMoviesImage();
          }).catch(() => {
        this.error = "Something went wrong in our side"
        this.loading = false;
      });

    },
    fetchMoviesByCategory() {
      this.loading = true;
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Movies/category/"+this.selectedCategory)
          .then((result) => {
            this.movies = result.data;
            this.loading = false;
            this.setMoviesImage();
          }).catch(() => {
        this.error = "Something went wrong in our side"
        this.loading = false;
      });
    },
    fetchMovies() {
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Movies/")
          .then((result) => {
            this.movies = result.data;
            this.setMoviesImage();
            this.loading = false;
          }).catch(() => {
            this.error = "Something went wrong in our side"
            this.loading = false;
          });
    },
    setMoviesImage(){
      this.movies.forEach((m) => {
        var asd = "data:image/jpg;base64,"+m.image;
        m.image = asd;
      })
    }
  }
};
</script>
<style>


</style>