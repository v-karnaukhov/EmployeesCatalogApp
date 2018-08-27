using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesCatalog.Data.Data.Abstract;
using EmployeesCatalog.Data.Data.Entities;
using EmployeesCatalog.Data.Entities;
using EmployeesCatalog.Data.Specifications.Employees;
using EmployeesCatalog.Web.Extensions;
using EmployeesCatalog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ICollection<EmployeeModel>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            return _mapper.Map<ICollection<Employee>, ICollection<EmployeeModel>>(employees);
        }


        [HttpGet]
        [ExactQueryParam("filter", "sortDirection", "pageNumber", "pageSize")]
        public async Task<EmployeePagingModel> GetAllPaging(
            [FromQuery]string filter,
            [FromQuery]string sortDirection,
            [FromQuery]int pageNumber = 0,
            [FromQuery]int pageSize = 5)
        {
            var employees = await _unitOfWork.Employees
                .FindBy(new EmployeeByFullNamePart(filter), x => x.Department)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(employees);

            return _mapper.Map<IEnumerable<EmployeeModel>, EmployeePagingModel>(result, opt =>
                opt.AfterMap((src, dest) => dest.Count = employees.Count));
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Incorrect Employee id");
            }

            var entry = await _unitOfWork.Employees.FindAsync(x => x.EmployeeId == id, inc => inc.Department);
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

            // если департамент не совпадает с текущим, то создаем запись в истории.
            if (entryFromDb.DepartmentId != employeeModel.DepartmentId)
            {
                _unitOfWork.EmployeeChangeDepartmentHistory.Add(new EmployeeDepartmentsChangesHistory
                {
                    CurrentDepartmentId= entryFromDb.DepartmentId,
                    NewDepartmentId = employeeModel.DepartmentId,
                    EmployeeId = entryFromDb.EmployeeId,
                    ChangeDate = DateTime.Now
                });
            }

            entryFromDb.BirthDate = employeeModel?.BirthDate;
            entryFromDb.DepartmentId = employeeModel.DepartmentId;
            entryFromDb.Sex = employeeModel.Sex;
            entryFromDb.Email = employeeModel.Email;
            entryFromDb.Surname = employeeModel.Surname;
            entryFromDb.FirstName = employeeModel.FirstName;
            entryFromDb.Patronymic = employeeModel.Patronymic;
            entryFromDb.IsActual = employeeModel.IsActual;

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

        [HttpGet("{id:int}/departmentsChangeHistory")]
        public async Task<IActionResult> GetDepartmentChangeHistory(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var changeHistory =
                await _unitOfWork.EmployeeChangeDepartmentHistory.FindByAsync(
                    x => x.EmployeeId == id,
                    inc => inc.CurrentDepartment,
                    inc => inc.NewDepartment);

            return new ObjectResult(changeHistory);
        }
    }
}
