using System;
using System.Linq;
using System.Linq.Expressions;
using EmployeesCatalog.Data.Concrete;
using EmployeesCatalog.Data.Data.Abstract;
using EmployeesCatalog.Data.Data.Concrete;
using EmployeesCatalog.Data.Entities;
using EmployeesCatalog.Data.Specifications.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remotion.Linq.Clauses;

namespace EmployeesCatalog.Data.Tests
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private EmployeesContext _context;
        private UnitOfWork _unitOfWork;
        private readonly string _inMemoryDbName = "EmployeesCatalogInMemoryTests";

        [TestInitialize]
        public void Initialize()
        {
            _context = CreateInMemoryDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        private EmployeesContext CreateInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<EmployeesContext>();
            builder.UseInMemoryDatabase(databaseName: _inMemoryDbName);

            var context = new EmployeesContext(builder.Options);

            return context;
        }

        private IUnitOfWork CreateInMemoryUnitOfWork()
        {
            return new UnitOfWork(CreateInMemoryDbContext());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void RepositoryAddMethod_MustAddEntryToDb()
        {
            var testOrganizationName = "TestOrganization1";
            _unitOfWork.Organizations.Add(new Organization {Name = testOrganizationName});
            _unitOfWork.Save();

            var newContext = CreateInMemoryDbContext();
            var resultOrganization = newContext.Organizations.FirstOrDefault(x => x.Name == testOrganizationName);

            Assert.IsNotNull(resultOrganization);
            Assert.AreEqual(testOrganizationName, resultOrganization.Name);
        }

        [TestMethod]
        public void RepositoryUpdateMethod_MustUpdateEntryInDb()
        {
            var testorganizationName = "TestOrganization";
            var updatedOrganizationName = "Updated organization name";
            var testOrganization = new Organization { Name = testorganizationName };
            _unitOfWork.Organizations.Add(testOrganization);
            _unitOfWork.Save();

            var unitOfWorkForUpdating = new UnitOfWork(CreateInMemoryDbContext());
            testOrganization.Name = updatedOrganizationName;
            unitOfWorkForUpdating.Organizations.Update(testOrganization);
            unitOfWorkForUpdating.Save();

            var newContext = CreateInMemoryDbContext();
            var resultOrganization = newContext.Organizations.FirstOrDefault();

            Assert.IsNotNull(resultOrganization);
            Assert.AreEqual(newContext.Organizations.Count(), 1);
            Assert.AreEqual(updatedOrganizationName , resultOrganization.Name);
        }

        [TestMethod]
        public void RepositoryDeleteMethod_MustDeleteEntry()
        {
            var testOrganization1 = new Organization { Name = "TestOrganization1"};
            var testOrganization2 = new Organization { Name = "TestOrganization2"};
            var testOrganization3 = new Organization { Name = "TestOrganization3"};

            _context.Organizations.Add(testOrganization1);
            _context.Organizations.Add(testOrganization2);
            _context.Organizations.Add(testOrganization3);
            _context.SaveChanges();

            _unitOfWork.Organizations.Delete(testOrganization3);
            _unitOfWork.Save();

            var newContext = CreateInMemoryDbContext();
            var deletedEntry = newContext.Organizations.FirstOrDefault(x => x.Name == testOrganization3.Name);

            Assert.AreEqual(newContext.Organizations.Count(), 2);
            Assert.AreSame(null, deletedEntry);
        }

        [TestMethod]
        public void RepositoryGetMethod_MustReturnSearchedEntry()
        {
            var testOrganization1 = new Organization { Name = "TestOrganization1" };

            _unitOfWork.Organizations.Add(testOrganization1);
            _unitOfWork.Save();

            using (var newUnitOfWork = CreateInMemoryUnitOfWork())
            {
                var resultOrganization = newUnitOfWork.Organizations.Get(testOrganization1.OrganizationId);

                Assert.AreEqual(testOrganization1.OrganizationId, resultOrganization.OrganizationId);
                Assert.AreEqual(testOrganization1.Name, resultOrganization.Name);
            }
        }

        [TestMethod]
        public void RepositoryFindMethod_MustReturnSearchedByPredicateEntry()
        {
            var testOrganization1 = new Organization { Name = "TestOrganization1" };
            var testOrganization2 = new Organization { Name = "Organization2" };

            _unitOfWork.Organizations.Add(testOrganization1);
            _unitOfWork.Organizations.Add(testOrganization2);
            _unitOfWork.Save();

            using (var newUnitOfWork = CreateInMemoryUnitOfWork())
            {
                Expression<Func<Organization, bool>> findPredicate = x => x.Name.StartsWith("Test");
                var resultOrganization = newUnitOfWork.Organizations.Find(findPredicate);

                Assert.IsNotNull(resultOrganization);
                Assert.AreEqual(testOrganization1.Name, resultOrganization.Name);
                Assert.AreEqual(testOrganization1.OrganizationId, resultOrganization.OrganizationId);
            }
        }

        [TestMethod]
        public void RepositoryFindAllMethod_MustReturnAllSearchedByPredicateEntries()
        {
            var testOrganization1 = new Organization { Name = "TestOrganization1" };
            var testOrganization2 = new Organization { Name = "Organization2" };
            var testOrganization3 = new Organization { Name = "TestOrganization3" };

            _unitOfWork.Organizations.Add(testOrganization1);
            _unitOfWork.Organizations.Add(testOrganization2);
            _unitOfWork.Organizations.Add(testOrganization3);
            _unitOfWork.Save();

            using (var newUnitOfWork = CreateInMemoryUnitOfWork())
            {
                Expression<Func<Organization, bool>> findPredicate = x => x.Name.StartsWith("Test");
                var resultOrganizations = newUnitOfWork.Organizations.FindAll(findPredicate).ToList();

                Assert.IsNotNull(resultOrganizations);
                Assert.AreEqual(2, resultOrganizations.Count);
                Assert.AreEqual(testOrganization1.Name, resultOrganizations[0].Name);
                Assert.AreEqual(testOrganization1.OrganizationId, resultOrganizations[0].OrganizationId);
                Assert.AreEqual(testOrganization3.OrganizationId, resultOrganizations[1].OrganizationId);
                Assert.AreEqual(testOrganization3.Name, resultOrganizations[1].Name);
            }
        }

        [TestMethod]
        public void RepositoryFindByMethod_MustReturnSearchedEntry()
        {
            var testOrganization1 = new Organization { Name = "TestOrganization1" };
            var testOrganization2 = new Organization { Name = "Organization2" };
            var testOrganization3 = new Organization { Name = "TestOrganization3" };

            _unitOfWork.Organizations.Add(testOrganization1);
            _unitOfWork.Organizations.Add(testOrganization2);
            _unitOfWork.Organizations.Add(testOrganization3);
            _unitOfWork.Save();

            using (var newUnitOfWork = CreateInMemoryUnitOfWork())
            {
                var spec = new OrganizationNameStartsWith("Organization");
                var resultOrganizations = newUnitOfWork.Organizations.FindBy(spec).ToList();

                Assert.IsNotNull(resultOrganizations);
                Assert.AreEqual(1, resultOrganizations.Count);
                Assert.AreEqual(testOrganization2.Name, resultOrganizations[0].Name);
                Assert.AreEqual(testOrganization2.OrganizationId, resultOrganizations[0].OrganizationId);
            }
        }
    }
}
