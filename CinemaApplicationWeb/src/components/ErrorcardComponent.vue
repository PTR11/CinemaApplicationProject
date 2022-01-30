<template>
  <b-card center img-left class="d-flex justify-content-center mb-3 bg-warning text-dark h-25 border-dark mh-10">
    <b-card-title >
      Something went wrong
    </b-card-title>
    <b-card-text>
     {{ errorMessage }}
    </b-card-text>

  </b-card>

</template>
<script>
export default {
  name: "Errorcard",
  props: ["errorMessage"],
  data(){
    return{

    }
  }
}
</script>