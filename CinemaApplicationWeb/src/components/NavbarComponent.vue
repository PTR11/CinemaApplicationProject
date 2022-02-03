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
          <b-nav-form>
            <b-form-input size="sm" class="mr-sm-2" placeholder="Search"></b-form-input>
            <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
          </b-nav-form>


          <b-avatar variant="light" v-if="userLoggedIn"></b-avatar>
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
export default {
  name: "NavbarComponent",
  data: () => ({
    active: "",
  }),
  created() {
    this.active = this.$route.name.toLowerCase();
  },
}
</script>

<style scoped>

</style>