<template>
  <TheHeader />
  <div class="main">
    <TheNavbar />
    <TheContent />
  </div>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheNavbar from "./components/layout/TheNavbar.vue";
import TheContent from "./components/layout/TheContent.vue";

export default {
  name: "App",
  components: {
    TheHeader,
    TheNavbar,
    TheContent,
  },
};
</script>

<style>
@import url("./components/style/main.css");
</style>
