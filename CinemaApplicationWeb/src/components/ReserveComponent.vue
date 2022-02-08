<template>
  <div id="app-sudoku">
    <transition name="fade">
      <div class="grid-order bg-warning" v-if="isGameStarted && !showAnswer">
        <div
          v-for="(row, rowIndex) in sudokuMatrix"
          class="grid-row"
          :key="rowIndex"
        >
          <span
            class="grid-cell-row"
            >{{ convert(rowIndex+1) }} line</span
          >
          <label ref="label" class="grid-cell" v-for="(cell, index) in row" :key="rowIndex+'-'+index" :style="sudokuMatrix[rowIndex][index] === 1 ? 'background-color: red' : '' ">
            <input type="checkbox" style="display:none" :id="index" @click="checkItem(rowIndex,index)" />
            <span class="justify-content-sm-center" v-if="sudokuMatrix[rowIndex][index] === 0 ||  sudokuMatrix[rowIndex][index] === 2" >{{ index+1 }}</span>
            <span class="justify-content-sm-center" v-else>P</span>
          </label>
          
        </div>
      </div>
    </transition>

    <transition name="fade">
      <div v-if="showAnswer" class="answer">
        <img v-bind:src="answerImage" class="answer-image" />
      </div>
    </transition>
  </div>
</template>

<script>
import reservedPlace from "../assets/reservedPlace.png"
import emptyPlace from "../assets/emptyPlace.png"
import axios from "axios";

export default {
  data() {
    return {
      sudokuMatrix: [],
      answerImage: "",
      isGameStarted: false,
      showAnswer: false,
      num: 0,
      isLoading: true,
      styleObject:{
        backgroundColor: 'red'
      }
    };
  },
  created: function() {
    this.image = '"@/assets/emptyPlace.png"';
    this.initializeGame();
    this.fetchShow();
  },
  methods: {
    fetchShow(){
      axios
          .get("http://localhost:7384/api/Shows/show/"+this.$route.params.id)
          .then((result) => {
            console.log(result);
          });
    },
    checkItem(row,col){
      this.isLoading = !this.isLoading;
      const idx = row * this.sudokuMatrix.length + col;
      if(this.sudokuMatrix[row][col] != 1){
        console.log("Kurva anyád");
        this.sudokuMatrix[row][col] = this.sudokuMatrix[row][col] === 0 ? 2 : 0;
        if(this.sudokuMatrix[row][col] === 2) {
          this.$refs.label[idx].style.backgroundImage = 'url('+reservedPlace+')';
          this.$refs.label[idx].style.backgroundColor = 'green';
          this.$refs.label[idx].classList.remove('bg-warning');
        }else{
          this.$refs.label[idx].style.backgroundImage = 'url('+emptyPlace+')';
          this.$refs.label[idx].style.backgroundColor = 'none';
          this.$refs.label[idx].classList.add('bg-warning');
        }
        this.$forceUpdate();
      }
    },
    convert(num) {
      return num
        .toString()    // convert number to string
        .split('')     // convert string to array of characters
        .map(Number)   // parse characters as numbers
        .map(n => (n || 10) + 64)   // convert to char code, correcting for J
        .map(c => String.fromCharCode(c))   // convert char codes to strings
        .join('');     // join values together
    }, 
    initializeGame() {
      this.initializeGameText = "Restart";
      this.isGameStarted = true;
      for (var x = 0; x < 9; x++) {
        this.sudokuMatrix[x] = [];
        for (var j = 0; j < 9; j++) {
          if(x === j ){
            this.sudokuMatrix[x][j] = 1;//FOGLALT
          }else{
            this.sudokuMatrix[x][j] = 0;//SZABAD - 3 ANYÁD
          }
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
  border: 3px solid black;
}

.grid-cell {
  display: table-cell;
  padding: 20px;
  border: 1px solid gray;
  width: 2rem;
  height: 2rem;
  background-image: url('../assets/emptyPlace.png');
  background-size: 75%;
  background-position: center;
  font-family: "Monaco", Courier, monospace;
}
.grid-cell-row {
  display: table-cell;
  padding: 20px;
  border: 1px solid gray;
  width: 7rem;
  height: 2rem;
  background-size: 75%;
  color: black;
  background-position: center;
  font-family: "Monaco", Courier, monospace;
}
</style>
