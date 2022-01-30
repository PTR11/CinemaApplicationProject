<template>
  <div
      class="d-flex bg-warning border border-dark mx-auto container justify-content-sm-center mb-5 p-2"
  >
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
</template>
<script>
import Datepicker from "vuejs-datepicker";
import axios from "axios";
export default {
  name:"Picker",
  components: {
    Datepicker,
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
          .post("http://localhost:7384/api/Shows/availableDates/")
          .then((result) => {
            this.disabledDates.to = new Date(result.data[0]);
            this.disabledDates.from = new Date(result.data[1]);
          });
    }
  }
};
</script>