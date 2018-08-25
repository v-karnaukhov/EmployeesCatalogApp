using System;
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
    public class OrganizationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ICollection<OrganizationModel>> GetAll()
        {
            var organizations = await _unitOfWork.Organizations.GetAllAsync();
            var result = _mapper.Map<ICollection<Organization>, ICollection<OrganizationModel>>(organizations);

            return result;
        }

        [HttpGet("{id:int}", Name = "GetOrganizationById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Incorrect organization id");
            }

            var entry = await _unitOfWork.Organizations.GetAsync(id);
            if (entry == null)
            {
                return NotFound($"Organization with id: {id} not found");
            }

            return new ObjectResult(_mapper.Map<Organization, OrganizationModel>(entry));
        }

        [HttpGet("{id:int}/departments", Name = "GetOrganizationDepartments")]
        public async Task<IActionResult> GetOrganizationDepartments(int id)
        {
            if (id <= 0)
            {
                return NotFound("Incorrect organization id");
            }

            var entry = await _unitOfWork.Organizations.FindAsync(x=>x.OrganizationId == id, inc => inc.Departments);
            if (entry == null)
            {
                return NotFound($"Organization with id: {id} not found");
            }

            return new ObjectResult(_mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(entry.Departments));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganizationModel organization)
        {
            if (organization == null || !ModelState.IsValid)
            {
                return BadRequest("Organization object is invalid");
            }

            var organizationEntry = _mapper.Map<OrganizationModel, Organization>(organization);

            await _unitOfWork.Organizations.AddAsync(organizationEntry);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute(
                "GetOrganizationById",
                new { id = organizationEntry.OrganizationId },
                _mapper.Map<Organization, OrganizationModel>(organizationEntry));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, OrganizationModel organizationFromClient)
        {
            var entryFromDb = await _unitOfWork.Organizations.GetAsync(id);
            if (entryFromDb == null || id <= 0)
            {
                return NotFound();
            }

            entryFromDb.Name = organizationFromClient.Name;
            _unitOfWork.Organizations.Update(entryFromDb);

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

            var entryFromDb = _unitOfWork.Organizations.Get(id);
            if (entryFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Organizations.Delete(entryFromDb);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
