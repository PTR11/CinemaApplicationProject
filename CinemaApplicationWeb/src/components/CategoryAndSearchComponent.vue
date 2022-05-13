<template>
  <div
      class="d-flex mx-auto bg-warning center container justify-content-center mb-5 p-2 border border-dark text-dark">
    <div>
      <label class="mr-2 text-dark">Choose a category:</label>
      <select
          name="cars"
          id="cars"
          v-model="category"
          class="col-sm-2 btn border border-dark text-dark mr-2 p-2 pt-1"
          @change="changeCategory()"
      >
        <option
            v-for="movie in categories"
            :value="movie"
            :key="movie"
            class="text-dark"
        >
          {{ movie }}
        </option>
      </select>
      or
      <div class="d-inline ml-2 my-auto form-inline">
        <input
            class="col-sm-3 btn form-control mr-sm-2 bg-warning border border-dark p-2 h-10"
            type="search"
            placeholder="Type here"
            aria-label="Search"
            v-model="search"
        />
        <button class="btn btn-outline-dark" @click="submitSearch()">
          Search
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import {mapState} from "vuex";

export default {
  data() {
    return {
      search: "",
      category:"",
      categories: [""],
    };
  },
  computed:
      mapState({
        selectedCategory: (state) => state.selectedCategory,
        searchText: (state) => state.searchText
      }),
  mounted() {
    this.fetchCategories();
    this.category = this.$store.getters.getCategory;
    this.search = this.$store.getters.getText;
  },
  methods: {
    fetchCategories(){
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Categories/all")
          .then((result) => {
            result.data.map(e => e.category).forEach(x => this.categories.push(x))
          });
    },
    changeCategory(){
      this.$store.dispatch("setCategory",this.category);
      this.search = "";
      this.$store.dispatch("setSearchText",this.search);
    },
    submitSearch(){
      this.$store.dispatch("setSearchText",this.search);
      this.category = ""
      this.$store.dispatch("setCategory",this.category);
    },
    reserve () {
      this.loading = true
      setTimeout(() => (this.loading = false), 2000)
    },
  },
};
</script>

