using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Employee : BaseEntities
    {
        #region Properties

        /// <summary>
        /// <summary>
        /// Id của nhân viên - Khóa chính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public Guid EmployeeId { get; set; }

        /// Mã của nhân viên
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [MISARequired]
        [PropertyNameDisplay("Mã nhân viên")]
        [ExelColumnName]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên 
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [MISARequired]
        [PropertyNameDisplay("Tên nhân viên")]
        [ExelColumnName]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Chức danh của nhân viên
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Chức danh")]
        public string? PositionName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        public Gender? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Giới sính")]
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Female:
                        return ResourceVN.Female;
                        break;
                    case Enum.Gender.Male:
                        return ResourceVN.Male;
                        break;
                    default:
                        return ResourceVN.Other;
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
        [EmailValid]
        public string? Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Số tài khoản")]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Tên ngân hàng")]
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
        /// Tên của đơn vị - Khóa ngoại
        /// </summary>
        /// CreatedBy: NDVan (3/3/2022)
        [ExelColumnName]
        [PropertyNameDisplay("Tên đơn vị")]
        public string DepartmentName { get; set; }

        #endregion

        #region Constructor
        #endregion

        #region Method
        #endregion

    }
}
