using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {

        /// <summary>
        /// Khởi tạo đối tượng 
        /// </summary>
        /// CreatedBy NDVan (13/03/2022)
        IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository):base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        /// <summary>
        /// Thực hiện check mã đơn vị có trùng hay không
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns>
        /// true - trùng, false - không trùng
        /// </returns>
        /// CreatedBy NDVan (09/03/2022)
        public int CheckDepartmentCode(string departmentCode)
        {

            var res = _departmentRepository.CheckDepartmentCode(departmentCode);
            return res;
        }
    }
}
