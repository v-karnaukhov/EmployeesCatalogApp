import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { Employee } from "../../Data/Employee";
import { EmployeesService as EmployeeService } from "../../services/employees.service";
import { MatPaginator, MatTableDataSource, MatSort } from "@angular/material";
import { Observable, of, BehaviorSubject, fromEvent, merge } from "rxjs";
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { EmployeePagingModel } from '../../Data/EmployeePagingModel';
import {
  catchError,
  finalize,
  debounceTime,
  distinctUntilChanged,
  startWith,
  tap,
  delay
} from "rxjs/operators";

@Component({
  selector: "app-employees-list",
  templateUrl: "./employees-list.component.html",
  styleUrls: ["./employees-list.component.css"]
})
export class EmployeesListComponent {
  displayedColumns: string[] = [
    "employeeId",
    "surname",
    "firstName",
    "patronymic",
    "email",
    "actions"
  ];
  dataSource = new MatTableDataSource();
  totalEmployeesCount: number = 0;

  constructor(private employeeService: EmployeeService) {}

  @ViewChild(MatPaginator)
  paginator: MatPaginator;

  @ViewChild(MatSort)
  sort: MatSort;

  @ViewChild("input")
  input: ElementRef;

  ngOnInit() {
    this.getEmployees();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    fromEvent(this.input.nativeElement, "keyup")
      .pipe(
        debounceTime(250),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;

          this.getEmployees();
        })
      )
      .subscribe();

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(tap(() => this.getEmployees()))
      .subscribe();
  }

  getEmployees(): void {
    this.employeeService
      .getEmployees(
        this.input.nativeElement.value,
        this.sort.direction,
        this.paginator.pageIndex,
        this.paginator.pageSize
      )
      .subscribe(employeesData => {
        this.totalEmployeesCount = employeesData.count;
        this.dataSource.data = employeesData.data;
      });
  }

  editEmployee(employee: Employee): void {}

  deleteEmployee(employee: Employee): void {
    this.employeeService.deleteEmployee(employee)
      .subscribe(_ => {
        
        let index: number = this.dataSource.data.findIndex(d => d === employee);
        console.log(this.dataSource.data.findIndex(d => d === employee));
        this.dataSource.data.splice(index,1);
  
        this.dataSource = new MatTableDataSource(this.dataSource.data);
        
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
        });

        console.log(`${employee.employeeId} remove from list`);

      });
  }
}
