namespace MISA.AMIS.Web.Models
{
    /// <summary>
    /// Đơn vị
    /// </summary>
    public class Department
    {
        #region Properties
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string ModifiedBy { get; set; }
        #endregion
    }
}
