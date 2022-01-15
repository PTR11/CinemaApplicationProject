<template>
  <div class="mx-auto p-10">
    <div class="center"><h1 class="my-3 mx-auto">Movies</h1></div>
    <div
        class="bg-warning center mx-auto container justify-content-sm-center mb-5 p-2 border border-3">
      <div class="mx-auto font-weight-bold">Choose a category: </div>

      <select
          name="cars"
          id="cars"
          v-model="selectedCategory"
          style="float: left; width: 150px; border: 2px solid white; color: white; font-weight:bold; padding: 8px"
      >
        <option
            v-for="movie in categories"
            :value="movie"
            :key="movie"
            class="text-light"
        >
          {{ movie }}
        </option>
      </select>
      <div class="ml-3 font-weight-bold">or </div>

      <div style="float: left; margin-left: 10px;">
        <form class="form-inline">
          <input
              class="form-control mr-sm-2 col-sm-6 fasz"
              type="search"
              placeholder="Search"
              aria-label="Search"
          />
          <button class="btn btn-outline-light" type="submit">
            Search
          </button>
        </form>
      </div>

    </div>
    <Card v-for="movie in movies" :element="movie" :key="movie.title"/>
  </div>
</template>

<script>
import CardComponent from '../components/CardComponent.vue'



export default {
  components: {
    Card: CardComponent,
  },
  props: [
    'title'
  ],
  data() {
    return {
      loading: false,
      selection: 1,
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
    };
  },
  methods: {
    reserve () {
      this.loading = true
      setTimeout(() => (this.loading = false), 2000)
    },
  },
};
</script>
<style>
.center {
  margin: auto;
  display: flex;
  align-items: center;
}
</style>

<style scoped>

.headerSpan {
  font-weight: bold;
  text-decoration: underline;
}
</style>
