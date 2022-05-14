<template>
  <div>
    <div v-if="loading" class="text-center">
      <b-spinner style="width: 3rem; height: 3rem;" variant="warning" label="Text Centered"></b-spinner>
    </div>
    <div v-else class="col-sm-7 mx-auto p-10">
      <Datepicker/>
      <Errorcard v-if="error.length != 0" :error-message="error" class="mx-auto d-flex p-2"></Errorcard>
      <Card
          v-for="program in programs"
          :key="program.title"
          :element="program"
          :site="'Main'"
      />
    </div>
  </div>

</template>

<script>
import ShowCardComponent from "@/components/ShowCardComponent";
import DatePickerComponent from "@/components/DatePickerComponent";
import ErrorcardComponent from "@/components/ErrorcardComponent";
import axios from "axios";
import {mapState} from 'vuex';


export default {
  name: "Program",
  components: {
    Datepicker: DatePickerComponent,
    Card: ShowCardComponent,
    Errorcard: ErrorcardComponent
  },
  data() {
    return {
      programs: [],
      error: "",
      loading: true
    };
  },
  computed:
      mapState({
        filterDate: (state) => state.filterDate
      }),
  watch:{
    filterDate(){
      this.fetchShows()
    }
  },
  created() {
    this.fetchShows()
  },
  methods: {
    fetchShows() {
      this.programs = [];
      this.error = "";
      axios
          .get(process.env.VUE_APP_API_ADDRESS+"/api/Shows/"+this.filterDate.toDateString())
          .then((result) => {
            if(result.data.length == 0){
              this.error = "No shows available on the chosen date";
            }else{
              this.programs = result.data;
              this.programs.forEach((m) => {
                var asd = "data:image/jpg;base64,"+m.image;
                m.image = asd;
              })
            }
            this.loading = false;
          }).catch(() => {
        this.error = "Something went wrong in our side"
        this.loading = false;
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