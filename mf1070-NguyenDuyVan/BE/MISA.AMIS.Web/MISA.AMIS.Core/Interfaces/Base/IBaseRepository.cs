using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    public interface IBaseRepository<MISAEntity> where MISAEntity : class
    {
        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public IEnumerable<MISAEntity>Get();

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy thông tin đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id nhân viên cần lấy dữ liệu</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public MISAEntity GetById(Guid entityId);

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Thêm thông tin đối tượng
        /// </summary>
        /// <param name="department">Thông tin đối tượng cần thêm</param>
        /// <returns>
        /// 201 - Thêm thành công
        /// 204 - Không có thông tin
        /// 400 - Dữ liệu đầu vào sai
        /// </returns>
        /// param
        /// CreatedBy NDVan (04/03/2022)
        public int Insert(MISAEntity department);

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Sửa thông tin nhân viên theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng cần sửa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int Update(Guid entityId, MISAEntity department);


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng cần xóa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int Delete(Guid entityId);


        
    }
}
