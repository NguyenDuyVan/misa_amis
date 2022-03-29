using Dapper;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// trả về mã nhân viên mới
        /// </returns>
        /// CreatedBy NDVan (16/03/2022)
        public string GetNewEmployeeCode()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_GetNewEmployeeCode";
                var parameter = new DynamicParameters();

                // 2.2 Thực hiện xóa nhân viên
                var res = connection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);

                return res;
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
        public IEnumerable<Guid> GetAllEmployeesId()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_GetAllEmployeesId";

                // 2.2 Thực hiện xóa nhân viên
                var res = connection.Query<Guid>(sqlCommand, commandType: CommandType.StoredProcedure);

                return res;
            }
            
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có trùng không
        /// </summary>
        /// <param name="EmployeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// CreatedBy NDVan (10/03/2022)
        public int CheckEmployeeCode(string EmployeeCode)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_CheckEmployeeCode";
                var parameter = new DynamicParameters();
                parameter.Add("@EmployeeCode", EmployeeCode);

                // 2.2 Thực hiện xóa nhân viên
                var res = connection.ExecuteScalar<int>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                return res;
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Chuỗi tất cả Id cần xóa</param>
        /// <returns>
        /// Số đối tượng bị xóa
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int DeleteMulti(string entitiesIdText)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"Proc_DeleteMultiEmployees";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add($"@EmployeesIdText", entitiesIdText);

                // 2.2 Thực hiện xóa dữ liệu
                var res = connection.Execute(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                // 3. Trả kết quả về cho Client
                return res;
            }
        }
    }
}
