import { Department } from './Department';
/**
 * Описывает модель данных сущности "История изменения департаментов".
 */
export class EmployeeDepartmentChangeHistory {
    
    /**
     * Идентификатор строки в БД.
     */
    public entryId: number;

    /**
     * Идентификатор текущей (на момент изменения) сущности "Департамент".
     */
    public currentDepartmentId: number;

    /**
     * Текущая (на момент изменения) сущность "Департамент".
     */
    public currentDepartment: Department;

    /**
     *  Идентификатор новой (на момент изменения) сущности "Департамент".
     */
    public newDepartmentId: number;

    /**
     * Новый (на момент изменения) сущность "Департамент".
     */
    public newDepartment: Department;

    /**
     * Идентификатор сущности "Сотрудник".
     */
    public employeeId: number;

    /**
     * Дата внесения изменения.
     */
    public changeDate: Date;
}