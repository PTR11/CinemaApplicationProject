<template>
  <div class="col-sm-7 mx-auto">
    <div class="row my-3">
      <Movies />
    </div>
  </div>
</template>

<script>
import MoviesComponent from "../components/MoviesComponent.vue";

export default {
  data() {
    return {
      selectedCategory: "",
      categories: ["action", "sci-fi", "horror", "animation"],
    };
  },
  components: {
    Movies: MoviesComponent,
  },
};
</script>
