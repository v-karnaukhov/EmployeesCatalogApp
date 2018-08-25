import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { MessagesService as MessageService } from "./messages.service";
import { Employee } from "../Data/Employee";
import { EmployeePagingModel } from "../Data/EmployeePagingModel";
import { Department } from "../Data/Department";
import { Organization } from "../Data/Organization";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json" })
};

@Injectable({
  providedIn: "root"
})
export class OrganizationsService {
  private organizationsUrl = "api/Organizations";

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  getOrganizationDepartments(organizationId: number) {

    return this.http.get<Department[]>(`${this.organizationsUrl}/${organizationId}/departments`).pipe(
      tap(employees => this.log("fetched organization departments")),
      catchError(this.handleError("getOrganizationDepartments", []))
    );
    
  }

  getAllOrganization() {
    return this.http.get<Organization[]>(this.organizationsUrl).pipe(
      tap(employees => this.log("fetched Organizations")),
      catchError(this.handleError("getAllOrganization", []))
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
