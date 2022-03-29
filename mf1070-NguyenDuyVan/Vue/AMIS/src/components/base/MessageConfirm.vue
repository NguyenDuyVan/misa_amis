<template lang="">
  <div class="m-message-container m-message-confirm" ref="msgConfirmRef">
    <div class="m-overlay"></div>
    <div class="m-message-box">
      <div class="m-message-content">
        <div class="m-icon icon-message"></div>
        <div class="text-message">
          {{ confirmSaveMsg }}
        </div>
      </div>
      <div style="justify-content: space-between" class="m-message-footer">
        <div>
          <button class="m-btn m-btn-2" @click="handleMessage('Hủy')">
            Hủy
          </button>
        </div>
        <div>
          <button class="m-btn m-btn-2" @click="handleMessage('Không')">
            Không
          </button>
          <button
            style="margin-left: 5px"
            class="m-btn"
            @click="handleMessage('Có')"
          >
            Có
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { listMessage } from "../script/ResourceVN";
import { mapActions, mapState } from "vuex";
export default {
  props: ["handleMessage"],

  beforeMount() {
   this.updateWhereKeyup("msgConfirm");
  },

  mounted() {
    // Set sự kiện khi nhấn Enter Hoặc Esc thì đóng Message
    document.addEventListener("keyup", this.handleKeyupEvent);
  },

  beforeUnmount() {
    document.addEventListener("keyup", this.handleKeyupEvent);
    this.updateWhereKeyup("form");
  },

  methods: {
    //
    ...mapActions(["updateWhereKeyup"]),

    /**
     * Mô tả : Xử lý sự kiện nhấn ENTER hoặc ESC trong Message Confirm
     * Created by: NGUYEN DUY VAN
     * Created date: 12:41 20/03/2022
     */
    handleKeyupEvent(e) {
      e.stopPropagation();
      if (this.keyupWhere === "msgConfirm") {
        // 1.Khi nhấn ESC
        if (e.key === "Escape") {
          this.handleMessage("Hủy");
          return;
        }
        // 1.Khi nhấn ENTER
        if (e.key === "Enter") {
          this.handleMessage("Có");
          return;
        }
      }
    },
  },

  data() {
    return {
      confirmSaveMsg: listMessage.confirmSaveMsg,
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
