/**
 * Описывает клиентскую модель сущности "Сотрудник".
 */
export class Employee {
        
        /**
         * Уникальный идентификатор экземпляра сущности.
         */
        public employeeId: number;

        
        /**
         * Имя сотрудника.
         */
        public firstName: string;

        /**
         * Фамилия сотрудника.
         */
        public surname: string;

        /**
         * Отчество сотрудника.
         */
        public patronymic: string;

        /**
         * Электронный ящик сотрудника.
         */
        public email: string;

        /**
         * Идентификатор записи о департаменте, к которому относится сотрудник.
         */
        public departmentId: number;
}
