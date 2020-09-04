import { Injectable, Output, EventEmitter } from '@angular/core';

import { HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';

import { map, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';

import { MMDQueryEncoder } from '../../shared/mmd-query-encoder';
import { ApiProvider } from './api.provider';
import { StorageService } from './storage.service';
import { ToastService, TostType } from './toast.service';

@Injectable()
export class AuthService {

  authUrl: string = '/Token';
  isAuthenticated: boolean = false;
  redirectUrl: string;
  public loggedIn: any;

  @Output() authChanged: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    public apiService: ApiProvider,
    public storage: StorageService,
    private tostService: ToastService
  ) { }

  private userAuthChanged(status: boolean) {
    this.authChanged.emit(status); //Raise changed event
  }

  login(userLogin): Observable<boolean> {

    let params = new HttpParams();
    params = params.set('grant_type', 'password');
    params = params.set('username', userLogin.email);
    params = params.set('password', userLogin.password);
    let config = { contentType: 'application/x-www-form-urlencoded' };

    return this.apiService.post('token', params, config, false).pipe(
      map((response) => {
        this.loggedIn = response;
        this.storage.setObject('userItem', this.loggedIn);
        this.storage.setValue('userName', userLogin.email);
        this.isAuthenticated = this.loggedIn;
        this.userAuthChanged(this.loggedIn);
        return this.loggedIn;
      }), catchError(this.handleError));
  }

  logout(): Observable<boolean> {
    return this.apiService.post('api/Account/Logout', {}, {}, false).pipe(
      map((response) => {
        const loggedOut = true;
        this.storage.removeItem('userItem');
        this.storage.removeGenericData();
        this.isAuthenticated = false;
        this.userAuthChanged(false); //Return loggedIn status
        return status;
      }), catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      // return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'Node.js server error');
  }

  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }


}