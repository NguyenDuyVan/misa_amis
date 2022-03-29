<template lang="">
  <div class="m-message-container m-message-info" ref="msgInfoRef">
    <div class="m-overlay"></div>
    <div class="m-message-box m-message-info">
      <div class="m-message-content">
        <div class="m-icon icon-message"></div>
        <div class="text-message">{{ message }}</div>
      </div>
      <div class="m-message-footer">
        <button
          @click="
            closeMessage();
            focusInputEmpty();
          "
          class="m-btn"
        >
          Đóng
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapState } from "vuex";
export default {
  name: "MessageInfo",
  props: ["msgInfo", "closeMessage", "isMessageInfo"],
  beforeMount() {
    this.message = this.msgInfo;
  },

  mounted() {
    // Set sự kiện khi nhấn Enter Hoặc Esc thì đóng Message
    this.updateWhereKeyup("msgInfo");
    document.addEventListener("keyup", this.handleKeyupEvent);
  },

  beforeUnmount() {
    document.removeEventListener("keyup", this.handleKeyupEvent);
    this.updateWhereKeyup("form");
  },

  methods: {
    //
    ...mapActions(["updateWhereKeyup"]),

    /***************************************************************************************
     *  Focus vào ô input trống khi đóng message cảnh báo
     * Created by NDVan (22/2/2022)
     */
    focusInputEmpty() {
      ["#EmployeeCode", "#EmployeeName"].every((item) => {
        const inputRequire = document.querySelector(item);
        if (!inputRequire?.value) {
          inputRequire.focus();
          return false;
        } else {
          return true;
        }
      });
    },

    /**
     * Mô tả : Xử lý sự kiện nhấn ENTER hoặc ESC trong Message Info
     * Created by: NGUYEN DUY VAN
     * Created date: 12:41 20/03/2022
     */
    handleKeyupEvent(e) {
      e.stopPropagation();
      if (this.keyupWhere === "msgInfo") {
        // 1.Khi nhấn ESC
        if (e.key === "Escape") {
          this.closeMessage();
          this.focusInputEmpty();
          return;
        }
        // 1.Khi nhấn ENTER
        if (e.key === "Enter") {
          this.closeMessage();
          this.focusInputEmpty();
          return;
        }
      }
    },
  },

  data() {
    return {
      message: null,
    };
  },

  computed: {
    ...mapState(["keyupWhere"]),
  },
};
</script>
<style>
@import url("../style/components/message.css");
</style>
