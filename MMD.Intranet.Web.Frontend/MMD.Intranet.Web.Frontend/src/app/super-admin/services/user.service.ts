import { Injectable, Inject } from '@angular/core';
import { Response, URLSearchParams } from '@angular/http';

import { ApiService } from '../../core/services/api.service';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class UserService {

  constructor( @Inject('apiUrls') public apiUrls, public apiService: ApiService) {

  }

  getUserList() {
    return this.apiService.get(this.apiUrls.superAdmin.usersList, {}).map(res => {
      return res.json();
    }).catch(this.handleError);
  }


  getUser(userId) {
    var params = new URLSearchParams();
    params.set('id', userId);
    return this.apiService.get(this.apiUrls.superAdmin.userById, { search: params }).map(res => {
      return res.json();
    }).catch(this.handleError);
  }


  getClaims() {
    return this.apiService.get(this.apiUrls.superAdmin.claims, {}).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  saveUser(user) {
    return this.apiService.post(this.apiUrls.superAdmin.register, user).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  updateUser(rolesClaims) {
    return this.apiService.post(this.apiUrls.superAdmin.updateUser, rolesClaims).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  handleError(error: any) {
    console.error('server error:', error);
    if (error instanceof Response) {
      let errMessage = '';
      try {
        errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      return Observable.throw(errMessage);
    }
    return Observable.throw(error || 'server error');
  }



}
