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
export class OrderService {
    cachedData: any = {};
    constructor(@Inject('apiUrls') private apiUrls,
        public apiService: ApiService,
        private tostService: ToastService) {

    }
    salesOrder(params, force = false) {
        let key = 'salesOrder';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.order.salesOrderparams).map(res => {
                return res.json();
            }).catch(this.handleError);

        }
    }

    purchaseOrder(params, force = false) {
        let key = 'purchaseOrder';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.order.purchaseOrderparams).map(res => {
                return res.json();
            }).catch(this.handleError);
        }
    }
    workOrder(params, force = false) {
        let key = 'workOrder';
        let partData = this.cachedData ? this.cachedData[key] : null;

        if (partData && !force) {
            return this._returnObserables(partData);
        }
        else {
            return this.apiService.get(this.apiUrls.order.workOrderparams).map(res => {
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
