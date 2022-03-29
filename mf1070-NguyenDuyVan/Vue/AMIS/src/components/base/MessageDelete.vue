<template lang="">
  <div class="m-message-container m-message-warning">
    <div class="m-overlay"></div>
    <div class="m-message-box m-message-info">
      <div class="m-message-content">
        <div class="m-icon icon-message"></div>
        <div class="text-message">{{ handleDeleteMsg() }}</div>
      </div>
      <div style="justify-content: space-between" class="m-message-footer">
        <button class="m-btn m-btn-2" @click="handleDelete(false)">
          Không
        </button>
        <button class="m-btn" @click="handleDelete(true)">Có</button>
      </div>
    </div>
  </div>
</template>
<script>
import { deleteMessage } from "../script/ResourceVN";
import { mapActions, mapState } from "vuex";

export default {
  name: "DeleteMassage",
  props: ["employeeCode", "deleteEmployee", "deleteEmployees", "deleteMode"],

  beforeMount() {
    this.updateWhereKeyup("msgDelete");
  },

  mounted() {
    window.addEventListener("keyup", this.handleShorcut);
  },

  beforeUnmount() {
    this.updateWhereKeyup(null);
    window.removeEventListener("keyup", this.handleShorcut);
  },

  methods: {
    //
    ...mapActions(["updateWhereKeyup"]),

    /*******************************************************************
     * Hàm xử lý xóa đơn hay xóa nhiều
     * CreatedBy NDVan(17/03/2022)
     */
    handleDelete(bool) {
      // xóa đơn
      if (this.deleteMode === "single") {
        this.deleteEmployee(bool);
      }
      // xóa nhiều
      else if (this.deleteMode === "multi") {
        this.deleteEmployees(bool);
      }
    },

    /*******************************************************************
     * Xử lý message xóa nhiều hay xóa đơn
     * CreatedBy NDVan(18/03/2022)
     */
    handleDeleteMsg() {
      if (this.deleteMode === "single") {
        const msg = deleteMessage.singleDeleteMsg.replace(
          "{0}",
          `${this.employeeCode}`
        );
        return msg;
      } else {
        return deleteMessage.multiDeleteMsg;
      }
    },

    /*******************************************************************
     * Xử lý khi nhấn ESC hoặc ENTER
     * CreatedBy NDVan(19/03/2022)
     */
    handleShorcut(e) {
      e.stopPropagation();
      // Hủy mà không xóa
      if (e.key === "Escape" && this.keyupWhere === "msgDelete") {
        this.handleDelete(false);
      }
      // Xóa Nhân viên
      if (e.key === "Enter" && this.keyupWhere === "msgDelete") {
        this.handleDelete(true);
      }
    },
  },

  computed: {
    ...mapState(["keyupWhere"]),
  },
};
</script>
<style>
@import url("../style/components/message.css");
</style>
