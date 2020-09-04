import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { Events } from '../../../core/events';
import { StorageService } from '../../../core/services/storage.service';


@Component({
    selector: 'mmd-part-narrative',
    templateUrl: './part-narrative.component.html',
    styleUrls: ['part-narriative.component.scss']
})
export class PartNarrtiveComponent implements OnInit, OnDestroy {
    data: any = [];
    formSubmitSubscribe: any;
    sortBy: any;
    sortOrder: any;
    result: any;
    header: any = [];

    constructor(
        private partAndBomService: PartAndBomService,
        private partDetailService: PartDetailService,
        private activatedRoute: ActivatedRoute,
        private event: Events,
        private router: Router,
        public storage: StorageService,
        @Inject('apiUrls') private apiUrls) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.partNarrDeatail(params, true, true);
        });
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.unSubscribeAll();
    }

    partNarrDeatail(parameter, val, isback) {
        var parameters = this.partDetailService.params;
        if (parameters) {
            parameter = parameters;
        }

        if (this.storage.getValue('currentTab')) {
            isback = false;
        }

        this.event.publish('loaderShow', true);
        localStorage.removeItem('IsFirstLoadNarative');
        parameter.searchBy = 'PARTNARRDETAIL';
        var searchfors = parameter.searchfor;
        this.partAndBomService.partNarrDeatail(parameter, val, searchfors, isback)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                if (result) {
                    this.data = result.result;
                    this.header = result.headers;
                }
                if (isback && result.result.length > 0)
                    this.router.navigate(['/extranet/partsearchdetail/' + searchfors + '/partnarrtive']);

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
