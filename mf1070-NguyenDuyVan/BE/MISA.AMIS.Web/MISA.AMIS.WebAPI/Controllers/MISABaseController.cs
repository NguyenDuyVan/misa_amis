using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using MISA.AMIS.Core.Resource;

namespace MISA.AMIS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase where MISAEntity : class
    {
        /// <summary>
        /// Khởi tạo đối tượng
        /// </summary>
        /// CreatedBy NDVan (13/03/2022)
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        public MISABaseController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// 200 - Danh sách thông tin cần lấy
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _baseRepository.Get();
                return Ok(entities);
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
        /// Lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="entityId">Id nhân viên cần lấy dữ liệu</param>
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// 204 - Không có dữ liệu
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            try
            {
                var entity = _baseRepository.GetById(entityId);
                return Ok(entity);
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
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="entity">Thông tin đối tượng cần thêm</param>
        /// <returns>
        /// 201 - Thêm thành công
        /// 204 - Không có thông tin
        /// 400 - Dữ liệu đầu vào sai
        /// </returns>
        /// param
        /// CreatedBy NDVan (04/03/2022)
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
                if (res > 0) return StatusCode(201, res);
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


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Sửa thông tin nhân viên theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng cần sửa</param>
        /// <returns>
        /// Số đối tượng bị ảnh hưởng
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpPut("{entityId}")]
        public IActionResult Put(Guid entityId, MISAEntity entity)
        {
            try
            {
                var res = _baseService.Update(entityId, entity);
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


        /// <summary>$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        /// Xóa đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng cần xóa</param>
        /// <returns>
        /// Số đối tượng bị ảnh hưởng
        /// </returns>
        /// CreatedBy NDVan (04/03/2022)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _baseRepository.Delete(entityId);
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
        /// Xử lý Exception
        /// </summary>
        /// <param name="ex">Object Exception</param>
        /// <returns></returns>
        protected IActionResult HandleValidateException(ValidateException ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = ResourceVN.Error_Exception;
            error.Data = ex.Data;
            return BadRequest(error);
        }

        /// <summary>
        /// Xử lý Exception
        /// </summary>
        /// <param name="ex">Object Exception</param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.Data = ex.Data;
            error.UserMsg = Core.Resource.ResourceVN.Error_Exception;
            return StatusCode(500, error);
        }
    }
}
