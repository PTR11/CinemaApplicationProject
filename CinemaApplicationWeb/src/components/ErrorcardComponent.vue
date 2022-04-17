<template>
  <b-card center img-left class="d-flex justify-content-center mb-3 bg-dark text-danger h-auto border-danger mh-10">
    <b-card-title>
      {{ errorMessage }}
    </b-card-title>
    <b-card-text v-if="errorsList">
      <ul>
        <li v-for="error in errorsList" :key="error">
          {{error}}
        </li>
      </ul>
    </b-card-text>

  </b-card>

</template>
<script>
export default {
  name: "Errorcard",
  props: ["errorMessage","errorsList"],
  data(){
    return{

    }
  }
}
</script>