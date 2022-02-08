<template>
  <div class="overflow-auto" v-if="opinions.length > 0" >
      <div v-for="item in itemsForList" :key="item.id" class="card mb-2 border border-dark">
        <div class="card-body"  >
          <h5 class="card-title">{{item.first_name}}</h5>
          <h6 class="card-subtitle mb-2 text-muted">Card subtitle</h6>
          <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>

        </div>
    </div>
    <b-pagination
        v-model="currentPage"
        :total-rows="rows"
        :per-page="perPage"
        align="center"
    ></b-pagination>
  </div>
  <div v-else>
    <p style="font-size: 20px">No opinions</p>
  </div>
</template>

<script>

export default {
  name: "Opinions",
  props: ["opinions"],
  data() {
    return {
      perPage: 3,
      currentPage: 1,
      items: [
        {id: 1, first_name: 'Fred', last_name: 'Flintstone'},
        {id: 2, first_name: 'Wilma', last_name: 'Flintstone'},
        {id: 3, first_name: 'Barney', last_name: 'Rubble'},
        {id: 4, first_name: 'Betty', last_name: 'Rubble'},
        {id: 5, first_name: 'Pebbles', last_name: 'Flintstone'},
        {id: 6, first_name: 'Bamm Bamm', last_name: 'Rubble'},
        {id: 7, first_name: 'The Great', last_name: 'Gazzoo'},
        {id: 8, first_name: 'Rockhead', last_name: 'Slate'},
        {id: 9, first_name: 'Pearl', last_name: 'Slaghoople'},{id: 11, first_name: 'The Great', last_name: 'Gazzoo'},
        {id: 12, first_name: 'Rockhead', last_name: 'Slate'},
        {id: 13, first_name: 'Pearl', last_name: 'Slaghoople'}
      ]
    }
  },
  computed: {
    rows() {
      return this.opinions.length
    },
    itemsForList() {
      return this.opinions.slice(
          (this.currentPage - 1) * this.perPage,
          this.currentPage * this.perPage,
      );
    }
  }
}
</script>
<style>
.page-item > .page-link{
  color: black;
  border: 1px solid black;
  margin: 3px 3px 3px 3px;
}
.page-item.active .page-link {
  color:white !important;
  background-color: black !important;
  border:1px solid white !important;
}

.page-item .page-link:hover {
  color:white !important;
  background-color: gray !important;
  border:1px solid white !important;
}

li.page-item.disabled .page-link {
  color:black !important;
  background-color: white !important;
  border:none !important;
}

li.page-item.disabled .page-link:hover {
}
</style>