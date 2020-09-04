import { Component, OnInit, OnDestroy } from '@angular/core';
import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { Events } from '../../../core/events';

@Component({
    selector: 'part-allocation',
    templateUrl: './part-allocation.component.html',
    styleUrls: ['./part-allocation.component.scss']
})
export class PartAllocationComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private partAndBomService: PartAndBomService,
        private partDetailService: PartDetailService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.partAllocation(params);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.partAllocation(params);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    partAllocation(parameter) {
        parameter.piFrRecType = '30';
        parameter.piToRecType = '30';
        parameter.piAll = 'Y';
        parameter.piFrOrderType = '0';
        parameter.piToOrderType = '0';
        parameter.piToRef = 'ZZZ';
        parameter.piFrLine = '0';
        parameter.piToLine = '9999999';
        parameter.piToContCode = 'ZZZ';
        parameter.piSortBy = 'ORDS1_REF';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.partAndBomService.partAllocation(parameter)
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
