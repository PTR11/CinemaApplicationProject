<template>
  <div class="mx-auto container">
    <ErrorCard error-message="No shows available" v-if="!date"/>
    <div v-else class= "d-flex justify-content-sm-center mb-5 p-2  bg-warning border border-dark">
      <label class="mr-2 text-dark " style="padding: 0.2em">Pick a date:</label>
      <datepicker
          :disabled-dates="disabledDates"
          class="text-dark"
          :placeholder="'Pick a date'"
          v-model="date"
          @input="changeDate()"
      >
      </datepicker>

    </div>
  </div>

</template>
<script>
import Datepicker from "vuejs-datepicker";
import ErrorcardComponent from "@/components/ErrorcardComponent";
import axios from "axios";
export default {
  name:"Picker",
  components: {
    Datepicker,
    ErrorCard : ErrorcardComponent
  },

  data() {
    return {
      disabledDates: {
        to: null, // Disable all dates up to specific date
        from: null,
      },
      date: new Date(),
    };
  },
  created() {
    this.fetchDates();
  },
  methods:{
    changeDate(){
      console.log("change")
      this.$store.dispatch("setFilterDate",this.date);
    },
    fetchDates(){
      axios
          .post( process.env.VUE_APP_API_ADDRESS+"/api/Shows/availableDates/")
          .then((result) => {
            console.log("date:",result.data);
            if(result.data.length == 0){
              this.date = null;
              this.$store.dispatch("setFilterDate",this.date);
            }else{
              this.disabledDates.to = new Date(result.data[0]);
              this.disabledDates.from = new Date(result.data[1]);
            }

          });
    }
  }
};
</script>