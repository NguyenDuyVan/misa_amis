using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    public interface IBaseService<MISAEntity> where MISAEntity : class
    {
        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Thêm thông tin 
        /// </summary>
        /// <param name="entity">Thông tin đối tượng cần thêm</param>
        /// <returns>
        /// 201 - Thêm thành công
        /// 204 - Không có thông tin
        /// 400 - Dữ liệu đầu vào sai
        /// </returns>
        /// param
        /// CreatedBy NDVan (04/03/2022)
        public int Insert(MISAEntity entity);

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Sửa thông tin đối tượng theo Id
        /// </summary>
        /// <param name="departmentId">Id đối tượng cần sửa</param>
        /// <returns>
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        public int Update(Guid entityId, MISAEntity entity);

        
    }
}
