using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class BaseEntities
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy NDVan (3/3/2022)
        public string? ModifiedBy { get; set; }
    }
}
