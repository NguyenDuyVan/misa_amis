<template>
  <div class="navbar">
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-dashboard"></div>
        <div class="menu-text">Tổng quan</div>
      </a>
    </div>
    <div class="menu-item">
      <router-link to="/employees" class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-cash"></div>
        <div class="menu-text">Nhân viên</div>
      </router-link>
    </div>
    <div class="menu-item">
      <router-link to="/cash/deposit" class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-deposits"></div>
        <div class="menu-text">Tiền gửi</div>
      </router-link>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-buy"></div>
        <div class="menu-text">Mua hàng</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-sell"></div>
        <div class="menu-text">Bán hàng</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-invoice"></div>
        <div class="menu-text">Quản lý hóa đơn</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-stock"></div>
        <div class="menu-text">Kho</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-tools"></div>
        <div class="menu-text">Công cụ dụng cụ</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-fixed-assets"></div>
        <div class="menu-text">Tài sản cố định</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-tax"></div>
        <div class="menu-text">Thuế</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-price"></div>
        <div class="menu-text">Giá thành</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-general"></div>
        <div class="menu-text">Tổng hợp</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-budget"></div>
        <div class="menu-text">Ngân sách</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-report"></div>
        <div class="menu-text">Báo cáo</div>
      </a>
    </div>
    <div class="menu-item">
      <a class="menu-item-link">
        <div class="m-icon menu-icon menu-icon-finance"></div>
        <div class="menu-text">Phân tích tài chính</div>
      </a>
    </div>
  </div>
</template>
<script>
export default {};
</script>
<style>
@import url("../style/layout/navbar.css");
</style>
