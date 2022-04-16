<template>

  <div class="mx-auto col-sm-7">
    <Datepicker/>
    <Card
        v-for="program in programs"
        :key="program.title"
        :element="program"
        :site="'Main'"
    />
  </div>
</template>

<script>
import ShowCardComponent from "@/components/ShowCardComponent";
import DatePickerComponent from "@/components/DatePickerComponent";
import axios from "axios";
import {mapState} from 'vuex';


export default {
  name: "Program",
  components: {
    Datepicker: DatePickerComponent,
    Card: ShowCardComponent,
  },
  data() {
    return {
      programs: []
    };
  },
  computed:
      mapState({
        filterDate: (state) => state.filterDate
      }),
  watch:{
    filterDate(){
      console.log("Kurva anyádat")
      this.fetchShows()
    }
  },
  created() {
    this.fetchShows()
  },
  methods: {
    fetchShows() {
      axios
          .get("http://localhost:7384/api/Shows/"+this.filterDate.toDateString())
          .then((result) => {

            //window.location.href = result.data.headers[0].value[0]
            this.programs = result.data;
            this.programs.forEach((m) => {
              var asd = "data:image/jpg;base64,"+m.image;
              m.image = asd;



            })
            console.log(this.programs);
          });
    }
  }

};
</script>
<style>
input,select {
  padding: 0.2em;
  font-size: 100%;
  border: 1px solid black !important;
  width: 100%;
}
</style>