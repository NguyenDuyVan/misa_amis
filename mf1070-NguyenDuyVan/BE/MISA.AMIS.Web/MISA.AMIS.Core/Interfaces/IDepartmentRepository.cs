using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Thực hiện check mã nhân viên có trùng hay không
        /// </summary>
        /// <param name="DepartmentCode"></param>
        /// <returns>
        /// true - trùng, false - không trùng
        /// </returns>
        /// CreatedBy NDVan (09/03/2022)
        public int CheckDepartmentCode(string DepartmentCode);
    }
}
