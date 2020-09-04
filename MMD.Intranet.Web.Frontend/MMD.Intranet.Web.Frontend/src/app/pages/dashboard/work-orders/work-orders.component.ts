
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { OrderService } from '../services/order.service';
import { Events } from '../../../core/events';

@Component({
    selector: 'work-orders',
    templateUrl: './work-orders.component.html',
    styleUrls: ['./work-orders.component.scss']
})
export class WorkOrdersComponent implements OnInit, OnDestroy {
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
            this.workOrders(params, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.workOrders(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    workOrders(parameter, force: boolean) {
        parameter.piToOrder = 'ZZZ';
        parameter.piAll = 'N';
        parameter.piToContCode = 'ZZZ';
        parameter.piToPlanner = 'ZZZ';
        parameter.piFrStatus = '0';
        parameter.piToStatus = '999';
        parameter.piAll = 'Y';
        parameter.piSortBy = 'WORD1_REF';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.orderService.workOrder(parameter, force)
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
