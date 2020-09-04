
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { SupplierService } from '../services/supplier.services';
import { Events } from '../../../core/events';


@Component({
    selector: 'supplier-price',
    templateUrl: './supplier-price.component.html',
    styleUrls: ['./supplier-price.component.scss']
})
export class SupplierPriceComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private partDetailService: PartDetailService,
        private supplierService: SupplierService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.supplierPrice(params, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.supplierPrice(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    supplierPrice(parameter, force: boolean) {
        parameter.piSortBy = 'ARRY1_DATE_IN';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.supplierService.supplierPrices(parameter, force)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                this.data = result.result;
            }, error => {

            });
    }
    unSubscribeAll() {
        if (this.formSubmitSubscribe) {
            this.formSubmitSubscribe.unsubscribe();
            this.formSubmitSubscribe = null;
        }
    }
}
