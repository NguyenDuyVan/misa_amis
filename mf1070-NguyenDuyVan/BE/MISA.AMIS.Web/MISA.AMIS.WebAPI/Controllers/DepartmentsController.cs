using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;


namespace MISA.AMIS.WebAPI.Controllers
{
    public class DepartmentsController : MISABaseController<Department>
    {
        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentService departmentService) : base(departmentRepository, departmentService)
        {
        }
    }
}
