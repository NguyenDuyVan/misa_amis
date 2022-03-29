using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Tìm kiếm thông tin nhân viên theo Mã hoặc Họ Tên
        /// </summary>
        /// <param name="employeeFilter">Id nhân viên cần lấy dữ liệu</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public object GetPaging(string? employeeFilter, int? pageSize, int? pageNumber);

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy tất cả Id nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public IEnumerable<Guid> GetAllEmployeesId();

        /// <summary>
        /// Thực hiện check mã nhân viên có trùng hay không
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>
        /// true - trùng, false - không trùng
        /// </returns>
        /// CreatedBy NDVan (09/03/2022)
        public int CheckEmployeeCode(string employeeCode);



        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entitiesId">Danh sách Id những đối tượng cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int DeleteMulti(string entitiesIdText);


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// trả về mã nhân viên mới
        /// </returns>
        /// CreatedBy NDVan (16/03/2022)
        public string GetNewEmployeeCode();

    }
}
