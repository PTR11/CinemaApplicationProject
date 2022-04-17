<template>
  <div class="overflow-auto" v-if="opinions.length > 0" >
      <div v-for="item in itemsForList" :key="item.id" class="card mb-2 border border-dark">
        <div class="card-body"  >
          <h5 class="card-title">User: {{ item.anonymus === true ? "Anonymus" : item.guestName }}</h5>
          <h6 class="card-subtitle mb-2">Rating: {{item.ranking}}</h6>
          <p class="card-text">Comment: {{item.description}}</p>

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
      currentPage: 1
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