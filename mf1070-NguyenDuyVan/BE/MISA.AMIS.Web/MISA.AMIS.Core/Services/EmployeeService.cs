using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using MISA.AMIS.Core.Resource;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        /// <summary>
        /// Khởi tạo đối tượng 
        /// </summary>
        /// CreatedBy NDVan (13/03/2022)
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Thực hiện override validate dữ liệu cho từng đối tượng đặc thù
        /// </summary>
        /// <param name="employee"> Đối tượng cần validate</param>
        /// <returns>
        /// true - nếu hợp lệ
        /// false - nếu không hợp lệ
        /// </returns>
        /// CreatedBy NDVan(14/03/2022)
        protected override void ValidateCustom(Employee employee)
        {
            //    // 1.4 Check trùng mã nhân viên
            if (isInsert == true && CheckEmployeeCode(employee.EmployeeCode) > 0)
            {
                errorData.Add("EmployeeCode", Resource.ResourceVN.Error_EmployeeCodeExits);
            }
        }

        /// <summary>
        /// Thực hiện check mã nhân viên có trùng hay không
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>
        /// true - trùng, false - không trùng
        /// </returns>
        /// CreatedBy NDVan (09/03/2022)
        public int CheckEmployeeCode(string employeeCode)
        {

            var res = _employeeRepository.CheckEmployeeCode(employeeCode);
            return res;
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xử lý TH pageSize = 0 hoặc Null
        /// </summary>
        /// <param name="employeeId">Id nhân viên cần sửa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public object GetPaging(string? employeeFilter, int? pageSize, int? pageNumber)
        {
            if (pageSize is null || pageSize == 0)
            {
                // gán giá trị mặc định là 20
                pageSize = 20;
            }
            var employees = _employeeRepository.GetPaging(employeeFilter, pageSize, pageNumber);
            return employees;
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entitiesId">Danh sách Id những đối tượng cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int DeleteMulti(List<string> entitiesId)
        {
            // Khai báo chuỗi chứa tất cả id cần xóa
            string entitiesIdText = string.Empty;
            foreach (var item in entitiesId)
            {
                entitiesIdText += $"{item}, ";
            }
            var res = _employeeRepository.DeleteMulti(entitiesIdText);
            return res;
        }


        public MemoryStream ExportData()
        {
            // lấy dữ liệu nhân viên và các thuộc tính
            var employees = _employeeRepository.Get();
            var props = typeof(Employee).GetProperties();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            MemoryStream stream;
            using (var excelPackage = new ExcelPackage())
            {

                // Thêm sheet exel
                excelPackage.Workbook.Worksheets.Add(ResourceVN.ExelSheetTitle);
                var workSheet = excelPackage.Workbook.Worksheets[ResourceVN.ExelSheetTitle];

                // Định dạng file Exel
                // 1.Đặt tên cột Số thứ tự
                workSheet.Cells[3, 1].Value = "STT";

                List<string> columns = new List<string>();
                foreach (var prop in props)
                {
                    var propExcel = Attribute.IsDefined(prop, typeof(ExelColumnName));
                    if (propExcel)
                    {
                        var displayNameAttribute = Attribute.GetCustomAttribute(prop, typeof(PropertyNameDisplay));

                        if (displayNameAttribute != null)
                        {
                            columns.Add(prop.Name);
                            workSheet.Cells[3, columns.Count + 1].Value = (displayNameAttribute as PropertyNameDisplay).propertyName;
                        }
                    }
                }

                // 2.Định dạng font chữ, màu cho title và header
                workSheet.Cells[1, 1, 3, columns.Count + 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1, 3, columns.Count + 1].Style.Font.Name = "Arial";
                workSheet.Cells[1, 1, 3, columns.Count + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // 2.1 Đặt font size cho header
                workSheet.Cells[3, 1, 3, columns.Count + 1].Style.Font.Size = 10;

                // 2.2. đặt tiêu đề cho sheet
                workSheet.Cells[1, 1].Value = ResourceVN.ExelSheetTitle;
                workSheet.Cells[1, 1, 1, columns.Count + 1].Merge = true;

                // 2.2.1 Đặt khoảng trắng dưới tiêu đề
                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1, 2, columns.Count + 1].Merge = true;

                // 2.3 Đặt font size cho title
                workSheet.Cells[1, 1, 2, columns.Count + 1].Style.Font.Size = 16;


                // 3.Tạo dữ liệu trong trang exel
                // Dữ liệu nhân viên đầu tiên bắt đầu từ hàng 4
                int rowIndex = 4;

                // Đặt font size và kiểu chữ
                workSheet.Cells[4, 1, employees.Count() + 3, columns.Count + 1].Style.Font.Size = 11;
                workSheet.Cells[4, 1, employees.Count() + 3, columns.Count + 1].Style.Font.Name = "Times New Roman";
                workSheet.Cells[4, 1, employees.Count() + 3, columns.Count + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                foreach (var employee in employees)
                {
                    // Dữ liệu nhân viên bắt dầu từ cột 1
                    int colIndex = 1;
                    // Đặt số thứ tự cho từ hàng data nhân viên
                    workSheet.Cells[rowIndex, colIndex].Value = rowIndex - 3;
                    foreach (var col in columns)
                    {
                        colIndex++;
                        var prop = employee.GetType().GetProperty(col);
                        var propValue = prop.GetValue(employee);
                        // kiểm tra Datetime
                        if (typeof(DateTime?).IsAssignableFrom(prop.PropertyType))
                        {
                            if (propValue != null)
                            {
                                workSheet.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                workSheet.Cells[rowIndex, colIndex].Value = ((DateTime)propValue).ToShortDateString();
                            }
                            else
                            {
                                workSheet.Cells[rowIndex, colIndex].Value = "";
                            }

                        }
                        else if (prop.Name == "EmployeeName")
                        {
                            workSheet.Cells[rowIndex, colIndex].Value = propValue.ToString().ToUpper();
                        }
                        else
                        {
                            workSheet.Cells[rowIndex, colIndex].Value = propValue;
                        }

                    }
                    rowIndex++;
                }

                // Đặt border
                var border = workSheet.Cells[3, 1, employees.Count() + 3, columns.Count + 1].Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells.AutoFitColumns();

                // Lưu thông tin
                excelPackage.Save();
                stream = new MemoryStream(excelPackage.GetAsByteArray());
            }
            return stream;
        }
    }
}
