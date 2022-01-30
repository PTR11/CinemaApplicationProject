import Vue from "vue";
import Vuex from "vuex"

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        selectedCategory : "",
        searchText:"",
        filterDate: new Date()
    },
    mutations: {
        setCategory(state, category){
            state.selectedCategory = category;
        },
        setSearchText(state,text){
            state.searchText = text;
        },
        setFilterDate(state,date){
            state.filterDate = date
        }
    },
    actions: {
        setCategory(state, category){
            state.commit("setCategory",category);
        },
        setSearchText(state, text){
            state.commit("setSearchText",text);
        },
        setFilterDate(state, date){
            state.commit("setFilterDate",date);
        },
    },
    modules: {},
    getters: {
        getCategory: (state) => state.selectedCategory,
        getText: (state) => state.searchText,
        getDate: (state) => state.filterDate,
    }
})