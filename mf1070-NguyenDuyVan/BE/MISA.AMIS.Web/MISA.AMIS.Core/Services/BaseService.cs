using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using MISA.AMIS.Core.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        protected IDictionary errorData ;
        public bool isInsert { get; set; }

        /// <summary>
        /// Khởi tạo đối tượng 
        /// </summary>
        /// CreatedBy NDVan (13/03/2022)
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            errorData = new Dictionary<string, string>();
        }

        /// <summary>
        /// Kiểm tra thông tin đối tượng cần thêm có hợp lệ không
        /// </summary>
        /// <param name="entity">Thông tin cần thêm</param>
        /// <returns>
        /// Số đối tượng được thêm
        /// </returns>
        /// CreatedBy NDVan (13/03/2022)
        public int Insert(MISAEntity entity)
        {
            isInsert = true;
            var isValid = ValidateObject(entity);
            if (isValid)
            {
                isInsert = false;
                var entities = _baseRepository.Insert(entity);
                return entities;
            }
            else
            {
                isInsert = false;
                throw new Exception();
            }

        }

        /// <summary>
        ///  Kiểm tra thông tin đối tượng cần sửa có hợp lệ không
        /// </summary>
        /// <param name="entityId">Id đối tượng cần sửa</param>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <returns>
        /// Số đối tượng được sửa
        /// </returns>
        /// CreatedBy NDVan (13/03/2022)
        public int Update(Guid entityId, MISAEntity entity)
        {
            isInsert = false;
            var isValid = ValidateObject(entity);
            if (isValid)
            {
                var entities = _baseRepository.Update(entityId, entity);
                return entities;
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Thực hiện Validate dữ liệu 
        /// </summary>
        /// <param name="entity">Thông tin cần validate</param>
        /// <returns>
        /// true - hợp lệ
        /// false - không hợp lệ
        /// </returns>
        /// CreatedBy NDVan (13/03/2022)
        private bool ValidateObject(MISAEntity entity)
        {
            // Biến chứa thông tin lỗi validate
            errorData = new Dictionary<string, string>();
            var isValid = true;
            //  1. Quét toàn bộ các property của đối tượng có đánh dấu vào validate chung: 
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                // Lấy ra tên Props:
                var propName = prop.Name;
                // Lấy ra giá trị của Prop
                var propValue = prop.GetValue(entity);

                //Lấy ra tên của property
                var propNameAttribute = Attribute.GetCustomAttribute(prop, typeof(PropertyNameDisplay));
                if (propNameAttribute != null)
                {
                    propName = (propNameAttribute as PropertyNameDisplay).propertyName;

                }
                // Kiểm tra prop có đánh dấu validate không
                var isRequired = Attribute.IsDefined(prop, typeof(MISARequired));
                // 1.1 Kiểm tra bắt buộc nhập
                if (isRequired == true && (propValue == null || propValue.ToString() == string.Empty))
                {
                    isValid = false;
                    var requiredAttributeObject = Attribute.GetCustomAttribute(prop, typeof(MISARequired));
                    var errorMsg = (requiredAttributeObject as MISARequired).ErrorValidateMsg;
                    if (errorMsg == null)
                        errorMsg = string.Format(ResourceVN.Validate_Empty, propName);
                    errorData.Add(prop.Name, errorMsg);
                }
                // 1.2 Kiểm tra email có đúng định dạng
                var emailValid = Attribute.IsDefined(prop, typeof(EmailValid));
                if (emailValid == true)
                {
                    // Kiểm tra Email có đúng định dạng không
                    if(propValue != null)
                    {
                        isValid = IsValidEmail(propValue.ToString());
                    };
                    if (!isValid && propValue != null && propValue.ToString() != string.Empty)
                    {
                        isValid = false;
                        errorData.Add(prop.Name, ResourceVN.Error_Email);
                    }
                }

                // 1.3 Độ dài tối đa nhập

            }

            // Xử lý validate đối với từng đối tượng khác nhau
            ValidateCustom(entity);
            if (errorData.Count > 0)
            {
                throw new ValidateException("Dữ liệu không hợp lệ", errorData);
            }
            return true;
        }


        /// <summary>
        /// Thực hiện validate dữ liệu cho từng đối tượng đặc thù
        /// </summary>
        /// <param name="entity"> Đối tượng cần validate</param>
        /// <returns>
        /// true - nếu hợp lệ
        /// false - nếu không hợp lệ
        /// </returns>
        /// CreatedBy NDVan(14/03/2022)
        protected virtual void ValidateCustom(MISAEntity entity)
        {
        }


        /// <summary>
        /// Kiểm tra email có đúng định dạng không
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns></returns>
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }


        
    }
}
