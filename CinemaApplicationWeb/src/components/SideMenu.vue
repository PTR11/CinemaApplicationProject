<template>
  <div class="hidden col-sm-1">
    <vs-sidebar
      absolute
      hover-expand
      reduce
      background="warn"
      textWhite
      v-model="active"
      id="slideMenu"
    >
      <template #logo>
        <!-- ...img logo -->
      </template>

      <vs-sidebar-item id="home" class="item">
        <template #icon>
          <router-link to="/">
            <b-icon
              icon="house"
              font-scale="1"
              class="element"
              style="width: auto"
            ></b-icon>
          </router-link>
        </template>
        <router-link to="/" style="text-decoration: none;">
          <p class="element" style="">Home</p>
        </router-link>
      </vs-sidebar-item>
      <vs-sidebar-item id="program" class="item">
        <template #icon>
          <router-link to="/program" style="text-decoration: none;">
            <b-icon
              icon="calendar3"
              font-scale="2"
              class="element"
              style="width: auto"
            ></b-icon>
          </router-link>
        </template>
        <router-link to="/program" style="text-decoration: none;">
          <p class="element">Program</p>
        </router-link>
      </vs-sidebar-item>

      <vs-sidebar-item id="opinions" class="item">
        <template #icon>
          <router-link to="/opinions">
            <b-icon
              icon="chat-left-text"
              font-scale="2"
              class="element"
              style="width: auto"
            ></b-icon>
          </router-link>
        </template>
        <router-link to="/opinions" style="text-decoration: none;">
          <p class="element">Opinions</p>
        </router-link>
      </vs-sidebar-item>
      <vs-sidebar-item id="movies" class="item">
        <template #icon>
          <router-link to="/movies">
            <b-icon
              icon="film"
              font-scale="2"
              class="element"
              style="width: auto"
            ></b-icon>
          </router-link>
        </template>
        <router-link to="/movies" style="text-decoration: none;">
          <p class="element">Movies</p>
        </router-link>
      </vs-sidebar-item>
      <vs-sidebar-item id="profile" class="item">
        <template #icon>
          <router-link to="/sign">
            <b-icon icon="person" class="element" font-scale="2" style="width: auto"></b-icon>
          </router-link>
        </template>
        <router-link to="/sign" style="text-decoration: none;">
          <p class="element">Profile</p>
        </router-link>
      </vs-sidebar-item>
    </vs-sidebar>
  </div>
</template>
<script>
export default {
  data: () => ({
    active: "",
  }),
  created() {
    this.active = this.$route.name.toLowerCase();
  },
};
</script>

<style scoped>
#slideMenu {
  height: auto;
  padding-top: 20px;
  padding-bottom: 20px;
  margin: 0;
  top: 60%;
  position: fixed !important;
  -ms-transform: translateY(-50%);
  transform: translateY(-50%);
  border: none;
}
.item {
  outline: none;
  width: 100%;
  padding: 0;
  margin: 15px;
  margin-left: 0px;
}
.element {
  vertical-align: middle;
  color: white;
  width: 200px;
  height: 45px;
  margin: 0px;
  padding: 0px;
  text-decoration: none !important;

  background-color: none !important;
}
</style>
