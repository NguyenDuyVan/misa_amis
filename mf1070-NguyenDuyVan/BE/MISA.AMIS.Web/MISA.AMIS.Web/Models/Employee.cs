namespace MISA.AMIS.Web.Models
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy NDVan (3/3/2022)
    public class Employee
    {
        #region Field 
        #endregion

        #region Properties

        /// <summary>
        /// Id của nhân viên - Khóa chính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã của nhân viên
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên 
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string EmployeeName { get; set; }

        /// <summary>
        /// Chức danh của nhân viên
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string EmployeePosition { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public int? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return "Nữ";
                        break;
                    case 1:
                        return "Nam";
                        break;
                    default:
                        return "Khác";
                        break;
                }
            }
        }

        /// <summary>
        /// Số CMND/ Căn cước
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email của nhân viên
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Id của đơn vị - Khóa ngoại
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public string? ModifiedBy { get; set; }

        #endregion

        #region Constructor
        #endregion

        #region Method
        #endregion

    }
}
