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

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeesService,
    private organizationService: OrganizationsService,
    private location: Location,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.getEmployee();
    this.createForm();
  }

  /** Создание формы*/
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
      this.getDepartments(employee.organizationId);
    });
  }

  getDepartments(organizationId: number): void {
    this.organizationService.getOrganizationDepartments(organizationId)
    .subscribe(departments => this.departmentOptions = departments);
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
    this.location.back();
  }

  save(): void {
    if (this.form.invalid) {
      // TODO: Выдать оповещение.
      return;
    }

    this.employeeService.updateEmployee(this.form.value).subscribe();
  }
}
