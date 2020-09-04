import { Component, OnInit, OnDestroy } from '@angular/core';
// import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { CostService } from '../services/cost.services';
import { Events } from '../../../core/events';

@Component({
    selector: 'cost-set-detail',
    templateUrl: 'cost-set-details.component.html',
    styleUrls: ['cost-set-details.component.scss']
})
export class CostSetDetailComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    constructor(
        private costService: CostService,
        private partDetailService: PartDetailService,
        private event: Events) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.costSet(params);
            this.costElements(params);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.costElements(params);
            this.costSet(params);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    costSet(parameter) {
        this.event.publish('loaderShow', true);
        this.costService.costSet(parameter)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                this.data = result.result;
            }, error => {

            });
    }
    costElements(parameter) {
        this.event.publish('loaderShow', true);
        this.costService.costElements(parameter)
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
