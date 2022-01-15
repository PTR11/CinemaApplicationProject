<template>
  <div class="mx-auto col-sm-7" >
    <div class="center"><h1 class="my-3 mx-auto">Programs</h1></div>

    <div
      class="bg-warning border border-3  center mx-auto container justify-content-sm-center mb-5 p-2"
    >
      <div class="font-weight-bold mr-3">Pick a date: </div>
      <datepicker
        :disabled-dates="disabledDates"
        style="color: black; border: 2px solid white"
        class="col-sm-4"
        v-model="ertek"
      >
      </datepicker>
    </div>
    <Card
        v-for="program in programs"
        :key="program.movie"
        :element="program"
        :time="'present'"
    />
  </div>
</template>
<script>
import Datepicker from "vuejs-datepicker";
import CardComponent from "@/components/CardComponent.vue";
export default {
  data() {
    return {
      ertek: "",
      disabledDates: {
        to: new Date(2021, 7, 8), // Disable all dates up to specific date
        from: new Date(2021, 7, 15),
      },
      programs: [
        {
          title: "Star Wars",
          times: ["08:00", "09:30", "11:45", "19:30", "20:00", "22:05","08:00", "09:30", "11:45", "19:30", "20:00", "22:05"],
          date: "2021-07-08",
        },
        {
          title: "Lego Movie",
          times: ["08:00", "09:30", "11:45", "19:30", "20:00", "22:05"],
          date: "2021-07-08",
        },
        {
          title: "Valami",
          times: ["08:00", "09:30", "11:45", "19:30", "20:00", "22:05"],
          date: "2021-07-08",
        },
      ],
    };
  },
  components: {
    Datepicker,
    Card : CardComponent,
  },
  watch: {
    ertek: function() {
      this.shit();
    },
  },
  methods: {
    shit: function() {
      console.log("fasz"+this.ertek);
    },
  },
};
</script>