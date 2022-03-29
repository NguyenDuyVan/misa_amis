<template lang="">
  <div class="m-combo-box">
    <div
      class="select-selected"
      @click="
        (e) => {
          isAddBorder = true;
          e.stopPropagation();
          isOpenOption = !isOpenOption;
        }
      "
      v-bind:class="{ active: isAddBorder == true }"
    >
      {{ pageSize }} bản ghi trên 1 trang
      <span class="select-selected-arrow"></span>
    </div>
    <div class="option-container" v-show="isOpenOption">
      <!-- select-hide -->
      <div class="option" @click="pageSize = 10">10 bản ghi trên 1 trang</div>
      <div class="option active" @click="pageSize = 20"> 20 bản ghi trên 1 trang</div>
      <div class="option" @click="pageSize = 30">30 bản ghi trên 1 trang</div>
      <div class="option" @click="pageSize = 50">50 bản ghi trên 1 trang</div>
      <div class="option" @click="pageSize = 100">100 bản ghi trên 1 trang</div>
    </div>
  </div>
</template>
<script>
export default {
  name: "ComboBoxRecord",
  props: ["selectNumberRecord"],

  mounted() {
    //tắt dropdown khi window click
    window.addEventListener("click", () => {
      this.isOpenOption = false;
      this.isAddBorder = false;
    });

    //Thêm Background-Color cho Option khi click
    let options = document.querySelectorAll(".option");
    options.forEach((item) => {
      item.addEventListener("click", (e) => {
        // bỏ hết BG-color
        options.forEach((child) => {
          child.classList.remove("active");
        });
        // thêm BD-color cho option click
        e.target.classList.add("active");
      });
    });
  },

  watch: {
    pageSize(value) {
      this.selectNumberRecord(value);
    },
  },
  data() {
    return {
      // số bản ghi / trang
      pageSize: 20,
      isOpenOption: false,
      isAddBorder: false,
    };
  },
};
</script>
<style lang=""></style>
