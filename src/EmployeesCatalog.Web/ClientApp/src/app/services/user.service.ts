import { Injectable } from "@angular/core";

import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, BehaviorSubject, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";

import { ConfigService } from "../utils/config.service";

import { BaseService } from "./base.service";

import { UserRegistration } from "../Data/user.registration.interface";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json" })
};

@Injectable()
export class UserService extends BaseService {
  baseUrl: string = "";

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();
  

  private loggedIn = false;

  constructor(
    // private http: Http,
    private http: HttpClient,
    private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem("auth_token");
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

  login(userName, password) {
debugger;
    return this.http
      .post(
        "api/auth/login",
        JSON.stringify({ userName, password }),
        httpOptions
      ).pipe( 
        tap(res => {
          debugger;
          localStorage.setItem("auth_token", res["auth_token"]);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
          return true;
        }),
        catchError(this.handleError));
  }

  logout() {
    localStorage.removeItem("auth_token");
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}
