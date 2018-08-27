using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EmployeesCatalog.Data.Data.Entities;
using EmployeesCatalog.Data.Entities;
using EmployeesCatalog.Web.Models;

namespace EmployeesCatalog.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Organizations

            CreateMap<Organization, OrganizationModel>();
            CreateMap<OrganizationModel, Organization>();

            #endregion

            #region Departments

            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>();

            #endregion

            #region Employees

            CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.Department.OrganizationId));
            CreateMap<EmployeeModel, Employee>();

            CreateMap<IEnumerable<EmployeeModel>, EmployeePagingModel>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()));

            #endregion

            #region Auth

            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            #endregion
        }
    }
}
