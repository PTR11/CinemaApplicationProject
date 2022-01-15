<template>
  <b-card img-src="https://placekitten.com/300/300" center img-alt="Card image" img-left class="mb-3 bg-warning text-white h-25">
    <b-card-title >
      <router-link v-if="site == 'Main'" :to="'movie/'+1" class="text-white text-decoration-none">
        <div >{{ element.title }} <span v-if="element.length != nil">({{ element.length }} min)</span></div>
      </router-link>
      <div v-else>
        {{ element.title }}
      </div>
    </b-card-title>
    <b-card-text v-if="site != 'Main'">
      Length: {{ element.length }}
    </b-card-text>

    <b-card-text>
      Some quick example text to build on the <em>card title</em> and make up the bulk of the card's
      content.
    </b-card-text>
    <div v-if="site == 'Main'">
      <b-button variant="warning" class="border border-2 border-light circle" @click="visible = !visible">Tonight's availability</b-button>
      <b-collapse :visible="!visible">
        <v-card-title>Tonight's availability</v-card-title>
        <v-card-text>
          <div class="vertical-scroll">
            <v-chip-group
                v-model="selection"
                active-class="orange accent-4 white--text"
                column
            >
              <v-chip v-for="ti in element.times" :key="ti">{{ti}}</v-chip>
            </v-chip-group>
          </div>

        </v-card-text>
        <v-card-actions>
          <router-link :to="'reserve/'+1" >
            <v-btn color="lighten-2" class="border border-2 border-light circle" text @click="reserve">
              Reserve
            </v-btn>
          </router-link>
        </v-card-actions>
      </b-collapse>

    </div>
  </b-card>
</template>

<script>
export default {
  props: ["element", "time", "site"],
  data(){
    return{
      visible : 'false',
    }
  },
  methods:{
    asd(){
      this.visible = this.visible ? 'false' : 'true'
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
  border: 2px solid white;
  height: 7em;
}
</style>
