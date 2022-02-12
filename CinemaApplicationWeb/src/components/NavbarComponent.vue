<template>
  <div>
    <b-navbar toggleable="sm" type="dark" variant="warning" class="col-sm-7 mx-auto mt-3 text-dark border b-1 border-dark" align="center" v-model="active">
      <b-navbar-brand href="#">NavBar</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav >
          <b-nav-item to="/"><div class="text-dark">Home</div></b-nav-item>
          <b-nav-item to="/movies"><div class="text-dark">Movies</div></b-nav-item>
          <b-nav-item to="/program"><div class="text-dark">Programs</div></b-nav-item>
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto" align="center">


          <b-nav-item-dropdown right v-if="user">
            <!-- Using 'button-content' slot -->
            <template #button-content>
              <b-avatar variant="light" class="mr-2" ></b-avatar>
              <span>{{user.userName}}</span>
            </template>
            <b-dropdown-item to="/login">Rents</b-dropdown-item>
            <b-dropdown-item @click="clickItem">Log out</b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown right v-else>
            <!-- Using 'button-content' slot -->
            <template #button-content>
              <em>User</em>
            </template>
            <b-dropdown-item to="/login">Log In</b-dropdown-item>
            <b-dropdown-item to="/sign">Sign In</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "NavbarComponent",
  data: () => ({
    active: "",
  }),
  computed:
      mapState({
        user: (state) => state.user,
      }),
  created() {
    this.active = this.$route.name.toLowerCase();
  },
  methods:{
    clickItem(){
      alert("Log out");
    }
  }
}
</script>

<style scoped>

</style>