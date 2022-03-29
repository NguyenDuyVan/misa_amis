<template>
  <div class="m-employee-form">
    <div class="m-overlay"></div>
    <div class="m-employee-popup">
      <div class="m-popup-close">
        <div class="m-icon help-icon m-r-6"></div>
        <div
          @click="handleCloseEvent"
          class="m-icon close-icon"
          sc-title="Đóng (ESC)"
        ></div>
      </div>
      <div class="m-popup-header">
        <div class="m-popup-title">Thông tin nhân viên</div>
        <label class="flex-center checkbox-wrapper">
          <div class="checkbox-group">
            <input type="checkbox" id="isCustomer" />
            <label for="isCustomer">Là khách hàng</label>
          </div>
        </label>
        <label class="flex-center checkbox-wrapper">
          <div class="checkbox-group">
            <input type="checkbox" id="isProvider" />
            <label for="isProvider">Là nhà cung cấp</label>
          </div>
        </label>
      </div>
      <div class="m-popup-content">
        <div class="flex-center m-content-block">
          <div style="padding-right: 26px" class="left-inner">
            <div class="flex-center">
              <div class="flex-column w-40-percent p-r-6 p-b-12 item-info">
                <label for="">Mã <span style="color: red">*</span></label>
                <input
                  type="text"
                  class="m-input capitalize"
                  v-model="employeeSelected.EmployeeCode"
                  v-bind:class="{ error: isEmployeeCodeEmpty }"
                  @blur="checkInputRequire('EmployeeCodeInput')"
                  id="EmployeeCode"
                  errorTitle=""
                  style="text-transform: uppercase"
                  placeholder="Nhập mã NV"
                  maxlength="20"
                />
                <div class="error-title" style="width: 162px">
                  Mã NV không được để trống.
                </div>
              </div>
              <div class="flex-column w-60-percent p-b-12 item-info">
                <label for="">Tên <span style="color: red">*</span></label>
                <input
                  type="text"
                  class="m-input"
                  v-model="employeeSelected.EmployeeName"
                  v-bind:class="{ error: isEmployeeNameEmpty }"
                  @blur="checkInputRequire('EmployeeNameInput')"
                  style="text-transform: capitalize"
                  id="EmployeeName"
                  ref="EmployeeNameRef"
                  placeholder="Nhập họ và tên"
                  maxlength="100"
                />
                <div class="error-title" style="width: 150px">
                  Tên không được để trống.
                </div>
              </div>
            </div>
            <div class="flex-column p-b-12 item-info">
              <label for="">Đơn vị <span style="color: red">*</span></label>
              <ComboBoxDepartment
                style="width: 100%"
                v-bind:class="{ error: isDepartmentNameEmpty }"
                @blur="checkInputRequire('DepartmentNameInput')"
                :handleDepartment="handleDepartment"
                :departmentName="employeeSelected.DepartmentName"
              />
            </div>
            <div class="flex-column p-b-12 item-info">
              <label for="">Chức danh</label>
              <input
                type="text"
                class="m-input"
                required
                v-model="employeeSelected.PositionName"
                style="text-transform: capitalize"
                placeholder="Nhập chức danh"
                maxlength="100"
              />
            </div>
          </div>
          <div class="flex-column right-inner">
            <div style="width: 100%" class="flex-center">
              <div class="flex-column w-40-percent p-r-6 p-b-12 item-info">
                <label for=""
                  >Ngày sinh</label
                >
                <input
                  type="date"
                  v-model="employeeSelected.DateOfBirth"
                  v-bind:class="{
                    error: isDateOfBirthError,
                    active: employeeSelected.DateOfBirth,
                  }"
                  @input="checkDate('DateOfBirthInput')"
                  @blur="checkDate('DateOfBirthInput')"
                  title="Vui lòng nhập ngày sinh"
                  class="m-input m-input-date"
                  @focus="(e) => e.target.classList.add('active')"
                />
                <div class="m-error-info" v-show="isDateOfBirthError == true">
                  <div class="m-error-text">
                    Ngày sinh không được lớn hơn ngày hiện tại.
                  </div>
                  <div class="m-error-arrow"></div>
                </div>
              </div>
              <div
                style="padding-left: 6px; box-sizing: border-box"
                class="flex-column w-60-percent p-b-12 item-info"
                :tabindex="0"
                @focus="
                  if (!employeeSelected.Gender) employeeSelected.Gender = 0;
                "
                @keyup.arrow-right="
                  () => {
                    if (employeeSelected.Gender < 2) ++employeeSelected.Gender;
                  }
                "
                @keyup.arrow-left="
                  () => {
                    if (employeeSelected.Gender > 0) --employeeSelected.Gender;
                  }
                "
              >
                <div><label>Giới tính</label></div>
                <div style="height: 32px" class="flex-center">
                  <label class="p-r-20" for="male">
                    <input
                      type="radio"
                      id="male"
                      name="radio-1-set"
                      class="regular-radio"
                      :value="0"
                      v-model="employeeSelected.Gender"
                      v-bind:checked="employeeSelected.GenderName === 'Nam'"
                    />
                    <label style="top: 6px" for="male"></label>
                    <label style="margin-left: 7px" for="male">Nam</label>
                  </label>
                  <label class="p-r-20" for="female">
                    <input
                      type="radio"
                      id="female"
                      name="radio-1-set"
                      class="regular-radio"
                      :value="1"
                      v-model="employeeSelected.Gender"
                      v-bind:checked="employeeSelected.GenderName === 'Nữ'"
                    />
                    <label style="top: 6px" for="female"></label
                    ><label style="margin-left: 7px" for="female">Nữ</label>
                  </label>
                  <label class="p-r-20" for="otherSex">
                    <input
                      type="radio"
                      id="otherSex"
                      name="radio-1-set"
                      class="regular-radio"
                      :value="2"
                      v-model="employeeSelected.Gender"
                      v-bind:checked="employeeSelected.GenderName === 'Khác'"
                    />
                    <label
                      @focus="employeeSelected.GenderName = 'Khác'"
                      style="top: 6px"
                      for="otherSex"
                    ></label
                    ><label style="margin-left: 7px" for="otherSex">Khác</label>
                  </label>
                </div>
              </div>
            </div>
            <div style="width: 100%" class="flex-center">
              <div class="flex-column w-60-percent p-r-6 p-b-12 item-info">
                <label for="">Số CMTND</label>
                <input
                  type="text"
                  class="m-input"
                  v-model="employeeSelected.IdentityNumber"
                  placeholder="Nhập Số CMTND"
                  maxlength="25"
                />
              </div>
              <div class="flex-column w-40-percent p-b-12 item-info">
                <label for="">Ngày cấp </label>
                <input
                  style="padding: 0"
                  type="date"
                  v-model="employeeSelected.IdentityDate"
                  v-bind:class="{
                    error: isIdentityDateError,
                    active: employeeSelected.IdentityDate,
                  }"
                  @input="checkDate('IdentityDateInput')"
                  @blur="checkDate('IdentityDateInput')"
                  class="m-input m-input-date"
                  title="Vui lòng nhập ngày cấp"
                  @focus="(e) => e.target.classList.add('active')"
                />
                <div class="m-error-info" v-show="isIdentityDateError == true">
                  <div class="m-error-text">
                    Ngày cấp không được lớn hơn ngày hiện tại.
                  </div>
                  <div class="m-error-arrow"></div>
                </div>
              </div>
            </div>
            <div style="width: 100%" class="flex-column p-b-12 item-info">
              <label for="">Nơi cấp</label>
              <input
                type="text"
                required
                v-model="employeeSelected.IdentityPlace"
                class="m-input"
                style="text-transform: capitalize"
                placeholder="Nhập nơi cấp"
                maxlength="255"
              />
            </div>
          </div>
        </div>
        <div class="m-content-block">
          <div class="flex-column p-b-12 item-info">
            <label for="">Địa chỉ</label>
            <input
              type="text"
              required
              v-model="employeeSelected.Address"
              class="m-input"
              placeholder="Nhập địa chỉ"
              maxlength="255"
            />
          </div>
          <div class="flex-center">
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">ĐT di động</label>
              <input
                type="text"
                required
                v-model="employeeSelected.PhoneNumber"
                class="m-input"
                placeholder="Nhập ĐT di động"
                maxlength="50"
              />
            </div>
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">ĐT cố định</label>
              <input
                type="text"
                required
                v-model="employeeSelected.TelephoneNumber"
                class="m-input"
                placeholder="Nhập ĐT cố định"
                maxlength="50"
              />
            </div>
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">Email</label>
              <input
                type="email"
                class="m-input"
                placeholder="Nhập Email"
                title="Vui lòng nhập Email"
                v-model="employeeSelected.Email"
                v-bind:class="{ error: isEmailError }"
                @blur="checkEmail('blur')"
                maxlength="100"
              />
              <div class="m-error-info" v-show="isEmailError == true">
                <div style="width: 115px" class="m-error-text">
                  Email không đúng định dạng.
                </div>
                <div class="m-error-arrow"></div>
              </div>
            </div>
          </div>
          <div class="flex-center">
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">TK ngân hàng</label>
              <input
                type="text"
                required
                v-model="employeeSelected.BankAccountNumber"
                class="m-input"
                placeholder="Nhập TK ngân hàng"
                maxlength="25"
              />
            </div>
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">Tên ngân hàng</label>
              <input
                type="text"
                required
                v-model="employeeSelected.BankName"
                class="m-input"
                placeholder="Nhập Tên ngân hàng"
                maxlength="255"
              />
            </div>
            <div class="flex-column w-25-percent p-r-6 p-b-12 item-info">
              <label for="">Chi nhánh</label>
              <input
                type="text"
                required
                v-model="employeeSelected.BankBranchName"
                class="m-input"
                placeholder="Nhập Chi nhánh"
                maxlength="255"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="m-popup-footer">
        <button
          @click="toggleFormEmployee"
          v-on:keyup.enter="toggleFormEmployee"
          class="m-btn m-btn-2"
        >
          Hủy
        </button>
        <div>
          <button
            @click="buttonSaveClick('saveBtn')"
            style="margin: 0 10px"
            class="m-btn m-btn-2"
            sc-title="Cất (Ctrl + S)"
          >
            Cất
          </button>
          <button
            @click="buttonSaveClick('save&&AddBtn')"
            class="m-btn"
            id="saveAddBtn"
            sc-title="Cất và Thêm (Ctrl + Shift + S)"
          >
            Cất và Thêm
          </button>
        </div>
      </div>
    </div>
    <MessageInfo
      v-if="isMessageInfo"
      :msgInfo="msgInfo"
      :closeMessage="closeMessage"
      :isMessageInfo="isMessageInfo"
    />
    <MessageWarning
      :Ecode="employeeSelected.EmployeeCode"
      v-if="isEmployeeCodeExit == true"
      :closeMessage="closeMessage"
    />
    <MessageConfirm
      v-if="isMessageConfirm"
      :handleMessage="handleMessageConfirm"
    />
  </div>
</template>
<script>
import axios from "axios";
import { mapActions, mapState } from "vuex";
import MessageInfo from "../../base/MessageInfo.vue";
import MessageWarning from "../../base/MessageWarning.vue";
import MessageConfirm from "../../base/MessageConfirm.vue";
import ComboBoxDepartment from "../../base/ComboBoxDepartment.vue";
import { listMessage, genderName } from "../../script/ResourceVN";
import { gender } from "../../script/enum";

export default {
  name: "EmployeeForm",
  props: [
    "toggleFormEmployee",
    "employee",
    "Loading",
    "reload",
    "mode",
    "getNewEmployeeCode",
    "urlEmployees",
  ],
  components: {
    MessageInfo,
    MessageWarning,
    MessageConfirm,
    ComboBoxDepartment,
  },
  beforeMount() {
    this.formMode = this.mode;
    this.isLoading = this.Loading;
    this.updateWhereKeyup("form");
  },
  mounted() {
    //Focus tự động vào ô input Họ và tên
    this.$refs.EmployeeNameRef.focus();

    document.addEventListener("keydown", this.handleShortcutSave);
    document.addEventListener("keyup", this.handleShortcutEsc);
  },

  beforeUnmount() {
    document.removeEventListener("keydown", this.handleShortcutSave);
    this.updateWhereKeyup(null);
    document.removeEventListener("keyup", this.handleShortcutEsc);
  },

  methods: {
    /**
     * Mô tả:Lấy các Action trong store VueX
     * CreatedBy NDVan(15/03/2022)
     */
    ...mapActions(["updateSaveEvent", "updateDataState", "updateWhereKeyup"]),

    /**
     * Mô tả:Xử lý logic của ComboBox Department
     * @param (object) department: chứa thông tin departmentName, departmentId,...
     * Created by NDVan (27/2/2022)
     */
    handleDepartment(department) {
      try {
        //  1_Gán dữ liệu được truyền vào
        const obj = this.employeeSelected;
        obj.DepartmentName = department.DepartmentName;
        obj.DepartmentId = department.DepartmentId;
        //  2_Check lại DepartmentName có trống không
        if (obj.DepartmentName) {
          this.isDepartmentNameEmpty = false;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Validate Input TEXT khi blur
     * Created by NDVan (22/2/2022)
     */
    //1.Kiểm tra giá trị của input: mã nv, họ tên, đơn vị
    checkInputRequire(place) {
      try {
        //  1.0_Kiểm tra place
        const all = place === "All" ? true : false;
        //  1.1_Kiểm tra mã nhân viên
        const code = this.employeeSelected.EmployeeCode;
        if (place === "EmployeeCodeInput" || all) {
          this.isEmployeeCodeEmpty = code ? false : true;
        }
        //  1.2_Kiểm tra họ và tên
        const eName = this.employeeSelected.EmployeeName;
        if (place === "EmployeeNameInput" || all) {
          this.isEmployeeNameEmpty = eName ? false : true;
        }
        //  1.3_Kiểm tra đơn vị mà nhân viên làm việc
        const dName = this.employeeSelected.DepartmentName;
        if (place === "DepartmentNameInput" || all) {
          this.isDepartmentNameEmpty = dName ? false : true;
        }
        //  1.4_Trả về giá trị Boolean = false nếu thông tin bị thiếu
        return (
          !this.isEmployeeCodeEmpty &&
          !this.isEmployeeNameEmpty &&
          !this.isDepartmentNameEmpty
        );
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Validate Input DATE chọn ngày hoặc Blur
     * Created by NDVan (22/2/2022)
     */
    //2.Kiểm tra ngày nhập có lớn hơn ngày hiện tại không
    checkDate(place) {
      try {
        const today = new Date();
        const dob = this.employeeSelected.DateOfBirth;
        //  2.1_Kiểm tra ngày sinh
        if (place == "DateOfBirthInput") {
          if (dob) {
            this.isDateOfBirthError = new Date(dob) > today ? true : false;
          }
        }
        //  2.2_Kiểm tra ngày cấp
        const iDate = this.employeeSelected.IdentityDate;
        if (place == "IdentityDateInput") {
          if (iDate) {
            this.isIdentityDateError = new Date(iDate) > today ? true : false;
          }
        }
        //  2.3_Trả về giá trị Boolean = false nếu tất cả date không hợp lệ
        return !this.isDateOfBirthError && !this.isIdentityDateError;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Validate Input Email khi Blur
     * Created by NDVan (22/2/2022)
     */
    //3.Kiểm tra email có đúng định dạng không
    checkEmail(eventName) {
      try {
        const email = this.employeeSelected.Email;
        if (email) {
          //  Regex kiểm tra Email có sai định dạng ko
          if (
            !email.match(
              /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            )
          ) {
            this.isEmailError = true;
          } else {
            this.isEmailError = false;
          }
        }

        //  Không cần kiểm tra khi nhấn save
        else if (eventName === "save") {
          this.isEmailError = false;
        }
        //  Trả về giá trị boolean = false nếu email không hợp lệ
        return !this.isEmailError;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Đưa ra MESSAGE cho người dùng sửa khi nhấn SAVE BUTTON
     * Created by NDVan (22/2/2022)
     */
    //4.Đưa ra thông báo khi nhấn SAVE
    buttonSaveClick(btnName) {
      try {
        //  4.1_Kiểm tra các giá trị của input
        this.checkInputRequire("All");
        //  4.1.1_Kiểm tra mã nhân viên
        const code = this.employeeSelected.EmployeeCode;
        if (!code) {
          //  4.1.1.1_Gán string thông báo mã nhân viên trống
          this.msgInfo = listMessage.employeeCodeEmptyMsg;
          this.isMessageInfo = true;
          return false;
        }
        //  4.1.2_Kiểm tra họ và tên
        const eName = this.employeeSelected.EmployeeName;
        if (!eName) {
          //  4.1.2.1_Gán string thông báo Họ tên nhân viên trống
          this.msgInfo = listMessage.employeeNameEmptyMsg;
          this.isMessageInfo = true;
          return false;
        }
        //  4.1.3_Kiểm tra đơn vị mà nhân viên làm việc
        const dName = this.employeeSelected.DepartmentName;
        if (!dName) {
          //  4.1.3.1_Gán string thông báo Họ tên nhân viên trống
          this.msgInfo = listMessage.departmentNameEmptyMsg;
          this.isMessageInfo = true;
          return false;
        }
        this.insertUpdateEmployee(btnName);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: THÊM hoặc SỬA thông tin nhân viên
     * Created by NDVan (22/2/2022)
     */
    insertUpdateEmployee(btnName) {
      try {
        //  Kiểm tra thông tin bắt buộc, nếu đúng cho thực hiện POST/PUT
        const bool =
          this.checkInputRequire() &&
          this.checkDate() &&
          this.checkEmail("save");
        if (bool) {
          const obj = this.employeeSelected;
          let url;
          if (this.formMode === "post") {
            url = this.urlEmployees;
          }
          if (this.formMode === "put") {
            url = `${this.urlEmployees}/${obj.EmployeeId}`;
          }
          this.isLoading = true;
          axios[this.formMode](url, obj)
            .then(async () => {
              // Xử lý logic khi nhấn nút CẤT
              if (btnName === "saveBtn") {
                // Cập nhật STATE: isDataChanged trong VUEX
                this.updateDataState(true);
                //Tắt Form và load lại info
                this.toggleFormEmployee();
                this.isLoading = false;
              }

              // Xử lý logic khi nhấn nút CẤT VÀ THÊM
              if (btnName === "save&&AddBtn") {
                // xóa thông tin nhân viên được chọn
                this.employeeSelected = {};
                // Xóa đơn vị
                this.updateSaveEvent(true);
                // gán mã nhân viên mới
                this.employeeSelected.EmployeeCode =
                  await this.getNewEmployeeCode();
                this.isLoading = false;
              }
            })
            .catch((err) => {
              this.isLoading = false;
              const DataObj = err.response.data.Data;
              if (Object.keys(DataObj)[0] === "EmployeeCode") {
                this.isEmployeeCodeExit = true;
              }
              if (Object.keys(DataObj)[0] === "Email") {
                this.isEmailError = true;
              }
            });
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Đóng MESSAGE Info và Message trùng Mã NV
     * Created by NDVan (22/2/2022)
     */
    closeMessage() {
      this.isMessageInfo = false;
      this.isEmployeeCodeExit = false;
    },

    /**
     * Mô tả: Xử lý logic khi CLOSE Form nhân viên
     * Created by NDVan (22/2/2022)
     */
    handleCloseEvent() {
      if (this.isDataChanged === true) {
        this.isMessageConfirm = true;
      } else {
        this.toggleFormEmployee();
      }
    },

    /**
     * Mô tả: Xử lý logic của MESSAGE CONFIRM khi dữ liệu thay đổi nhưng người dùng nhấn CLOSE
     * Created by NDVan (22/2/2022)
     */
    handleMessageConfirm(btnName) {
      try {
        if (this.isDataChanged === true) {
          // Đóng message confirm và giữ lại Form chi tiết.
          if (btnName === "Hủy") {
            this.isMessageConfirm = false;
          }
          // Đóng message confirm và Form chi tiết.
          if (btnName === "Không") {
            this.isMessageConfirm = false;
            this.toggleFormEmployee();
          }
          // Đóng message confirm và Lưu lại
          if (btnName === "Có") {
            this.isMessageConfirm = false;
            this.buttonSaveClick("saveBtn");
          }
        } else {
          this.toggleFormEmployee();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : Xử lý sự kiện nhấn phím ESC
     * Created by: NGUYEN DUY VAN
     * Created date: 12:34 20/03/2022
     */
    handleShortcutEsc(e) {
      e.preventDefault();
      if (e.key === "Escape" && this.keyupWhere === "form") {
        this.handleCloseEvent();
        console.log("ok");
      }
    },

    /**
     * Mô tả : Xử lý sự kiện nhấn phím Ctrl + S và Ctrl + Shift + S
     * Created by: NGUYEN DUY VAN
     * Created date: 12:36 20/03/2022
     */
    handleShortcutSave(e) {
      try {
        if (e.ctrlKey && e.key === "s") {
          e.preventDefault();
          this.buttonSaveClick("saveBtn");
          return;
        }
        if (e.ctrlKey && e.shiftKey && e.key === "S") {
          e.preventDefault();
          this.buttonSaveClick("save&&AddBtn");
          return;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },

  watch: {
    employeeSelected: {
      handler() {
        // Cập nhật STATE: isDataChanged trong VUEX
        this.updateDataState(true);
      },
      deep: true,
    },
    // Gán lại Giới tính khi có sự thay đổi
    GenderName(text) {
      switch (text) {
        case genderName.male:
          this.employeeSelected.Gender = gender.male;
          break;
        case genderName.female:
          this.employeeSelected.Gender = gender.female;
          break;
        default:
          this.employeeSelected.Gender = gender.other;
          break;
      }
    },
  },

  data() {
    return {
      //Nhân viên hiện tại để tương tác với form
      employeeSelected: this.employee,

      //Prop để validate form
      isEmployeeCodeEmpty: false,
      isEmployeeCodeExit: false,
      //
      isEmployeeNameEmpty: false,
      //
      isDepartmentNameEmpty: false,
      //
      isDateOfBirthError: false,
      //
      isIdentityDateError: false,
      //
      isEmailError: false,

      //biến bật message thông báo lỗi
      isMessageInfo: false,
      // message xác nhận đóng khi dữ liệu thay đổi
      isMessageConfirm: false,
      //
      msgInfo: "",
      //
      isLoading: false,
      // Trạng thái POST hay PUT
      formMode: "",
      //
      isCtrl: false,
    };
  },

  computed: {
    ...mapState(["isDataChanged", "keyupWhere"]),
  },
};
</script>
<style>
@import url("../../style/components/employee_form.css");
@import url("../../style/components/datePicker.css");
@import url("../../style/components/radio.css");
@import url("../../style/components/input.css");
</style>
