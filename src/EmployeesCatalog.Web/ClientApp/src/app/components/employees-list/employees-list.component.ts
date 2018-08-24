import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { Employee } from "../../Data/Employee";
import { EmployeesService as EmployeeService } from "../../services/employees.service";
import { MatPaginator, MatTableDataSource, MatSort } from "@angular/material";
import { Observable, of, BehaviorSubject, fromEvent, merge } from "rxjs";
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
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
  displayedColumns: string[] = [ "employeeId", "surname", "firstName", "patronymic", "email" ];
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
        debounceTime(150),
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

  //add(name: string): void {
  //  name = name.trim();
  //  if (!name) {
  //    return;
  //  }

  //  this.employeeService
  //    .addEmployee({ firstName: name } as Employee)
  //    .subscribe(hero => {
  //      this.employees.push(hero);
  //    });
  //}

  //delete(employee: Employee): void {
  //  this.employees = this.employees.filter(h => h !== employee);
  //  this.employeeService.deleteEmployee(employee).subscribe();
  //}
}
