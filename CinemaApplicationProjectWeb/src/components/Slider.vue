<template>
  <div id="slider">
    <div>
      <transition-group name="fade" tag="div">
        <div v-for="i in [currentIndex]" :key="i" class="container col-sm-7">
          <img src="../assets/logo.png" alt="" />
        </div>
      </transition-group>
    </div>
  </div>
</template>

<script>
export default {
  name: "Slider",
  data() {
    return {
      images: [
        "https://cdn.pixabay.com/photo/2015/12/12/15/24/amsterdam-1089646_1280.jpg",
        "https://cdn.pixabay.com/photo/2016/02/17/23/03/usa-1206240_1280.jpg",
        "https://cdn.pixabay.com/photo/2015/05/15/14/27/eiffel-tower-768501_1280.jpg",
        "https://cdn.pixabay.com/photo/2016/12/04/19/30/berlin-cathedral-1882397_1280.jpg",
      ],
      timer: null,
      currentIndex: 0,
    };
  },
  mounted: function () {
    //   this.startSlide();
  },
  methods: {
    startSlide() {
      this.time = setInterval(this.next, 4000);
    },
    next() {
      this.currentIndex += 1;
    },
    prev() {
      this.currentIndex -= 1;
    },
  },
  computed: {
    currentImg() {
      return this.images[Math.abs(this.currentIndex) % this.images.length];
    },
  },
};
</script>

<style scoped>
.container {
  position: relative;
  border: 3px solid orange;
  padding:0px;
  border-radius: 50px;
  margin-top: 20px;
}
.fade-enter-active,
.fade-leave-active {
  transition: all 0.9 ease-in;
  overflow: hidden;
  visibility: visible;
  position: absolute;
  opacity: 1;
}
.fade-enter,
.fade-leave-to {
  visibility: hidden;
  width: 100%;
  opacity: 0;
}

img {
  width: 100%;
  height: 300px; 
  border-radius: 50px;
}

.prev,
.next {
  position: absolute;
  top: 50%;
  background-color: transparent;
  color: white;
  font-size: 30px;
  border: none;
  cursor: pointer;
  border-radius: 5px;
  text-align: center;
  text-decoration: none;
}

.next {
  right: 50px;
}

.prev {
  left: 50px;
}

.prev:hover,
.next:hover {
  background-color: rgba(0, 0, 0, 0.9);
}
</style>