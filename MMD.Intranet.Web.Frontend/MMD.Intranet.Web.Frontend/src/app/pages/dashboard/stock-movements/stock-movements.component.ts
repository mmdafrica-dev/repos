
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { StockService } from '../services/stock.services';
import { Events } from '../../../core/events';
@Component({
    selector: 'stock-movement',
    templateUrl: './stock-movements.component.html',
    styleUrls: ['./stock-movements.component.scss']
})
export class StockMovementsComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private partDetailService: PartDetailService,
        private stockServise: StockService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.stockMovements(params, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.stockMovements(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    stockMovements(parameter, force: boolean) {
        parameter.piToAction = 'ZZZ';
        parameter.piToPart = 'ZZZ';
        parameter.piToContCode = 'ZZZ';
        parameter.piToLotRef = 'ZZZ';
        parameter.piNoRecords = '0';
        parameter.piToAdditRef = 'ZZZ';
        this.event.publish('loaderShow', true);
        this.stockServise.stockMovements(parameter, force)
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
