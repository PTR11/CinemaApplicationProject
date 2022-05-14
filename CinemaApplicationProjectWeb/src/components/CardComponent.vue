<template>
  <b-card :img-src="element.image" center img-alt="Card image" img-left img-width="200" class="mb-3 bg-warning text-dark h-25 border-dark rounded-30 mh-10">
    <b-card-title >
      <router-link :to="'movie/'+element.id" class="text-dark text-decoration-none">
        <div >{{ element.title }} <span v-if="element.length != nil">({{ element.length }} min)</span></div>
      </router-link>
    </b-card-title>
    <b-card-text v-if="site != 'Main'">
      Length: {{ element.length }}
    </b-card-text>

    <b-card-text>
      Actors: {{actorsList}}
    </b-card-text>

    <b-card-text class="short-text">
        {{element.description}}
    </b-card-text>
    <div v-if="site === 'Main'">
      <b-button variant="warning" class="border border-1 border-dark circle" @click="visible = !visible">Tonight's availability</b-button>
      <b-collapse :visible="!visible">
        <v-card-text>
          <div class="vertical-scroll">
            <v-chip-group
                active-class="orange accent-4 white--text"
                column
            >

              <v-chip v-for="ti in element.shows" :key="ti.date">
                <router-link :to="'reserve/'+ti.id" class="text-decoration-none text-dark">
                {{ timeChange(ti.date) }}
                </router-link>
              </v-chip>
            </v-chip-group>
          </div>

        </v-card-text>
      </b-collapse>

    </div>
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
  computed:{
    actorsList(){
      this.clearString();
      this.element?.actors.forEach(e => {
        this.actorsString = this.actorsString.concat('',e.name, ', ');
      });
      return this.actorsString.slice(0, -2);
    },
  },
  methods:{
    clearString(){
      this.actorsString = "";
    },
    asd(){
      this.visible = this.visible ? 'false' : 'true'
    },
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

.vertical-scroll::-webkit-scrollbar {
  width: 1em;
  height: 1em;
}

.vertical-scroll::-webkit-scrollbar-track {
  border-radius: 100vw;
  margin-block: 0.5em;
  background-color: #e0e0e0;
}

.vertical-scroll::-webkit-scrollbar-thumb {
  background: #fd7e20;
  border: 0.2em solid #e0e0e0;
  border-radius: 100vw;
}

.vertical-scroll::-webkit-scrollbar-thumb:hover {
  background: #fd7e20;
}

/* Vertical scrolling */

.vertical-scroll {
  display: grid;
  padding: 0.5em;
  overflow: auto;
  border-radius: 1em;
  position: relative;
  border: 1px solid black;
  height: 7em;
}

.short-text{
  text-align: left;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
