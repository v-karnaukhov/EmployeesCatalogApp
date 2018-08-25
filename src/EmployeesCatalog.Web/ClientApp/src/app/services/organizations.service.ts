import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { MessagesService as MessageService } from "./messages.service";
import { Employee } from "../Data/Employee";
import { EmployeePagingModel } from "../Data/EmployeePagingModel";
import { Department } from "../Data/Department";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json" })
};

@Injectable({
  providedIn: "root"
})
export class OrganizationsService {
  private employeesUrl = "api/Organizations";

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  getOrganizationDepartments(organizationId: number) {

    return this.http.get<Department[]>(`${this.employeesUrl}/${organizationId}/departments`).pipe(
      tap(employees => this.log("fetched organization departments")),
      catchError(this.handleError("getOrganizationDepartments", []))
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
      console.error(error); // log to console instead

      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`EmployeesService: ${message}`);
  }
}
