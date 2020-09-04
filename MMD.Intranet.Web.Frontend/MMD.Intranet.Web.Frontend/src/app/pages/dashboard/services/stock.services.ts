
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
export class StockService {
    cachedData: any = {};
    constructor(@Inject('apiUrls') private apiUrls,
        public apiService: ApiService,
        private tostService: ToastService) {

    }
    stockMovements(params, force = false) {
        let key = 'stockMovements';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.stock.stockMovementsparams).map(res => {
                return res.json();
            }).catch(this.handleError.bind(this));
        }
    }
    binDetail(params, force = false) {
        let key = 'binDetail';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.stock.binDetailparams).map(res => {
                return res.json();
            }).catch(this.handleError.bind(this));
        }
    }

    lotDetail(params, force = false) {
        let key = 'lotDetail';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.stock.lotDetailparams).map(res => {
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
