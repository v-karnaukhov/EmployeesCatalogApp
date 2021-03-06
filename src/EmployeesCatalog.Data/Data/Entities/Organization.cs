﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesCatalog.Data.Entities
{
    /// <summary>
    /// Описывает запись о сущности "Организация" в БД.
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Уникальный идентификатор записи в БД.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }

        /// <summary>
        /// Наименование организации.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Перечисление подразделений компании.
        /// </summary>
        public IEnumerable<Department> Departments { get; set; }
    }
}
