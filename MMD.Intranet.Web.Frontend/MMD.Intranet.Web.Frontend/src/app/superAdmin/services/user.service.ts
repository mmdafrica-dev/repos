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
import { Router, ActivatedRoute, ParamMap } from '@angular/router';


@Injectable()
export class UserService {

  constructor(@Inject('apiUrls') public apiUrls,
    public apiService: ApiProvider,
    private tostService: ToastService,
    private router: Router) {

  }

  getUserList() {
    return this.apiService.get(this.apiUrls.superAdmin.usersList, {}, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }


  getUser(userId) {
    var params: any = {};//new HttpParams();
    params.id = userId;
    return this.apiService.get(this.apiUrls.superAdmin.userById, params, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }


  getClaims() {
    return this.apiService.get(this.apiUrls.superAdmin.claims, {}, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  saveUser(user) {
    return this.apiService.post(this.apiUrls.superAdmin.register, user, null, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  updateUser(rolesClaims) {
    return this.apiService.post(this.apiUrls.superAdmin.updateUser, rolesClaims, null, false).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }

  handleError(error: any) {
    console.error('server error:', error);
    if (error instanceof Response) {
      let errMessage = '';
      if (error) {
        console.log(error);

        if (error.status != 401)
          return Observable.throw(error);
        else {
          // this.router.navigate(['/auth/login']);
          return Observable.empty();
        }
      }
      try {
        // errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      //this._errorAlert(errMessage);
      return Observable.throw(errMessage);
    }
    // this._errorAlert(error);
    return Observable.throw(error || 'server error');
  }
  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }

}
