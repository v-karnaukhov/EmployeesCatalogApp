using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesCatalog.Web.Models
{
    /// <summary>
    /// Описывает модель данных сущности "Сотрудник".
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Уникальный идентификатор экземпляра сущности.
        /// </summary>
        public int? EmployeeId { get; set; }

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
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пол сотрудника.
        /// </summary>
        public short Sex { get; set; }

        /// <summary>
        /// Дата рождения сотрудника.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Актуальность записи о сотрудники.
        /// </summary>
        public bool? IsActual { get; set; }

        /// <summary>
        /// Идентификатор записи о департаменте, к которому относится сотрудник.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Уникальный идентификатор организации, к которой относится департамент, к которому относится сотрудник.
        /// </summary>
        public int? OrganizationId { get; set; }
    }
}
