using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using OfficeOpenXml;

namespace MISA.AMIS.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("newEmployeeCode")]
        public IActionResult Get()
        {
            try
            {
                string newCode = _employeeRepository.GetNewEmployeeCode();
                // 3. Trả kết quả về cho Client
                return Ok(newCode);

            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

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
        [HttpGet("filter")]
        public IActionResult Get(string? employeeFilter, int? pageSize, int? pageNumber)
        {

            try
            {
                var employees = _employeeService.GetPaging(employeeFilter, pageSize, pageNumber);
                // 3. Trả kết quả về cho Client
                return Ok(employees);

            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy tất cả Id nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("allEmployeesId")]
        public IActionResult GetAllEmployeeIds()
        {
            try
            {
                var employeesId = _employeeRepository.GetAllEmployeesId();
                // 3. Trả kết quả về cho Client
                return Ok(employeesId);

            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entitiesId">Danh sách Id đối tượng cần xóa</param>
        /// <returns>
        /// Số đối tượng bị ảnh hưởng
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpDelete]
        public IActionResult Delete([FromBody] List<string> entitiesId)
        {
            try
            {
                var res = _employeeService.DeleteMulti(entitiesId);
                return Ok(res);
            }

            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }


        /// <summary>
        /// Xuất Danh sách nhân viên ra file exel
        /// </summary>
        /// <returns>
        /// File exel danh sách nhân viên
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("exelData")]
        public async Task<IActionResult> ExportData()
        {
            try
            {
                var stream = _employeeService.ExportData();
                string excelName = "EmployeesData.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }

        }
    }
}





