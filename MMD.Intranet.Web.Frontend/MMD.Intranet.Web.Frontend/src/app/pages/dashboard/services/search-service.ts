import { Injectable, Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { ApiProvider } from '../../../core/services/api.provider';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ToastService, TostType } from '../../../core/services/toast.service';

@Injectable()
export class SearchService {

    constructor(@Inject('apiUrls') private apiUrls,
        public apiService: ApiProvider,
        private tostService: ToastService,
        private router: Router
    ) {

    }
    getSearchOptions(menuKey, subMenuKey, val) {
        var params: any = {};// new HttpParams();
        params.menuKey = menuKey;
        params.subMenuKey = subMenuKey;
        params.selection = val;
        return this.apiService.get(this.apiUrls.menu.getSearchOptions, params).pipe().map(res => {
            return res;
        }).catch(this.handleError.bind(this));
    }

    getSearch(api, params) {
        return this.apiService.get('api/' + api, params).pipe().map(res => {
            return res;
        }).catch(this.handleError.bind(this));
    }
    handleError(error: any) {
        if (error.status != 401) {
            //this.router.navigate(['/auth/login']);
            return Observable.empty();
        }
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

}
