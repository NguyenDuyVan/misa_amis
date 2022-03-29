<template lang="">
  <div class="m-message-container m-message-warning">
    <div class="m-overlay"></div>
    <div class="m-message-box m-message-info">
      <div class="m-message-content">
        <div class="m-icon icon-message"></div>
        <div class="text-message">
          {{ EcodeExit }}
        </div>
      </div>
      <div class="m-message-footer">
        <button @click="closeMessage" class="m-btn">Đóng</button>
      </div>
    </div>
  </div>
</template>
<script>
import { listMessage } from "../script/ResourceVN";
import { mapActions, mapState } from "vuex";

export default {
  //Ecode : EmployeeCode
  props: ["Ecode", "closeMessage"],

  mounted() {
    // Set sự kiện khi nhấn Enter Hoặc Esc thì đóng Message
    this.updateWhereKeyup("msgWarning");
    document.addEventListener("keyup", this.handleKeyupEvent);
  },

  beforeUnmount() {
    this.updateWhereKeyup("form");
    document.removeEventListener("keyup", this.handleKeyupEvent);
  },

  methods: {
    //
    ...mapActions(["updateWhereKeyup"]),

    /**
     * Mô tả : Xử lý sự kiện nhấn ENTER hoặc ESC trong Message Warning
     * Created by: NGUYEN DUY VAN
     * Created date: 12:41 20/03/2022
     */
    handleKeyupEvent(e) {
      e.stopPropagation();
      if (this.keyupWhere === "msgWarning") {
        if (e.key === "Escape" || e.key === "Enter") {
          this.closeMessage();
          document.getElementById('EmployeeCode').focus();
          return;
        }
      }
    },
  },

  data() {
    return {
      EcodeExit: listMessage.employeeCodeExitsMsg.replace("{0}", this.Ecode),
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
