import { Injectable, Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { ApiService } from '../../../core/services/api.service';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ToastService, TostType } from '../../../core/services/toast.service';

@Injectable()
export class MainMenuService {
  cachedData: any = {};
  constructor(@Inject('apiUrls') private apiUrls,
    public apiService: ApiService,
    private tostService: ToastService) {

  }
  getmodule() {
    let key = 'module';
    let moduleData = this.cachedData ? this.cachedData[key] : null;
    if (moduleData) {
      return this._returnObserables(moduleData);
    }
    return this.apiService.get(this.apiUrls.menu.module).map(res => {
      let moduleData = res.json();
      this.cachedData[key] = moduleData;
      return moduleData;
    }).catch(this.handleError.bind(this));

  }

  getMainMenu(key) {
    var params: any = {};// new HttpParams();
    params.menuKey = key;
    return this.apiService.get(this.apiUrls.menu.mainMenuparams).map(res => {
      return res.json();
    }).catch(this.handleError.bind(this));

  }


  handleError(error: any) {
    if (error instanceof Response) {
      let errMessage = '';
      try {
        // errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      this._errorAlert(errMessage);
      return Observable.throw(errMessage);
    }
    this._errorAlert(error);
    return Observable.throw(error || 'server error');
  }
  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }

  _returnObserables(data: any) {
    return Observable.create((observer) => {
      observer.next(data);
      observer.complete();
    });
  }
}
