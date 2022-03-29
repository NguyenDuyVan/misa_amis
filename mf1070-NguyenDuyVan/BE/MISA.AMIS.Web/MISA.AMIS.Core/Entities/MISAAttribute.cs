using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Attribute đánh dấu các thông tin bắt buộc nhập
    /// </summary>
    /// CreatedBy NDVan(15/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string? ErrorValidateMsg;
        public MISARequired(string? errorMsg = null)
        {
            ErrorValidateMsg = errorMsg;
        }
    }

    /// <summary>
    /// Attribute đánh dấu các Email phải đúng định dạng
    /// </summary>
    /// CreatedBy NDVan(15/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValid : Attribute
    {
    }


    /// <summary>
    /// Attribute dùng để định danh cho Property
    /// </summary>
    /// CreatedBy NDVan(15/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNameDisplay : Attribute
    {
        public string propertyName = String.Empty;
        /// <summary>
        /// Khởi tạo tên property
        /// </summary>
        /// <param name="propName">Tên của property cần đặt</param>
        /// CreatedBy NDVan(15/03/2022)
        public PropertyNameDisplay(string? propName = null)
        {
            propertyName = propName;
        }
    }

    /// <summary>
    /// Attribute đánh dấu các Email phải đúng định dạng
    /// </summary>
    /// CreatedBy NDVan(15/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class ExelColumnName : Attribute
    {
    }
}
