import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Movies from '../views/Movies.vue'
import Reserve from "../views/Reserve";
import AddOpinion from '../views/AddOpinion.vue'
import Program from '../views/Program.vue'
import SignupComponent from '../components/SignupComponent.vue'
import LoginComponent from '../components/LoginComponent.vue'
import MovieDetailsComponent from '../components/MovieDetailsComponent.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/program',
    name: 'Programs',
    component: Program
  },
  {
    path: '/addOpinion',
    name: 'AddOpinion',
    component: AddOpinion,
  },
  {
    path: '/movies',
    name: 'Movies',
    component: Movies,
  },
  {
    path: '/movie/:id',  
    name: 'Movie',
    component: MovieDetailsComponent,
  },
  {
    path: '/reserve/:id',
    name: 'Reserve',
    component: Reserve,
  },
  {
    path: '/sign',
    Name: 'Sign',
    component: SignupComponent
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginComponent,
  }


]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
