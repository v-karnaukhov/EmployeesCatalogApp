using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesCatalog.Data.Data.Abstract;
using EmployeesCatalog.Data.Entities;
using EmployeesCatalog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesCatalog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ICollection<DepartmentModel>> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();

            return _mapper.Map<ICollection<Department>, ICollection<DepartmentModel>>(departments);
        }

        [HttpGet("{id:int}", Name = "GetDepartmentById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Incorrect Department id");
            }

            var entry = await _unitOfWork.Departments.GetAsync(id);
            if (entry == null)
            {
                return NotFound($"Department with id: {id} not found");
            }

            return new ObjectResult(_mapper.Map<Department, DepartmentModel>(entry));
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentModel departmentModel)
        {
            if (departmentModel == null || !ModelState.IsValid)
            {
                return BadRequest("Department object is invalid");
            }

            var parentOrganization = await _unitOfWork.Organizations.GetAsync(departmentModel.OrganizationId);
            if (parentOrganization == null)
            {
                return BadRequest("The department should be related to the existing organization. Check OrganizationId.");
            }

            var departmentEntry = _mapper.Map<DepartmentModel, Department>(departmentModel);

            await _unitOfWork.Departments.AddAsync(departmentEntry);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute(
                "GetDepartmentById",
                new { id = departmentEntry.DepartmentId },
                _mapper.Map<Department, DepartmentModel>(departmentEntry));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, DepartmentModel departmentModel)
        {
            var entryFromDb = await _unitOfWork.Departments.GetAsync(id);
            if (entryFromDb == null || id <= 0)
            {
                return NotFound();
            }

            entryFromDb.Name = departmentModel.Name;
            _unitOfWork.Departments.Update(entryFromDb);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entryFromDb = _unitOfWork.Departments.Get(id);
            if (entryFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Departments.Delete(entryFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
