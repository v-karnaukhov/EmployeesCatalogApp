using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesCatalog.Data.Entities
{
    /// <summary>
    /// Описывает запись о сущности "Сотрудник" в БД.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Уникальный идентификатор экземпляра сущности.
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество сотрудника.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Электронный ящик сотрудника.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Идентификатор записи о департаменте, к которому относится сотрудник.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Экземпляр <see cref="Organization"/>, в котором числится сотрудник.
        /// </summary>
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        /// <summary>
        /// Организация, в которой работает сотрудник.
        /// </summary>
        [NotMapped]
        public Organization Organization { get; set; }
    }
}
