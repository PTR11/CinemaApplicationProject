<template>
  <div id="background">
    <Slider />
    <NavbarComponent/>
    <div id="app">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Slider from "./components/Slider.vue";
import NavbarComponent from "./components/NavbarComponent";



export default {
  name: "App",
  components: {
    Slider,
    NavbarComponent
  },
  data() {
    return {
      windowHeight: window.innerWidth,
      txt: '',
      number: 6
    };
  },
  watch: {
    windowHeight(newHeight, oldHeight) {
      this.txt = `it changed to ${newHeight} from ${oldHeight}`;
    },
  },
  methods: {
    onResize() {
      this.windowHeight = window.innerHeight;
    },
  },
};
</script>

<style>
#app {
  margin: 30px;
  color: white;
}

body {
  background-image: url("assets/background.jpg");
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
  margin: 0px;
  min-width: 1000px !important;
}
</style>
