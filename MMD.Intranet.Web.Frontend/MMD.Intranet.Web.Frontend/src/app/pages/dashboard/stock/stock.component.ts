
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { StockService } from '../services/stock.services';
import { Events } from '../../../core/events';
import { SettingService } from '../../../core/services/setting.service';

@Component({
    selector: 'mmd-stock',
    templateUrl: './stock.component.html',
    styleUrls: ['./stock.component.scss']
})
export class StockComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private partDetailService: PartDetailService,
        private stockService: StockService,
        private setting: SettingService,
        private event: Events) {
            this.setting.emitChildRender();
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.binDetail(params, true);
            this.lotDetail(params, true);
           // this.setting.emitChildRender();
        });
    }
    ngOnInit() {
     
        let params = this.partDetailService.params;
        if (params) {
            this.binDetail(params, false);
            this.lotDetail(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    binDetail(parameter, force) {
        parameter.piToStore = 'ZZZ';
        parameter.piFrBin = 'ZZZ';
        parameter.piToContCode = 'ZZZ';
        parameter.piToCommod = 'ZZZ';
        parameter.piToPg = 'ZZZ';
        parameter.piStores = 'Y';
        parameter.piSortBy = 'BINS1_STORE';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.stockService.binDetail(parameter, force)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                this.data = result.result;
            }, error => {

            });
    }
    lotDetail(parameter, force: boolean) {
        parameter.piToOrder = 'ZZZ';
        parameter.piStores = 'Y';
        parameter.piToLotRef = 'ZZZ';
        parameter.piFrLotLine = '0';
        parameter.piToLotLine = '999';
        parameter.piToStore = 'ZZZ';
        parameter.piToBin = 'ZZZ';
        parameter.piToContCode = '999';
        parameter.piToCommod = 'ZZZ';
        parameter.piToPg = 'ZZZ';
        parameter.piSortBy = 'BLOT1_STORE';
        parameter.piSortType = 'ASC';
        this.stockService.lotDetail(parameter, force)
            .subscribe(result => {
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
