<template>
  <div class="col-sm-7 mx-auto p-10">


    <div v-if="loading" class="text-center">
      <b-spinner style="width: 3rem; height: 3rem;" variant="warning" label="Text Centered"></b-spinner>
    </div>
    <div v-else>
      <ErrorCard v-if="!error.length == 0" :error-message="error"/>
      <Card v-else v-for="movie in movies" :element="movie" :key="movie.id" :site="'Main'" />
    </div>

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
        movies: [],
        error:"",
        loading: true,
      }
    },
    created() {
      this.fetchShows();
    },
    methods:{
      fetchShows(){
        axios
            .get(process.env.VUE_APP_API_ADDRESS+"/api/Movies/today")
            .then((result) => {
              this.movies = result.data;
              console.log(this.movies);
              this.movies.forEach((m) => {
                var asd = "data:image/jpg;base64,"+m.image;
                m.image = asd;
              })
              if(this.movies.length === 0){
                this.error = 'No movies found today'
              }
              this.loading = false;
            }).catch(() =>{
          this.error = 'Something went wrong on our side'
          this.loading = false;
        });
      }
    }
  }
</script>
