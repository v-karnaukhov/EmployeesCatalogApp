﻿using AutoMapper;
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

            CreateMap<Employee, EmployeeModel>();
            CreateMap<EmployeeModel, Employee>();

            #endregion
        }
    }
}
