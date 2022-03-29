<template>
  <div class="content">
    <div v-bind:class="{ zindex10: isActiveForm }" class="content-header">
      <div class="content-header-text">Nhân viên</div>
      <button
        @click="
          async () => {
            await getNewEmployeeCode();
            toggleFormEmployee();
            formMode = 'post';
          }
        "
        id="btnAdd"
        class="m-btn"
      >
        Thêm nhân viên
      </button>
      <EmployeeForm
        :toggleFormEmployee="toggleFormEmployee"
        v-if="isActiveForm"
        :employee="employeeSelected"
        :Loading="isLoading"
        :reload="getList"
        :mode="formMode"
        :getNewEmployeeCode="getNewEmployeeCode"
        :urlEmployees="urlEmployees"
      />
    </div>
    <div
      class="content-toolbar"
      v-bind:class="{ zIndex10: isMultiDelete === true }"
    >
      <div
        class="m-handle-multiple flex-center"
        @click="handleMultiButton"
        v-bind:class="{ mouseLeave: isMultiDelete === true }"
      >
        <div class="m-handle-multiple-txt">Thực hiện hàng loạt</div>
        <div
          class="m-icon menu-icon m-handle-multiple-down"
          v-bind:class="{ active: listEmployeesId.length > 0 }"
        ></div>
        <div
          @click="
            isShowMessageDelete = true;
            deleteMode = 'multi';
          "
          v-show="isMultiDelete && listEmployeesId.length > 0"
          class="m-handle-multiple-delete"
        >
          Xóa
        </div>
      </div>
      <div class="flex-center">
        <div class="m-input-wrapper">
          <input
            type="text"
            class="m-input placeholder-italic"
            v-model="searchValue"
            @input="filterEmployee"
            placeholder="Tìm kiếm theo mã, tên nhân viên"
          />
          <div class="m-icon m-input-icon"></div>
        </div>
        <div @click="getList" class="m-icon refresh-btn"></div>
        <a
          :href="urlEmployees + '/exelData'"
          target="_blank"
          class="m-icon export-exel-btn"
        ></a>
      </div>
    </div>
    <div class="m-table-container" ref="tableContainerRef">
      <div class="m-table-wapper" style="min-height: 552.2px">
        <table class="m-table">
          <thead>
            <tr @click="getList">
              <th
                style="left: 0px"
                class="text-align-center pos-sticky sub-border-left"
              >
                <div
                  @click="
                    (e) => {
                      e.stopPropagation();
                    }
                  "
                  style="width: 22px"
                  class="m-row"
                >
                  <div class="checkbox">
                    <div class="checkbox-group">
                      <input type="checkbox" id="html" />
                      <label
                        @click="
                          checkedAll = !checkedAll;
                          pushEmployeeId({ mode: 'all' });
                        "
                        for="html"
                      ></label>
                    </div>
                  </div>
                </div>
              </th>
              <th style="left: 43px" class="text-align-left pos-sticky">
                <div style="width: 125px">Mã nhân viên</div>
              </th>
              <th style="left: 189px" class="text-align-left pos-sticky">
                <div style="width: 150px">Tên nhân viên</div>
              </th>
              <th class="text-align-left">
                <div style="width: 120px">Giới Tính</div>
              </th>
              <th class="text-align-center">
                <div style="width: 130px">Ngày sinh</div>
              </th>
              <th class="text-align-left">
                <div style="width: 150px">Số CMND</div>
              </th>
              <th class="text-align-left">
                <div style="width: 150px">Chức Danh</div>
              </th>
              <th class="text-align-left">
                <div style="width: 150px">Tên Đơn Vị</div>
              </th>
              <th class="text-align-left">
                <div style="width: 150px">Số Tài Khoản</div>
              </th>
              <th class="text-align-left">
                <div style="width: 200px">Tên Ngân Hàng</div>
              </th>
              <th class="text-align-left">
                <div style="width: 300px">Chi Nhánh TK Ngân Hàng</div>
              </th>
              <th
                style="right: 42px"
                class="text-align-center pos-sticky sub-border-right"
              >
                <div style="width: 100px; border-right: 0">Chức Năng</div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in employees"
              :key="index"
              ref="{{employee.EmployeeId}}"
              @dblclick="
                getEmployeeSelected(employee);
                formMode = 'put';
              "
              @click="handleRowClick"
            >
              <td
                style="left: 0px"
                class="text-align-center pos-sticky sub-border-left"
              >
                <div style="width: 22px" class="m-row">
                  <div class="checkbox">
                    <div class="checkbox-group">
                      <input
                        v-bind:id="employee.EmployeeId"
                        type="checkbox"
                        class="employee-checkbox"
                        v-bind:checked="
                          checkedAll || handleCheckInput(employee.EmployeeId)
                        "
                        @change="
                          (e) =>
                            pushEmployeeId({
                              e: e,
                              employeeChecked: employee,
                              mode: 'single',
                            })
                        "
                      />
                      <label
                        v-bind:for="employee.EmployeeId"
                        @dblclick="(e) => e.stopPropagation()"
                      ></label>
                    </div>
                  </div>
                </div>
              </td>
              <td style="left: 43px" class="text-align-left pos-sticky">
                <div style="text-transform: uppercase">
                  {{ employee.EmployeeCode }}
                </div>
              </td>
              <td style="left: 189px" class="text-align-left pos-sticky">
                <div style="text-transform: capitalize">
                  {{ employee.EmployeeName }}
                </div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.GenderName }}</div>
              </td>
              <td class="text-align-center">
                <div>{{ formatDate(employee.DateOfBirth, "d/m/y") }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.IdentityNumber }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.PositionName }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.DepartmentName }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.BankAccountNumber }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.BankName }}</div>
              </td>
              <td class="text-align-left">
                <div>{{ employee.BankBranchName }}</div>
              </td>
              <td
                style="right: 42px"
                class="text-align-center pos-sticky sub-border-right"
              >
                <div style="border-right: 0">
                  <span
                    @click="
                      getEmployeeSelected(employee);
                      formMode = 'put';
                    "
                    style="
                      color: #0075c0;
                      padding-left: 12px;
                      font-weight: Bold;
                      cursor: pointer;
                    "
                    >Sửa</span
                  >
                  <span
                    @click="handShowleDeleteBtn"
                    @dblclick="handShowleDeleteBtn"
                    class="m-modify-arrow"
                  >
                    <div
                      @click="employeeSelected = employee"
                      class="m-dropdown"
                    >
                      <div
                        @click="
                          copyEmployee(employee);
                          formMode = 'post';
                        "
                        class="btnCopy"
                      >
                        Nhân bản
                      </div>
                      <div
                        @click="
                          isShowMessageDelete = true;
                          deleteMode = 'single';
                        "
                        class="btnDelete"
                      >
                        Xóa
                      </div>
                    </div>
                  </span>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="m-pagination">
      <div class="left-inner">
        <p>
          Tổng số: <strong>{{ TotalRecord }}</strong> bản ghi
        </p>
      </div>
      <div class="right-inner">
        <ComboBoxRecord
          :selectNumberRecord="selectNumberRecord"
          style="margin: 0 16px"
        />
        <div
          class="flex-center"
          style="min-width: 140px; justify-content: space-between"
        >
          <!-- Previous Button -->
          <div
            class="m-page-previous"
            @click="
              indexArr--;
              handleArrayPage();
              handlePageNumber('-1');
            "
            v-bind:class="{ blur: currentPageNumber === 1 }"
          >
            Trước
          </div>
          <div class="m-page-number">
            <!-- Trường hợp số trang bằng 4 trở xuống -->
            <div v-if="totalPageNumber <= 4">
              <span
                v-for="(page, index) in totalPageNumber"
                :key="index"
                @click="
                  currentPageNumber = page;
                  handlePageNumber(page);
                "
                v-bind:class="{ active: currentPageNumber == page }"
                >{{ page }}</span
              >
            </div>
            <!-- Trường hợp số trang từ 5 trở lên -->
            <div v-if="totalPageNumber >= 5">
              <span
                @click="
                  indexArr = 2;
                  handleArrayPage();
                  handlePageNumber(1);
                "
                v-bind:class="{ active: currentPageNumber === 1 }"
                >1</span
              >
              <span
                v-show="indexArr - 1 >= 2"
                @click="
                  indexArr = indexArr - 3;
                  handleArrayPage();
                "
                >...</span
              >
              <span
                v-for="(page, index) in arrPageNumber"
                :key="index"
                @click="
                  indexArr = page;
                  handlePageNumber(page);
                "
                v-bind:class="{ active: currentPageNumber === page }"
                >{{ page }}</span
              >
              <span
                v-show="totalPageNumber - indexArr > 2"
                @click="
                  indexArr = indexArr + 3;
                  handleArrayPage();
                "
                >...</span
              >
              <span
                @click="
                  indexArr = totalPageNumber - 2;
                  handleArrayPage();
                  handlePageNumber(totalPageNumber);
                "
                v-bind:class="{ active: currentPageNumber === totalPageNumber }"
                >{{ totalPageNumber }}</span
              >
            </div>
          </div>
          <!-- Next Button -->
          <div
            class="m-page-next"
            @click="
              indexArr++;
              handleArrayPage();
              handlePageNumber('+1');
            "
            v-bind:class="{ blur: currentPageNumber === totalPageNumber }"
          >
            Sau
          </div>
        </div>
      </div>
    </div>
    <TheLoading v-show="isLoading == true" />
    <MessageDelete
      :employeeCode="employeeSelected.EmployeeCode"
      v-if="isShowMessageDelete == true"
      :deleteEmployee="deleteEmployee"
      :deleteEmployees="deleteEmployees"
      :deleteMode="deleteMode"
    />
  </div>
</template>
<script>
import axios from "axios";
import { mapState, mapActions } from "vuex";
import EmployeeForm from "./EmployeeForm.vue";
import TheLoading from "../../base/TheLoading.vue";
import MessageDelete from "../../base/MessageDelete.vue";
import ComboBoxRecord from "../../base/ComboBoxRecord.vue";
import url from "../../script/url";

export default {
  components: {
    EmployeeForm,
    MessageDelete,
    TheLoading,
    ComboBoxRecord,
  },

  async created() {
    try {
      await this.getList();
    } catch (error) {
      console.log(error);
    }
  },

  mounted() {
    //xử lý tắt hết nút xóa
    window.addEventListener("click", () => {
      this.handleHideDeleteBtn();
      this.isMultiDelete = false;
    });
  },

  methods: {
    // Lấy ACTION tử VUEX
    ...mapActions(["updateDataState"]),

    /**
     * Mô tả: Lấy danh sách NV theo số bản ghi và số trang
     * Created by NDVan (22/2/2022)
     */
    async getList() {
      try {
        this.isLoading = true;
        this.employees = null;
        this.searchValue = null;
        await axios
          .get(
            `${this.urlEmployees}/filter?pageSize=${this.pageSize}&pageNumber=${this.currentPageNumber}`
          )
          .then((res) => {
            this.handleResponse(res);
            // Tắt Loading
            this.isLoading = false;
          })
          .catch((error) => {
            this.isLoading = false;
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : Xử lý response BE trả về
     * @param res: response BE trả về
     * Created by: NGUYEN DUY VAN
     * Created date: 15:00 22/03/2022
     */
    handleResponse(res) {
      // 1. Gán dữ liệu mới
      this.employees = res.data.Data;
      // 2.Gán tổng số bản ghi
      this.TotalRecord = res.data.TotalRecord;
      // 3. Gán tổng số trang
      this.totalPageNumber = Math.ceil(this.TotalRecord / this.pageSize);
      // 4. Xử lý lại phân trang
      this.handlePageNumber();
      this.handleArrayPage();
      // 5. Cuộn table lên đầu
      this.$refs.tableContainerRef?.scrollTo(0, 0);
    },

    /**
     * Mô tả: Định dạng lại thành Ngày/Tháng/Năm
     * @param (date) Date Of Birth
     * Created by NDVan (22/2/2022)
     */
    formatDate(date, formatMode) {
      let newDate = new Date(date);
      // Lấy ngày, tháng , năm
      let day = newDate.getDate();
      let month = newDate.getMonth() + 1;
      let year = newDate.getFullYear();
      // định dạng lại ngày, tháng < 10
      day = day < 10 ? "0" + day : day;
      month = month < 10 ? "0" + month : month;
      // trả về ngày đúng format
      if (formatMode === "d/m/y") return `${day}/${month}/${year}`;
      if (formatMode === "y/m/d") return `${year}-${month}-${day}`;
    },

    /**
     * Mô tả: Tô đậm hàng khi row được click
     * Created by NDVan (22/2/2022)
     */
    handleRowClick(e) {
      document.querySelectorAll("tr").forEach((item) => {
        item.classList.remove("active");
      });
      e.currentTarget.classList.toggle("active");
    },

    /**
     * Mô tả: Mở/Đóng Form nhân viên
     * Created by NDVan (22/2/2022)
     */
    toggleFormEmployee() {
      this.isActiveForm = !this.isActiveForm;
      // Nếu là data thay đổi thì load lại trang
      if (this.isDataChanged === true) this.getList();
      this.updateDataState(false);
    },

    /**
     * Mô tả:  Lấy mã nhân viên mới
     * Created by NDVan (22/2/2022)
     */
    async getNewEmployeeCode() {
      try {
        this.employeeSelected = {};
        await axios
          .get(`${this.urlEmployees}/newEmployeeCode`)
          .then((res) => {
            this.employeeSelected.EmployeeCode = res.data;
          })
          .catch((err) => {
            console.log(err);
          });
        return this.employeeSelected.EmployeeCode;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Lấy thông tin của nhân viên cần sửa, định dạng ngày và binding vào form
     * Created by NDVan (22/2/2022)
     */
    getEmployeeSelected(employee) {
      try {
        this.isLoading = true;
        const dName = employee.DepartmentName;
        axios.get(`${this.urlEmployees}/` + employee.EmployeeId).then((res) => {
          this.employeeSelected = res.data;
          // Định dạng lại ngày sinh: năm/tháng/ngày
          const dob = this.employeeSelected.DateOfBirth;
          this.employeeSelected.DateOfBirth = this.formatDate(dob, "y/m/d");
          // Định dạng lại ngày cấp: năm/tháng/ngày
          const iDate = this.employeeSelected.IdentityDate;
          this.employeeSelected.IdentityDate = this.formatDate(iDate, "y/m/d");
          this.employeeSelected.DepartmentName = dName;
          this.isActiveForm = true;
          this.isLoading = false;
        });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Xử lý z-index để hiển thị nút xóa
     * Created by NDVan (22/2/2022)
     */
    handShowleDeleteBtn(e) {
      //ẩn tất cả các nút xóa khác
      this.handleHideDeleteBtn();
      e.stopPropagation();
      // Thêm border cho icon down
      e.target.classList.add("active");
      e.target.children[0]?.classList.add("active");
      e.target.closest("td").classList.add("zIndex9");
    },

    /**
     * Mô tả: Xử lý để ẩn hết nút xóa
     * Created by NDVan (22/2/2022)
     */
    handleHideDeleteBtn() {
      try {
        document.querySelectorAll("table tr td:last-child").forEach((item) => {
          item.classList.remove("zIndex9");
        });
        [".m-modify-arrow", ".m-dropdown"].forEach((parent) => {
          document.querySelectorAll(parent).forEach((child) => {
            child.classList.remove("active");
          });
        });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Nhân bản thông tin nhân viên
     * Created by NDVan (17/3/2022)
     */
    async copyEmployee(employeeCopied) {
      //  1.Lấy mã nhân viên mới và gán cho biến tạm: newCode
      const newCode = await this.getNewEmployeeCode();
      //  2.Sao chép thông tin muốn nhân bản để gán vào Form
      this.employeeSelected = employeeCopied;
      //  3.Gán lại mã nhân viên mới
      this.employeeSelected.EmployeeCode = newCode;
      //  4.Mở Form nhân viên
      this.toggleFormEmployee();
    },

    /**
     * Mô tả: Xóa nhân viên
     * param(bool): true - xóa nhân viên/ false: không xóa
     * Created by NDVan (22/2/2022)
     */
    deleteEmployee(bool) {
      try {
        if (bool) {
          this.isLoading = true;
          const id = this.employeeSelected.EmployeeId;
          this.isShowMessageDelete = false;
          axios
            .delete(`${this.urlEmployees}/` + id)
            .then(() => {
              this.getList();
              this.isLoading = false;
            })
            .catch((err) => {
              this.isLoading = false;
              console.log(err.response);
            });
        } else {
          this.isShowMessageDelete = false;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: XÓA NHIỀU:  Xử lý logic để show BUTTON xóa nhiều
     * Created by NDVan (17/03/2022)
     */
    handleMultiButton(e) {
      e.stopPropagation();
      // Nếu >= 1 record đc chọn, thì show xóa nhiều button
      if (this.listEmployeesId.length > 0) {
        this.isMultiDelete = !this.isMultiDelete;
      }
    },

    /**
     * Mô tả: XÓA NHIỀU: Thêm EmployeeId vào mảng phục vụ cho xóa nhiều
     * param(e, employeeChecked, mode)
     * + e -> event
     * + employeeChecked -> nhân viên đc check
     * + mode: 'single': check đơn/ 'all': check tất cả
     * Created by NDVan (17/03/2022)
     */
    async pushEmployeeId({ e, employeeChecked, mode }) {
      try {
        // 1. GÁN ĐƠN
        if (mode === "single") {
          const isChecked = e.target.checked;
          // Thêm EmployeeId được checked vào mảng
          if (isChecked) {
            this.listEmployeesId.push(employeeChecked.EmployeeId);
          }
          // Xóa EmployeeId bị unchecked vào mảng
          else {
            this.listEmployeesId = this.listEmployeesId.filter(
              (item) => item != employeeChecked.EmployeeId
            );
          }
        }

        // 2. GÁN TẤT CẢ
        if (mode === "all") {
          // Gán tất cả EmployeeId
          if (this.checkedAll) {
            await axios
              .get(`${this.urlEmployees}/employeesId`)
              .then((res) => {
                this.listEmployeesId = res.data;
              })
              .catch((err) => {
                console.log(err.response);
              });
          }
          // Xóa hết EmployeeId
          else {
            this.listEmployeesId = [];
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: XÓA NHIỀU: Thực hiện xóa nhiều
     * param(bool): true - xóa/ false - hủy
     * Created by NDVan (17/03/2022)
     */
    async deleteEmployees(bool) {
      try {
        if (bool) {
          this.isLoading = true;
          this.isShowMessageDelete = false;
          await axios
            .delete(`${this.urlEmployees}`, {
              data: this.listEmployeesId,
            })
            .then(() => {
              this.getList();
              this.isLoading = false;
            })
            .catch((err) => {
              console.log(err.response);
            });
        } else {
          this.isShowMessageDelete = false;
        }
      } catch (error) {
        this.isLoading = false;
        console.log(error);
      }
    },

    /**
     * Mô tả: Xử lý Input Checked không bị mất khi load lại dữ liệu
     * CreatedBy NDVan(18/03/2022)
     */
    handleCheckInput(employeeId) {
      const result = this.listEmployeesId.find((item) => item === employeeId);
      return result;
    },

    /**
     * Mô tả: Tìm kiếm thông tin nhân viên theo MÃ - HỌ TÊN - SỐ ĐIỆN THOẠI
     * Created by NDVan (26/2/2022)
     */
    filterEmployee() {
      try {
        const me = this;
        // sau khi ngừng gõ phím 500ms, gọi api để search
        clearTimeout(me.timer);
        me.timer = setTimeout(() => {
          me.employees = null;
          // Thực hiện gọi api để search với giá trị là input.value()
          axios
            .get(`${this.urlEmployees}/filter?employeeFilter=${me.searchValue}`)
            .then((res) => {
              // gán kết quả tìm kiếm backend trả về
              // me.employees = res.data.Data;
              // me.TotalRecord = me.employees?.length || 0;
              this.handleResponse(res);
            })
            .catch((err) => {
              console.log(err);
            });
        }, 500);
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Mô tả:  Lựa chọn số bản ghi trên trang
     * Created by NDVan(26/2/2022)
     */
    selectNumberRecord(value) {
      this.pageSize = value;
      this.currentPageNumber = 1;
      this.getList();
    },

    /**
     * Mô tả: Xử lý logic phân trang
     * Created by NDVan(26/2/2022)
     */
    handlePageNumber(value) {
      try {
        // Chọn trang trước
        if (value === "-1" && this.currentPageNumber > 1) {
          this.currentPageNumber--;
          this.getList();
        }
        // Chọn trang sau
        else if (
          value === "+1" &&
          this.currentPageNumber < this.totalPageNumber
        ) {
          this.getList();
          this.currentPageNumber++;
        }
        // Chọn số trang cụ thể
        else if (typeof value === "number") {
          this.currentPageNumber = value;
          this.getList();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả: Xử lý chỉ mục trang khi nhấn dấu 3 chấm
     * CreatedBy NDVan(15/03/2022)
     */
    handleArrayPage() {
      try {
        // Xử lý dấu 3 chấm, render thứ tự trang
        if (this.totalPageNumber >= 5) {
          if (this.indexArr <= 2) {
            this.arrPageNumber = [2, 3];
          }

          // Nếu số trang lớn hơn 3 và bé hơn Tổng trang - 2
          if (this.indexArr >= 3 && this.indexArr < this.totalPageNumber - 2) {
            this.arrPageNumber = [
              this.indexArr,
              this.indexArr + 1,
              this.indexArr + 2,
            ];
          }

          // Nếu số trang lớn hơn Tổng trang - 2
          if (this.indexArr >= this.totalPageNumber - 2) {
            this.arrPageNumber = [
              this.totalPageNumber - 2,
              this.totalPageNumber - 1,
            ];
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
  },

  data() {
    return {
      urlEmployees: `${url}/Employees`,
      employees: [],
      TotalRecord: null,
      //thông tin nhân viên được chọn để sửa
      employeeSelected: {},
      isShowMessageDelete: false,
      isDeleteEmployee: false,

      //bật, tắt form nhân viên (true => mở, false => đóng)
      isActiveForm: false,
      //check, uncheck tất cả checkbox (true => check, false => uncheck)
      checkedAll: false,

      isLoading: false,
      //chọn chế độ post hay put
      formMode: "",
      // giá trị cần search
      searchValue: null,
      // gán setTimeout
      timer: {},

      // số bản ghi / trang
      pageSize: 20,
      // trang hiện tại, mặc định = 1
      currentPageNumber: 1,
      // tổng số trang
      totalPageNumber: 1,
      // mảng để loop số trang sau dấu 3 chấm
      arrPageNumber: [],
      // Chỉ mục của mảng arrPageNumber
      indexArr: 2,

      // THực hiện xóa hàng loạt
      isMultiDelete: false,
      // Mảng lưu id nhân viên để xóa nhiều
      listEmployeesId: [],
      // Chế độ xóa: 'single': xóa đơn, 'multi': xóa nhiều
      deleteMode: null,
    };
  },

  computed: {
    ...mapState(["isDataChanged"]),
  },
};
</script>
<style>
@import url("../../style/components/pagination.css");
@import url("../../style/components/table.css");
@import url("../../style/components/checkbox.css");
@import url("../../style/components/input.css");
@import url("../../style/components/button.css");
</style>
