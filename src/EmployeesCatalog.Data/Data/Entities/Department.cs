using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesCatalog.Data.Entities
{
    /// <summary>
    /// Описывает запись сущности "Подразделение компании" в БД.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Уникальный идентификатор строки данных в БД.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Наименование подразделения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уникальный идентификатор записи об организации, к которой относится подразделение.
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Родительская организация.
        /// </summary>
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        [NotMapped]
        public IEnumerable<Employee> Employees { get; set; }
    }
}
