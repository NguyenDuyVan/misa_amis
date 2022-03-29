using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Web.Models;
using MySqlConnector;
using System.Data;
using System.Dynamic;

namespace MISA.AMIS.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Khai báo thông tin Database
                var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
                // 1. Khởi tạo kết nối với MariaDb:
                var connection = new MySqlConnection(connectionString);

                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_GetEmployee";

                // 2.2 Thực hiện lấy dữ liệu
                var employees = connection.Query<Employee>(sqlCommand, commandType: CommandType.StoredProcedure);

                // 3. Trả kết quả về cho Client
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="EmployeeId">Id nhân viên cần lấy dữ liệu</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("{EmployeeId}")]
        public IActionResult Get(Guid EmployeeId)
        {
            try
            {
                // Khai báo thông tin Database
                var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
                // 1. Khởi tạo kết nối với MariaDb:
                var connection = new MySqlConnection(connectionString);

                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_GetEmployeeById";
                var parameter = new DynamicParameters();
                parameter.Add("@EmployeeId", EmployeeId);

                // 2.2 Thực hiện lấy dữ liệu
                var employees = connection.QuerySingleOrDefault<Employee>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                // 3. Trả kết quả về cho Client
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Tìm kiếm thông tin nhân viên theo Mã hoặc Họ Tên
        /// </summary>
        /// <param name="EmployeeFilter">Id nhân viên cần lấy dữ liệu</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("filter")]
        public IActionResult Get(string? EmployeeFilter, int? PageSize, int?PageNumber)
        {
            try
            {
                // Khai báo thông tin Database
                var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
                // 1. Khởi tạo kết nối với MariaDb:
                var connection = new MySqlConnection(connectionString);

                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_GetEmployeePaging";
                var parameter = new DynamicParameters();
                parameter.Add("@EmployeeFilter", EmployeeFilter);
                parameter.Add("@PageSize", PageSize);
                parameter.Add("@PageNumber", PageNumber);
                parameter.Add("@TotalRecord", DbType.Int32, direction: ParameterDirection.Output);

                // 2.2 Thực hiện lấy dữ liệu
                var employees = connection.Query<Employee>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);
                var TotalRecord = parameter.Get<int>("@TotalRecord");

                // 3. Trả kết quả về cho Client
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm</param>
        /// <returns>
        /// 201 - Thêm thành công
        /// 204 - Không có thông tin
        /// 400 - Dữ liệu đầu vào sai
        /// </returns>
        /// param
        /// CreatedBy NDVan (04/03/2022)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                var error = new ErrorService();
                // 1. Validate dữ liệu

                // 1.1 Check mã nhân viên có trống không
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeCodeEmpty;
                    return BadRequest(error);
                }

                // 1.2 Check họ và tên nhân viên có trống không
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeNameEmpty;
                    return BadRequest(error);
                }

                // 1.3 Check đơn vị có trống không
                if (employee.DepartmentId == null || employee.DepartmentId == Guid.Empty)
                {
                    error.UserMsg = Resource.ResourceVN.Error_DepartmentNameEmpty;
                    return BadRequest(error);
                }

                // 1.4 Check trùng mã nhân viên
                if (CheckEmployeeCode(employee.EmployeeCode) > 0)
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeCodeExits;
                    return BadRequest(error);
                }

                // Khai báo thông tin Database
                var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
                // 2. Khởi tạo kết nối với MariaDb:
                var connection = new MySqlConnection(connectionString);

                // 3. Câu lệnh truy vấn thêm nhân viên
                var sqlCommand = "Proc_InsertEmployee";

                // 4. Thực hiện thêm nhân viên
                var res = connection.Execute(sqlCommand, employee, commandType: CommandType.StoredProcedure);

                return (res > 0) ? StatusCode(201, res) : Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Sửa thông tin nhân viên theo Id
        /// </summary>
        /// <param name="EmployeeId">Id nhân viên cần sửa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpPut("{EmployeeId}")]
        public IActionResult Put(Guid EmployeeId, [FromBody] Employee employee)
        {
            try
            {
                var error = new ErrorService();
                // 1. Validate dữ liệu

                // 1.1 Check mã nhân viên có trống không
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeCodeEmpty;
                    return BadRequest(error);
                }

                // 1.2 Check họ và tên nhân viên có trống không
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeNameEmpty;
                    return BadRequest(error);
                }

                // 1.3 Check đơn vị có trống không
                if (employee.DepartmentId == null || employee.DepartmentId == Guid.Empty)
                {
                    error.UserMsg = Resource.ResourceVN.Error_DepartmentNameEmpty;
                    return BadRequest(error);
                }

                // 1.4 Check trùng mã nhân viên
                if (CheckEmployeeCode(employee.EmployeeCode) > 1)
                {
                    error.UserMsg = Resource.ResourceVN.Error_EmployeeCodeExits;
                    return BadRequest(error);
                }

                // Khai báo thông tin Database
                var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
                // 1. Khởi tạo kết nối với MariaDb:
                var connection = new MySqlConnection(connectionString);

                // 2. Câu lệnh truy vấn thêm nhân viên
                var sqlCommand = "Proc_UpdateEmployee";
                employee.EmployeeId = EmployeeId;

                // 3. Thực hiện sửa nhân viên
                var response = connection.Execute(sqlCommand, employee, commandType: CommandType.StoredProcedure);

                // 4. Trả thông tin về cho Client
                return Ok(response);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="EmployeeId">Id nhân viên cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpDelete("{EmployeeId}")]
        public IActionResult Delete(Guid EmployeeId)
        {
            // Khai báo thông tin Database
            var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
            // 1. Khởi tạo kết nối với MariaDb:
            var connection = new MySqlConnection(connectionString);

            // 2. Lấy dữ liệu
            // 2.1 Câu lệnh truy vấn lấy dữ liệu
            var sqlCommand = "Proc_DeleteEmployee";
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeId", EmployeeId);

            // 2.2 Thực hiện xóa nhân viên
            var employees = connection.Query<Employee>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

            // 3. Trả kết quả về cho Client
            return Ok(employees);
        }

        public int CheckEmployeeCode(string EmployeeCode)
        {
            // Khai báo thông tin Database
            var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
            // 1. Khởi tạo kết nối với MariaDb:
            var connection = new MySqlConnection(connectionString);

            // 2. Lấy dữ liệu
            // 2.1 Câu lệnh truy vấn lấy dữ liệu
            var sqlCommand = "Proc_CheckEmployeeCode";
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeCode", EmployeeCode);

            // 2.2 Thực hiện xóa nhân viên
            var res = connection.Execute(sqlCommand, parameter, commandType: CommandType.StoredProcedure );

            return res;
        }

        /// <summary>
        /// Xử lý Exception
        /// </summary>
        /// <param name="ex">Object Exception</param>
        /// <returns></returns>
        public IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.Data = ex.Data;
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }
    }
}
