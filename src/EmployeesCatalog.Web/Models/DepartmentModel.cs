using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesCatalog.Web.Models
{
    /// <summary>
    /// Описывает модель данных сущности "Департамент".
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// Уникальный идентификатор строки данных в БД.
        /// </summary>
        //[Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Наименование подразделения.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Уникальный идентификатор записи об организации, к которой относится подразделение.
        /// </summary>
        [Required]
        public int OrganizationId { get; set; }
    }
}
