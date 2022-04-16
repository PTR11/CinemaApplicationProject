<template>
  <div id="app-sudoku">
    <div
        class="col-sm-5 bg-warning center container justify-content-center mb-5 border border-dark text-dark p-2">
      <div>
        <span class="mr-2 text-dark">Room:</span>
        <span>{{show.room.name}}</span>
      </div>
      <div>
        <label class="mr-2 text-dark">Movie:</label>
        <span>{{show.movie.title}}</span>
      </div>
      <div>
        <label class="mr-2 text-dark">Date:</label>
        <span>{{ show.date }}</span>
      </div>
    </div>
    <div
        class="d-flex mx-auto col-sm-5 bg-warning center container justify-content-center mb-5 border border-dark text-dark p-2">
      <div>
        <label class="mr-2 text-dark">Ticket type:</label>
        <select
            name="cars"
            id="cars"
            v-model="ticketCategory"
            class="btn border border-dark text-dark p-2 pt-1"
            style="width: auto"
        >
          <option value="normal" selected>Normal</option>
          <option value="student">Student</option>
          <option value="retired">Retired</option>
        </select>
      </div>
    </div>
    <transition name="fade">
      <div class="grid-order bg-warning mx-10">

        <div
            v-for="(row, rowIndex) in sudokuMatrix"
            class="grid-row"
            :key="rowIndex"
        >
          <span
              class="grid-cell-row"
          >Line {{ rowIndex+1 }}</span
          >

          <label ref="label" class="grid-cell" v-for="(cell, index) in row" :key="rowIndex+'-'+index" :style="sudokuMatrix[rowIndex][index] === 1 ? 'background-color: red' : '' ">
            <input type="checkbox" style="display:none" :id="index" @click="checkItem(rowIndex,index)" />
            <span class="justify-content-sm-center" >{{ convert(index) }}</span>
          </label>

        </div>

      </div>
    </transition>
    <button @click="reserve" class="btn btn-warning btn-lg mx-auto d-flex mt-4 border border-dark">Reserve</button>

  </div>
</template>

<script>
import reservedPlace from "../assets/reservedPlace.png"
import emptyPlace from "../assets/emptyPlace.png"
import axios from "axios";
import { mapState } from "vuex";

export default {
  name: "Reserve",
  data() {
    return {
      size1: 0,
      size2: 0,
      sudokuMatrix: [],
      ticketCategory: "normal",
      num: 0,
      isLoading: true,
      show: {}
    };
  },
  watch:{
    asd(date){
      return date.toDateString();
    }
  },
  computed:
      mapState({
        user: (state) => state.user,
      }),
  created: function() {
    this.image = '"@/assets/emptyPlace.png"';

    this.fetchShow();
  },
  methods: {
    reserve(){
        let response = {
          UserId: this.user?.id,
          ShowId: this.$route.params.id,
          places: []
        };
        for (let x = 0; x < this.size1; x++) {
          for (let j = 0; j < this.size2; j++) {
            if(this.sudokuMatrix[x][j] === 2 || this.sudokuMatrix[x][j] === 3 || this.sudokuMatrix[x][j] === 4 ){
              response.places.push({X: x, Y: j, TicketCategory: this.getCategory(this.sudokuMatrix[x][j])});
            }
          }
        }
        axios
            .post("http://localhost:7384/api/Rents/", response, {withCredentials: true})
            .then((result) => {
              if(result.status === 302){
                console.log("dadas")
                this.$router.push({name: 'Login', path:"/login"})
              }
              console.log(result);
            });

    },
    fetchRents(){
      let rents = [];
      axios
          .get("http://localhost:7384/api/Rents/"+this.$route.params.id)
          .then((result) => {
            console.log("kakakakakakkakakakakakakak");
            rents = result.data;
            console.log(result);
            if(rents.length !== 0){
              for(const element of rents){
                console.log("anyaddat");
                console.log(element);
                this.sudokuMatrix[element.x][element.y] = 1;
                this.checkItem(element.x,element.y);
              }
            }
          });

    },
    fetchShow(){
      axios
          .get("http://localhost:7384/api/Shows/show/"+this.$route.params.id)
          .then((result) => {
            this.show = result.data;
            this.show.date = this.show.date.split("T")[0] + " "+this.show.date.split("T")[1].split(".")[0];
            console.log(result);
            this.size1 = result.data.room["heigth"];
            this.size2 = result.data.room["width"];
            this.initializeGame();
            this.fetchRents();
            this.$forceUpdate();
          });

    },
    getType(){
      switch (this.ticketCategory){
        case "normal": return 2;
        case "student": return 3;
        case "retired": return 4;
        default: return 0;
      }
    },
    getCategory(num){
      switch (num){
        case 2: return "normal";
        case 3: return "student";
        case 4: return "retired";
      }
    },
    getColor(){
      switch (this.ticketCategory){
        case "normal": return "green";
        case "student": return "blue";
        case "retired": return "white";
      }
    },
    checkItem(row,col){
      this.isLoading = !this.isLoading;
      const idx = row * this.size2 + col;
      if(this.sudokuMatrix[row][col] !== 1){
        this.sudokuMatrix[row][col] = this.sudokuMatrix[row][col] === 0 ? this.getType() : 0;
        if(this.sudokuMatrix[row][col] === 2 || this.sudokuMatrix[row][col] === 3 || this.sudokuMatrix[row][col] === 4 ) {
          this.$refs.label[idx].style.backgroundImage = 'url('+reservedPlace+')';
          this.$refs.label[idx].style.backgroundColor = this.getColor();
          this.$refs.label[idx].classList.remove('bg-warning');
        }else{
          this.$refs.label[idx].style.backgroundImage = 'url('+emptyPlace+')';
          this.$refs.label[idx].style.backgroundColor = 'none';
          this.$refs.label[idx].classList.add('bg-warning');
        }
        this.$forceUpdate();
      }else{
        this.$refs.label[idx].style.backgroundImage = 'url('+reservedPlace+')';
        this.$refs.label[idx].style.backgroundColor = 'red';
        this.$refs.label[idx].classList.remove('bg-warning');
      }
    },
    convert(num) {
      return String.fromCharCode(65+num)     // join values together
    },
    initializeGame() {
      this.isGameStarted = true;
      this.sudokuMatrix = new Array(this.size1);
      for (let x = 0; x < this.size1; x++) {
        this.sudokuMatrix[x] = new Array(this.size2);
        for (let j = 0; j < this.size2; j++) {
            this.sudokuMatrix[x][j] = 0;
        }
      }
    },
  },
};
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
}
input:focus,
select:focus,
textarea:focus,
button:focus {
  outline: none;
}

#app-sudoku {
  place-self: center;
  display: grid;
  grid-template-rows: auto 1fr;
  justify-items: center;
}


.grid-order {
  display: table;
  background: white;
  border: 1px solid black;
}

.grid-cell {
  display: table-cell;
  border: 1px solid black;
  padding: 20px;
  width: 3rem;
  height: 2rem;
  background-image: url('../assets/emptyPlace.png');
  background-size: 75%;
  background-position: center;
  font-family: "Monaco", Courier, monospace;
}
.grid-cell-row {
  display: table-cell;
  padding: 20px;
  border: 0.5px solid black;
  width: 7rem;
  height: 2rem;
  background-size: 75%;
  color: black;
  background-position: center;
  font-family: "Monaco", Courier, monospace;
}
</style>
