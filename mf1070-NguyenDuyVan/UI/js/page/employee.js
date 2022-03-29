$(document).ready(function () {
  loadData();
  employeeModal();
  validateForm();
});

/**-----------------------------------------
 * Tải dữ liệu
 * Created by: NDVan (2/10/2022)
 */
function loadData() {
  $.ajax({
    url: "http://amis.manhnv.net/api/v1/Employees",
    method: "GET",
  })
    .done(function (res) {
      var data = res;
      $.each(data, function (index, item) {
        var date = item.DateOfBirth;
        date = formatDate(date);
        var tr = `<tr>
          <td class="text-align-left"><div>${item.EmployeeCode}</div></td>
          <td class="text-align-left"><div>${item.EmployeeName}</div></td>
          <td class="text-align-left"><div>${item.GenderName}</div></td>
          <td class="text-align-center"><div>${date}</div></td>
          <td class="text-align-left"><div>${item.PhoneNumber}</div></td>
          <td class="text-align-left"><div>${item.Email}</div></td>
          <td class="text-align-left"><div>${item.EmployeePosition}</div></td>
          <td class="text-align-left"><div>${item.DepartmentName}</div></td>
          <td class="text-align-right"><div>${item.Salary}</div></td>
          <td class="text-align-left"><div>${item.WorkStatus}</div></td>
        </tr>`;

        $("table tbody").append(tr);
      });
    })
    .fail(function () {
      alert("error");
    });
}

/**
 * Format ngày tháng năm sinh
 * Created by NDVan (10/2/2022)
 */
function formatDate(date) {
  var date = new Date(date);
  var day = date.getDate(),
    month = date.getMonth() + 1,
    year = date.getFullYear();
  day = day < 10 ? "0" + day : day;
  month = month < 10 ? "0" + month : month;

  return `${day}/${month}/${year}`;
}

/**
 * Bật modal
 * Created by NDVan (13/2/2022)
 */
function employeeModal() {
  $("#btnAdd").click(toggleFormEmployee);
  $(".employee-form .close-btn").click(toggleFormEmployee);
  $(".employee-modal .overlay").click(toggleFormEmployee);

  function toggleFormEmployee() {
    $(".employee-modal").toggleClass("open-form");
  }
}

/**
 * Validate dữ liệu
 * Created by NDVan (13/2/2022)
 */
function validateForm() {
  textValidate();
  dateValidate();
  emailValidate();
  $("#btnSave").click(function () {
    $(".employee-form .m-input").each(addBorderError);
  });
}

/**
 * Text Validate
 * Created by NDVan (13/2/2022)
 */
function textValidate() {
  $(".employee-form .m-input[type=text]").each(function () {
    $(this).blur(addBorderError);
  });
}

/**
 * Add border error
 * Created by NDVan (13/2/2022)
 */
function addBorderError() {
  const bool = $(this).val();
  if (!bool) {
    $(this).addClass("error");
  } else {
    $(this).removeClass("error");
  }
}

/**
 * Date Validate
 * Created by NDVan (13/2/2022)
 */
function dateValidate() {
  $(".employee-form .m-input[type=date]").each(function () {
    $(this).change(function () {
      let value = $(this).val();
      value = new Date(value);
      if (value > new Date()) {
        $(this).addClass("error");
        $(this).next().addClass("active");
      } else {
        $(this).removeClass("error");
        $(this).next().removeClass("active");
      }
    });
  });
}

/**
 * Email Validate
 * Created by NDVan (13/2/2022)
 */
function emailValidate() {
  $(".employee-form .m-input[type=email]").blur(function () {
    const email = $(this).val();
    debugger;
    if (
      !email.match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      )
    ) {
      $(this).next().addClass("active");
      $(this).addClass("error");
    } else {
      $(this).next().removeClass("active");
      $(this).removeClass("error");
    }
  });
}
