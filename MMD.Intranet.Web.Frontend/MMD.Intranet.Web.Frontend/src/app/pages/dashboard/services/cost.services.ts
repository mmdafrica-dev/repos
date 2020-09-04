
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
export class CostService {
    cachedData: any = {};
    constructor(@Inject('apiUrls') private apiUrls,
        public apiService: ApiService,
        private tostService: ToastService) {

    }
    costElements(params) {
        return this.apiService.get(this.apiUrls.cost.costElements, params).map(res => {
            return res.json();
        }).catch(this.handleError.bind(this));

    }
    costSet(params, force = false) {
        let key = 'costSet';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.cost.costSetparams).map(res => {
                return res.json();
            }).catch(this.handleError.bind(this));
        }

    }
    standardCostHistory(params, force = false) {
        let key = 'standardCostHistory';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.cost.standardCostHistoryparams).map(res => {
                return res.json();
            }).catch(this.handleError.bind(this));

        }
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
    _returnObserables(data: any) {
        return Observable.create((observer) => {
            observer.next(data);
            observer.complete();
        });
    }

}
