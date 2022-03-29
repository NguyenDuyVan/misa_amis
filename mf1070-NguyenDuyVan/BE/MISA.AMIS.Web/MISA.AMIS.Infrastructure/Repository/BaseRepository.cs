using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> where MISAEntity : class
    {
        // Chuỗi kết nối
        protected string ConnectionString = "Server=13.229.200.157;Database=MISA.WEB01.NDVAN;Uid=dev;Pwd=12345678;Character Set=utf8";
        string tableName = String.Empty;
        public BaseRepository()
        {
            // Lấy tên bảng dữ liệu cần lấy
            tableName = typeof(MISAEntity).Name;
        }

        // Lấy dữ liệu: 
        public IEnumerable<MISAEntity> Get()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"Proc_Get{tableName}";

                // 2.2 Thực hiện lấy dữ liệu
                var entities = connection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure);

                return entities;
            }
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id cần lấy dữ liệu</param>
        /// <returns>
        /// Dữ liệu cần lấy
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public MISAEntity GetById(Guid entityId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"Proc_Get{tableName}ById";
                var parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Id", entityId);

                // 2.2 Thực hiện lấy dữ liệu
                var entities = connection.QuerySingleOrDefault<MISAEntity>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                // 3. Trả kết quả về cho Client
                return entities;
            }
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Tìm kiếm thông tin nhân viên theo Mã hoặc Họ Tên
        /// </summary>
        /// <param name="entityFilter">Id cần lấy dữ liệu</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="pageNumber">Thứ tự trang</param>
        /// <returns>
        /// Danh sách bản ghi
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public object GetPaging(string? entityFilter, int? pageSize, int? pageNumber)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"Proc_Get{tableName}Paging";
                var parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Filter", entityFilter);
                parameter.Add("@PageSize", pageSize);
                parameter.Add("@PageNumber", pageNumber);
                parameter.Add("@OffsetNumber", (pageNumber - 1) * pageSize );
                parameter.Add("@TotalRecord", DbType.Int32, direction: ParameterDirection.Output);

                // 2.2 Thực hiện lấy dữ liệu
                var entities = connection.Query<MISAEntity>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);
                var TotalRecord = parameter.Get<int>("@TotalRecord");
                var result = new { TotalRecord = TotalRecord, Data = entities };

                // 3. Trả kết quả về cho Client
                return result;
            }
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="entity">Thông tin nhân viên cần thêm</param>
        /// <returns>
        /// 201 - Thêm thành công
        /// 204 - Không có thông tin
        /// 400 - Dữ liệu đầu vào sai
        /// </returns>
        /// param
        /// CreatedBy NDVan (04/03/2022)
        public int Insert(MISAEntity entity)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {

                // 3. Câu lệnh truy vấn thêm nhân viên
                var sqlCommand = $"Proc_Insert{tableName}";

                // 4. Thực hiện thêm thông tin
                var res = connection.Execute(sqlCommand, entity, commandType: CommandType.StoredProcedure);

                return res;
            }
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Sửa thông tin nhân viên theo Id
        /// </summary>
        /// <param name="entityId">Id đôi tượng cần sửa</param>
        /// <param name="entity">Thông tin đối tượng cần sửa</param>
        /// <returns>
        /// Số đối tượng bị ảnh hưởng
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int Update(Guid entityId, MISAEntity entity)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {

                // 2. Câu lệnh truy vấn thêm nhân viên
                var sqlCommand = $"Proc_Update{tableName}";
                entity.GetType().GetProperty($"{tableName}Id").SetValue(entity, entityId);


                // 3. Thực hiện sửa nhân viên
                var res = connection.Execute(sqlCommand, entity, commandType: CommandType.StoredProcedure);

                // 4. Trả thông tin về cho Client
                return res;
            }
        }


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa nhân viên theo Id
        /// </summary>
        /// <param name="entityId">Id nhân viên cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int Delete(Guid entityId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // 2. Lấy dữ liệu
                // 2.1 Câu lệnh truy vấn lấy dữ liệu
                var sqlCommand = $"Proc_Delete{tableName}";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add($"@{tableName}Id", entityId);

                // 2.2 Thực hiện xóa dữ liệu
                var res = connection.Execute(sqlCommand, parameter, commandType: CommandType.StoredProcedure);

                // 3. Trả kết quả về cho Client
                return res;
            }
        }
    }
}
