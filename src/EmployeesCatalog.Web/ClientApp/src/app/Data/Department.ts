/**
 * Описывает модель данных сущности "Департамент".
 */
export class Department {
    /**
     * Идентификатор записи в БД,
     */
    public departmentId: number;

    /**
     * Наименование департамента.
     */
    public name: string;

    /**
     * Идентификатор записи сущности типа "Организация", к которой относится департамент.
     */
    public organizationId: number;
}