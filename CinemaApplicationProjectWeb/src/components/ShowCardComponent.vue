<template>
  <b-card :img-src="element.image" center img-alt="Card image" img-width="200" img-left class="mb-3 bg-warning text-dark border-dark mh-10 show">
    <b-card-title >
      <router-link :to="'movie/'+element.id" class="text-dark text-decoration-none">
        <div >{{ element.title }} <span v-if="element.length != 0">({{ element.length }} min)</span></div>
      </router-link>
    </b-card-title>
    <b-card-text>
      <b-card-text>
        Shows:
        <div class="vertical-scroll">
          <v-chip-group
              active-class="orange accent-4 white--text"
              column
          >

              <v-chip v-for="ti in element.shows" :key="ti.id">
                <router-link :to="'reserve/'+ti.id" >
                  <span> {{ timeChange(ti.date) }} </span>
                </router-link>
              </v-chip>
          </v-chip-group>
        </div>
      </b-card-text>
    </b-card-text>
  </b-card>
</template>

<script>
export default {
  props: ["element", "site"],
  data(){
    return{
      visible : 'false',
      actorsString : "",
    }
  },
  methods:{
    timeChange(time){
      var date = new Date(time);
      if(date.getMinutes() < 10) {
        return date.getHours()+":0"+date.getMinutes();
      }else{
        return date.getHours()+":"+date.getMinutes();
      }
    }
  }

};
</script>

<style scoped>

a{
  text-decoration: none;
  color: black;
}

a:hover{
  text-decoration: none;
  color: black;
}

.vertical-scroll::-webkit-scrollbar {
  width: 1em;
  height: 0.5em;

}

.vertical-scroll::-webkit-scrollbar-track {
  margin: 0.2em 2em 0.2em 0.2em;
  border-radius: 100vw;
  background-color: #e0e0e0;
  height:3em;
}

.vertical-scroll::-webkit-scrollbar-thumb {
  background: #fd7e20;
  border: 0.2em solid #e0e0e0;
  border-radius: 100vw;
}

/* Vertical scrolling */

.vertical-scroll {
  display: grid;
  overflow: auto;
  position: relative;
  height: 5em;
  border: 1px solid black;
  border-radius: 0.5em;
  padding: 0.2em;
}
.show{
  height: auto
}
</style>
