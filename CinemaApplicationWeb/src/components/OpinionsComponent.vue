<template>
  <Card :element="opinion"/>
</template>

<script>
import CardComponent from "../components/CardComponent.vue"

export default {
  props: ["opinion"],
  components: {
    Card : CardComponent,
  }
};
</script>
<style scoped>
</style>
