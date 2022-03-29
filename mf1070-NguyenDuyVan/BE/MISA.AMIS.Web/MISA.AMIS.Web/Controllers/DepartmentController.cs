using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Web.Models;
using Dapper;
using MySqlConnector;
using System.Data;

namespace MISA.AMIS.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // GET: api/<DepartmentController>
        [HttpGet]
        public IActionResult Get()
        {// Khai báo thông tin Database
            var connectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
            // 1. Khởi tạo kết nối với MariaDb:
            var connection = new MySqlConnection(connectionString);

            // 2. Lấy dữ liệu
            // 2.1 Câu lệnh truy vấn lấy dữ liệu
            var sqlCommand = "Proc_GetDepartment";

            // 2.2 Thực hiện lấy dữ liệu
            var departments = connection.Query<Department>(sqlCommand, commandType: CommandType.StoredProcedure);

            // 3. Trả kết quả về cho Client
            return Ok(departments);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
