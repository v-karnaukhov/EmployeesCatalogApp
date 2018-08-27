import { Component, OnInit } from "@angular/core";
import { Employee } from "../../Data/Employee";
import { ActivatedRoute } from "@angular/router";
import { Location } from "@angular/common";
import { EmployeesService } from "../../services/employees.service";
import { OrganizationsService } from '../../services/organizations.service';
import { Department } from '../../Data/Department';
import {
  FormBuilder,
  Validators,
  FormGroup,
  FormControl
} from "@angular/forms";
import { Organization } from "../../Data/Organization";
import {Router} from '@angular/router';
import { EmployeeDepartmentChangeHistory } from '../../Data/EmployeeDepartmentChangeHistory';

@Component({
  selector: "app-employee-details",
  templateUrl: "./employee-details.component.html",
  styleUrls: ["./employee-details.component.css"]
})
export class EmployeeDetailsComponent implements OnInit {
  employee: Employee;
  form: FormGroup;
  sexList = [
    { value: 0, viewValue: "Не определено" },
    { value: 1, viewValue: "Мужской" },
    { value: 2, viewValue: "Женский" }
  ];
  employee_validation_messages = {
    firstName: [
      { type: "required", message: "Имя обязательно для заполнения" },
      { type: "minlength", message: "Имя дожно состоять не менее чем из 5 символов" },
      { type: "maxlength", message: "Имя не может включать более 25 символов" },
      { type: "pattern", message: "Имя должно состоять только из символов русского алфавита" }
    ],
    surname: [
      { type: "required", message: "Фамилия обязательна для заполнения" },
      { type: "minlength", message: "Фамилия дожно состоять не менее чем из 5 символов" },
      { type: "maxlength", message: "Фамилия не может включать более 25 символов" },
      { type: "pattern", message: "Фамилия должна состоять только из символов русского алфавита" }
    ],
    email: [
      { type: "required", message: "Email обязательно для заполнения" },
      { type: "email", message: "Введите корректный email" }
    ],
  };

  departmentOptions: Department[] = [];
  isNewEmployeeMode: boolean = false;
  organizationId: number = 0;
  organizations: Organization[];
  departmentsChangeHistory: EmployeeDepartmentChangeHistory[] = [];

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeesService,
    private organizationService: OrganizationsService,
    private location: Location,
    private fb: FormBuilder,
    private router: Router
  ) {

    this.isNewEmployeeMode = this.route.snapshot.paramMap.get("id") == "add";

  }

  ngOnInit() {
    if(!this.isNewEmployeeMode) {
        this.getEmployee();
    } else {
      this.getOrganizations();
    }

    this.createForm();
  }

  /**
   *  Создает форму сотрудника.
   * */
  createForm() {
    this.form = this.fb.group({
      firstName: [
        "",
        Validators.compose([
          Validators.required,
          Validators.pattern(/[А-я]/),
          Validators.minLength(5),
          Validators.maxLength(25)
        ])
      ],
      surname: [
        "",
        Validators.compose([Validators.required, Validators.pattern(/[А-я]/),Validators.minLength(5)])],
      patronymic: [""],
      email: ["", Validators.compose([Validators.required, Validators.email])],
      sex: [0],
      birthDate: [],
      isActual: [false],
      departmentId: [],
      employeeId: []
    });
  }

  getEmployee(): void {

    let id = this.route.snapshot.paramMap.get("id");
    this.employeeService.getEmployee(+id).subscribe(employee => {
      this.employee = employee;
      this.initializeForm();
      this.getDepartments(employee.organizationId)
      this.getEmployeeDepartmentsChangeHistory(employee.employeeId);
    });

  }

  getDepartments(organizationId: number): void {
    this.organizationService.getOrganizationDepartments(organizationId)
    .subscribe(departments => {
      this.departmentOptions = departments
    });
  }

  getOrganizations(): void {
    this.organizationService.getAllOrganization()
    .subscribe(organizations => this.organizations = organizations);
  }

  getEmployeeDepartmentsChangeHistory(id: number): void {

    this.employeeService.getEmployeeDepartmentsChangeHistory(id)
    .subscribe(history => this.departmentsChangeHistory = history);

  }

  onOrganizationChange(organizationId: number): void {
    this.organizationId = organizationId;
    this.getDepartments(organizationId);
  }

  initializeForm(): void {
    this.form.patchValue({
      firstName: this.employee.firstName,
      surname: this.employee.surname,
      patronymic: this.employee.patronymic,
      email: this.employee.email,
      departmentId: this.employee.departmentId,
      employeeId: this.employee.employeeId,
      sex: this.employee.sex,
      birthDate: this.employee.birthDate,
      isActual: this.employee.isActual
    });
  }

  goBack(): void {

    this.router.navigate(["/employees"]);

  }

  save(): void {
    if (this.form.invalid) {
      // TODO: Выдать оповещение.
      return;
    }

    if(this.isNewEmployeeMode) {
      this.employeeService.addEmployee(this.form.value).subscribe();
    } else {
      this.employeeService.updateEmployee(this.form.value).subscribe();
    }

    this.router.navigate(["/employees"]);
  }
}
