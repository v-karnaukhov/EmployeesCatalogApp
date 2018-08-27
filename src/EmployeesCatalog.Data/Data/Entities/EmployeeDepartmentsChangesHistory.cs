using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeesCatalog.Data.Entities;

namespace EmployeesCatalog.Data.Data.Entities
{
    /// <summary>
    /// Описывает сущность "История изменений департаментов сотрудника".
    /// </summary>
    public class EmployeeDepartmentsChangesHistory
    {
        [Key]
        public int EntryId { get; set; }

        /// <summary>
        /// Идентификатор записи о сотруднике.
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Уникальный идентификатор текущего подразделения сотрудника(на момент изменения).
        /// </summary>
        [Required]
        public int CurrentDepartmentId { get; set; }

        /// <summary>
        /// Уникальный идентификатор нового подразделения сотрудника(на момент изменения).
        /// </summary>
        [Required]
        public int NewDepartmentId { get; set; }

        /// <summary>
        /// Дата внесения изменения.
        /// </summary>
        [Required]
        public DateTime ChangeDate { get; set; }

        [ForeignKey("CurrentDepartmentId")]
        public virtual Department CurrentDepartment { get; set; }

        [ForeignKey("NewDepartmentId")]
        public virtual Department NewDepartment { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
