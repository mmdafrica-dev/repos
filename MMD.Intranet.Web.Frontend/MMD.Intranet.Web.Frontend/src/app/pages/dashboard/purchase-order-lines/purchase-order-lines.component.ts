import { Component, OnInit, OnDestroy } from '@angular/core';
// import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { OrderService } from '../services/order.service';
import { Events } from '../../../core/events';

@Component({
    selector: 'purchase-order-lines',
    templateUrl: './purchase-order-lines.component.html',
    styleUrls: ['purchase-order-lines.component.scss']
})
export class PurchaseOrderLinesComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private orderService: OrderService,
        private partDetailService: PartDetailService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.partNarrDeatail(params, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.partNarrDeatail(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    partNarrDeatail(parameter, force: boolean) {
        parameter.piToOrder = 'ZZZ';
        parameter.piAll = 'N';
        parameter.piToJob = 'ZZZ';
        parameter.piToSupp = 'ZZZ';
        parameter.piSortBy = 'ORDS1_REF';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.orderService.purchaseOrder(parameter, force)
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
