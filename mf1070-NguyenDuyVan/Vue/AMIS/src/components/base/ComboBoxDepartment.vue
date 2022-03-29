<template lang="">
  <div class="m-combo-box">
    <div
      class="select-selected m-select-department"
      @keyup.enter="handleSelectClick"
      @click="handleSelectClick"
      v-bind:class="{
        active: isAddBorder == true,
        error: isDepartmentNameEmpty === true,
      }"
      :tabindex="0"
    >
      <!-- {{ departmentNameVal || "Chọn phòng ban" }} -->
      <input
        class="m-input m-input-department"
        placeholder="Chọn phòng ban"
        @blur="checkDepartmentRequired"
        @input="handleAutoComplete"
        v-model="departmentNameVal"
      />
      <span class="select-selected-arrow"></span>
    </div>
    <div style="width: 165px; top: calc(100% + 5px)" class="error-title">
      Đơn vị không được để trống.
    </div>
    <div
      class="option-container"
      style="top: calc(100% + 3px)"
      v-show="isOpenOption"
    >
      <div
        class="option m-option-department"
        v-for="(department, index) in departmentList"
        :key="index"
        :tabindex="0"
        @click="handleOptionEvent(department)"
        @keyup.enter="handleOptionEvent(department)"
        v-bind:class="{
          active: departmentNameVal === department.DepartmentName,
        }"
      >
        {{ department.DepartmentName }}
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import { mapState, mapActions } from "vuex";

export default {
  name: "ComboBoxDepartment",
  props: ["handleDepartment", "departmentName"],

  async beforeMount() {
    await this.getDepartmentData();
    this.handleAutoComplete();
  },

  mounted() {
    //tắt dropdown khi window click
    window.addEventListener("click", () => {
      this.isOpenOption = false;
      this.isAddBorder = false;
    });
  },

  beforeUnmount() {
    window.removeEventListener("click", () => {
      this.isOpenOption = false;
      this.isAddBorder = false;
    });
  },

  methods: {
    /**
     * Mô tả: Lấy Action từ Store VueX
     * Created by NDVan (27/2/2022)
     */
    ...mapActions(["updateSaveEvent"]),

    /**
     * Mô tả: Lấy dữ liệu là ĐƠN VỊ từ trên serve
     * Created by NDVan (27/2/2022)
     */
    async getDepartmentData() {
      await axios.get("https://localhost:44323/api/Departments").then((res) => {
        this.departmentData = res.data;
        this.departmentList = res.data;
      });
    },

    /**
     * Mô tả: Xử lý logic của Select Tag khi được Click
     * Created by NDVan (27/2/2022)
     */
    handleSelectClick(e) {
      this.isAddBorder = true;
      e.stopPropagation();
      this.isOpenOption = !this.isOpenOption;
    },

    /**
     * Mô tả: Xử lý logic của Select Tag khi được Click
     * Created by NDVan (27/2/2022)
     */
    handleOptionEvent(department) {
      this.departmentNameVal = department.DepartmentName;
      this.handleDepartment(department);
      this.checkDepartmentRequired();
      this.isOpenOption = false;
    },

    /**
     * Mô tả: Kiểm tra DepartmentName có trống không
     * Created by NDVan (27/2/2022)
     */
    checkDepartmentRequired() {
      // TH DepartmentName Trống
      if (!this.departmentNameVal) {
        this.isDepartmentNameEmpty = true;
      } else {
        this.isDepartmentNameEmpty = false;
      }
    },

    /**
     * Mô tả : Xử lý Auto Complete cho input đơn vị
     * Created by: NDVan
     * Created date: 07:51 23/03/2022
     */
    handleAutoComplete() {
      this.departmentList = this.departmentData.filter((item) => {
        return (
          item.DepartmentName?.toLocaleLowerCase().indexOf(
            this.departmentNameVal?.toLocaleLowerCase() || ""
          ) > -1
        );
      });
    },
  },

  watch: {
    isSaveAndAddEvent(value) {
      if (value === true) {
        this.departmentNameVal = null;
        this.updateSaveEvent(false);
      }
    },
  },

  data() {
    return {
      //
      departmentData: [],
      departmentList: [],
      departmentNameVal: this.departmentName,
      isDepartmentNameEmpty: false,
      //
      isOpenOption: false,
      isAddBorder: false,
    };
  },
  computed: {
    ...mapState(["isSaveAndAddEvent"]),
  },
};
</script>
<style>
@import url("../style/components/combo_box.css");
</style>
