<template>
  <div>
    <div v-for="error in errors" :key="error">
      <span  class="text-danger">{{ error }}</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "InputErrorComponent",
  props: ['errors']
}
</script>