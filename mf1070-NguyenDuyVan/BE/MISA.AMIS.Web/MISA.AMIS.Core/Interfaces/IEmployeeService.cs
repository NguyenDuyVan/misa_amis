using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Tìm kiếm thông tin nhân viên theo Mã hoặc Họ Tên
        /// </summary>
        /// <param name="employeeFilter">Id nhân viên cần lấy dữ liệu</param>
        /// <param name="pageSize">Id nhân viên cần lấy dữ liệu</param>
        /// <param name="pageNumber">Id nhân viên cần lấy dữ liệu</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public object GetPaging(string? employeeFilter, int? pageSize, int? pageNumber);


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entitiesId">Danh sách Id những đối tượng cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int DeleteMulti(List<string> entitiesId);

        /// <summary>
        /// Xuất Danh sách nhân viên ra file exel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy NDVan (04/03/2022)
        public MemoryStream ExportData();
    }
}
