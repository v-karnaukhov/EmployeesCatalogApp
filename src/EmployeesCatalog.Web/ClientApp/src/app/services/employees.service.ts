import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { MessagesService as MessageService } from "./messages.service";
import { Employee } from "../Data/Employee";
import { EmployeePagingModel } from "../Data/EmployeePagingModel";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json" })
};

@Injectable({ providedIn: "root" })
export class EmployeesService {
  private employeesUrl = "api/Employees"; // URL to web api

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  /** GET Employees from the server */
  getEmployeesAll(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeesUrl).pipe(
      tap(employees => this.log("fetched Employees")),
      catchError(this.handleError("getEmployees", []))
    );
  }

  getEmployees(
    filter: string = '',
    sortDirection: string = 'asc',
    pageIndex: number = 0,
    pageSize: number = 3
  ): Observable<EmployeePagingModel> {
    return this.http
      .get<EmployeePagingModel>(`${this.employeesUrl}`, {
        params: new HttpParams()
          .set("filter", filter)
          .set("sortDirection", sortDirection)
          .set("pageNumber", pageIndex.toString())
          .set("pageSize", pageSize.toString())
      })
      .pipe(
        tap(employees => this.log("fetched Employees")),
        catchError(this.handleError("getEmployees", new EmployeePagingModel()))
      );
  }

  /** GET Employee by id. Return `undefined` when id not found */
  getEmployeeNo404<Data>(id: number): Observable<Employee> {
    const url = `${this.employeesUrl}/${id}`;

    return this.http.get<Employee[]>(url).pipe(
      map(employees => employees[0]),
      tap(h => {
        const outcome = h ? `fetched` : `did not find`;
        this.log(`${outcome} Employee id=${id}`);
      }),
      catchError(this.handleError<Employee>(`getEmployee id=${id}`))
    );
  }

  /** GET Employee by id. Will 404 if id not found */
  getEmployee(id: number): Observable<Employee> {
    const url = `${this.employeesUrl}/${id}`;

    return this.http.get<Employee>(url).pipe(
      tap(_ => this.log(`fetched Employee id=${id}`)),
      catchError(this.handleError<Employee>(`getEmployee id=${id}`))
    );
  }

  //////// Save methods //////////

  /** POST: add a new Employee to the server */
  addEmployee(employee: Employee): Observable<Employee> {
    return this.http
      .post<Employee>(this.employeesUrl, employee, httpOptions)
      .pipe(
        tap((employee: Employee) =>
          this.log(`added Employee w/ id=${employee.employeeId}`)
        ),
        catchError(this.handleError<Employee>("addEmployee"))
      );
  }

  /** DELETE: delete the Employee from the server */
  deleteEmployee(employee: Employee | number): Observable<Employee> {

    const id = typeof employee === "number" ? employee : employee.employeeId;
    const url = `${this.employeesUrl}/${id}`;

    return this.http.delete<Employee>(url, httpOptions).pipe(
      tap(_ => this.log(`deleted Employee id=${id}`)),
      catchError(this.handleError<Employee>("deleteEmployee"))
    );
  }

  /** PUT: update the Employee on the server */
  updateEmployee(employee: Employee): Observable<any> {

    const id = employee.employeeId;
    const url = `${this.employeesUrl}/${id}`;

    return this.http.put(url, employee, httpOptions).pipe(
      tap(_ => this.log(`updated Employee id=${employee.employeeId}`)),
      catchError(this.handleError<any>("updateEmployee"))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a EmployeesService message with the MessagesService */
  private log(message: string) {
    this.messageService.add(`EmployeesService: ${message}`);
  }
}
