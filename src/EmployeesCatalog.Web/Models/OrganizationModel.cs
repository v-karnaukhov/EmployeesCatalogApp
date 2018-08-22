using System.ComponentModel.DataAnnotations;

namespace EmployeesCatalog.Web.Models
{
    /// <summary>
    /// Описывает модель данных сущности "Организация".
    /// </summary>
    public class OrganizationModel
    {
        /// <summary>
        /// Уникальный идентификатор записи в БД.
        /// </summary>
        //[Range(1, int.MaxValue)]
        public int OrganizationId { get; set; }

        /// <summary>
        /// Наименование организации.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
