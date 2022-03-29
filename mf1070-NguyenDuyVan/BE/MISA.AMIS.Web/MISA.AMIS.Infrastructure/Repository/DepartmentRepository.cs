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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        /// <summary>
        /// Kiểm tra mã đơn vị có trùng không
        /// </summary>
        /// <param name="DepartmentCode">Mã đơn vị</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy NDVan (10/03/2022)
        public int CheckDepartmentCode(string DepartmentCode)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = "Proc_CheckDepartmentCode";
                var parameter = new DynamicParameters();
                parameter.Add("@DepartmentCode", DepartmentCode);

                // 2.2 Thực hiện xóa nhân viên
                var res = connection.Execute(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                return res;
            }
        }
    }
}
