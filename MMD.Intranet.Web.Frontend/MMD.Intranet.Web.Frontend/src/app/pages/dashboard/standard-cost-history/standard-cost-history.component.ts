
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { CostService } from '../services/cost.services';
import { Events } from '../../../core/events';

@Component({
    selector: 'standard-cost-history',
    templateUrl: './standard-cost-history.component.html',
    styleUrls: ['./standard-cost-history.component.scss']
})
export class StandardCostHistoryComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    constructor(
        private partDetailService: PartDetailService,
        private costService: CostService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.standardCostHistory(params, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.standardCostHistory(params, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    standardCostHistory(parameter, force: boolean) {
        parameter.CostHead = 'Y';
        parameter.piSortBy = 'CREC1_REF';
        parameter.piSortType = 'ASC';
        this.event.publish('loaderShow', true);
        this.costService.standardCostHistory(parameter, force)
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
