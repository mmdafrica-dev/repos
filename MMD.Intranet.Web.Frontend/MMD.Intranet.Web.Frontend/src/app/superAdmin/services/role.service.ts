import { Injectable, Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { ApiProvider } from '../../core/services/api.provider';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { ToastService, TostType } from '../../core/services/toast.service';

@Injectable()
export class RoleService {

  constructor(@Inject('apiUrls') public apiUrls,
    public apiService: ApiProvider,
    private tostService: ToastService) {

  }

  getRoles() {
    return this.apiService.get(this.apiUrls.superAdmin.roleList, {}, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  getRole(roleId) {
    var params: any = {};
    params.id = roleId;
    return this.apiService.get(this.apiUrls.superAdmin.role, params, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  deleteRole(roleId) {
    var params: any = {};
    params.id = roleId;
    return this.apiService.get(this.apiUrls.superAdmin.deleteRole, params, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  saveRole(role) {
    return this.apiService.post(this.apiUrls.superAdmin.role, role, null, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  updateRole(roleId, role) {
    var params: any = {};//new HttpParams();
    params.id = roleId;
    return this.apiService.post(this.apiUrls.superAdmin.updateRole, role, params, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this).bind(this));
  }

  handleError(error: any) {
    // console.error('server error:', error);
    if (error instanceof Response) {
      let errMessage = '';
      try {
        // errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      // this._errorAlert(errMessage);
      return Observable.throw(errMessage);
    }
    // this._errorAlert(error);
    return Observable.throw(error || 'server error');
  }
  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }

}
