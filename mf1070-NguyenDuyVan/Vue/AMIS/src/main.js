import { createApp } from "vue";
import App from "./App.vue";
import router from "./components/router";
import axios from "axios";
import VueAxios from "vue-axios";
import { createStore } from "vuex";
import indexStore from "./store";

// Create a new store instance.
const store = createStore(indexStore);

createApp(App).use(router, VueAxios, axios).use(store).mount("#app");
