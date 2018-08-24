using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public async Task<ICollection<EmployeeModel>> GetAll()
        //{
        //    var employees = await _unitOfWork.Employees.GetAllAsync();

        //    return _mapper.Map<ICollection<Employee>, ICollection<EmployeeModel>>(employees);
        //}


        [HttpGet]
        //public async Task<ICollection<EmployeeModel>> GetAll(
        public async Task<EmployeePagingModel> GetAll(
            [FromQuery]string filter,
            [FromQuery]string sortDirection,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize)
        {
            // TODO: сделать IQuerable + Отдельный Count запрос.
            var employees = await _unitOfWork.Employees.FindAllAsync(x => x.FirstName.Contains(filter));
            var result = employees.Skip(pageNumber * pageSize).Take(pageSize);

            return _mapper.Map<IEnumerable<Employee>, EmployeePagingModel>(result, opt =>
                opt.AfterMap((src, dest) => dest.Count = employees.Count));
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Incorrect Employee id");
            }

            var entry = await _unitOfWork.Employees.GetAsync(id);
            if (entry == null)
            {
                return NotFound($"Employee with id: {id} not found");
            }

            return new ObjectResult(_mapper.Map<Employee, EmployeeModel>(entry));
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            if (employeeModel == null || !ModelState.IsValid)
            {
                return BadRequest("Employee object is invalid");
            }

            var parentDepartment = await _unitOfWork.Departments.GetAsync(employeeModel.DepartmentId);
            if (parentDepartment == null)
            {
                return BadRequest("The employee should be related to the existing department. Check DepartmentId.");
            }

            var employeeEntry = _mapper.Map<EmployeeModel, Employee>(employeeModel);

            await _unitOfWork.Employees.AddAsync(employeeEntry);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute(
                "GetEmployeeById",
                new { id = employeeEntry.EmployeeId },
                _mapper.Map<Employee, EmployeeModel>(employeeEntry));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EmployeeModel employeeModel)
        {
            var entryFromDb = await _unitOfWork.Employees.GetAsync(id);
            if (entryFromDb == null || !ModelState.IsValid || id <= 0)
            {
                return NotFound();
            }

            entryFromDb.FirstName = employeeModel.FirstName;
            entryFromDb.Patronymic = employeeModel.Patronymic;
            entryFromDb.Surname = employeeModel.Surname;
            entryFromDb.Email = employeeModel.Email;
            entryFromDb.DepartmentId = employeeModel.DepartmentId;

            _unitOfWork.Employees.Update(entryFromDb);

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

            var entryFromDb = _unitOfWork.Employees.Get(id);
            if (entryFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Employees.Delete(entryFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
