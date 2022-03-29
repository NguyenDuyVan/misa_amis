using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Department : BaseEntities
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
        [MISARequired]
        [PropertyNameDisplay("Đơn vị")]
        public string DepartmentName { get; set; }

        #endregion
    }
}
