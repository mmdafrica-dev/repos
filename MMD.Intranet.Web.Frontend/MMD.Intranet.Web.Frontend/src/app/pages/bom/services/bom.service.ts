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
export class BomService {

    constructor(@Inject('apiUrls') private apiUrls,
        private apiService: ApiService,
        private tostService: ToastService) {

    }

    bomDetail(params) {
        return this.apiService.get(this.apiUrls.partsAndBom.bomDetail, params).map(res => {
            return res.json();
        }).catch(this.handleError.bind(this));
    }


    handleError(error: any) {
        console.error('server error:', error);
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
