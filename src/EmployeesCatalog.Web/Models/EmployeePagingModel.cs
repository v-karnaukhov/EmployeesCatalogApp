using System.Collections.Generic;

namespace EmployeesCatalog.Web.Models
{
    public class EmployeePagingModel
    {
        public int Count { get; set; }
        public IEnumerable<EmployeeModel> Data { get; set; }
    }
}
