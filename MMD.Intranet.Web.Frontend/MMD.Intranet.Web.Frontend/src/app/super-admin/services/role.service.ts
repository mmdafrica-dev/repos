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
export class RoleService {

  constructor( @Inject('apiUrls') public apiUrls, public apiService: ApiService) {

  }

  getRoles() {
    return this.apiService.get(this.apiUrls.superAdmin.roleList, {}).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  getRole(roleId) {
    var params = new URLSearchParams();
    params.set('id', roleId);
    return this.apiService.get(this.apiUrls.superAdmin.role, { search: params }).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  saveRole(role) {
    return this.apiService.post(this.apiUrls.superAdmin.role, role).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  updateRole(roleId, role) {
    var params = new URLSearchParams();
    params.set('id', roleId);
    return this.apiService.put(this.apiUrls.superAdmin.role, role, { search: params }).map(res => {
      return res.json();
    }).catch(this.handleError);
  }

  handleError(error: any) {
    console.error('server error:', error);
    if (error instanceof Response) {
      let errMessage = '';
      try {
        errMessage = error.json();
      } catch (err) {
        errMessage = error.statusText;
      }
      return Observable.throw(errMessage);
    }
    return Observable.throw(error || 'server error');
  }

}
