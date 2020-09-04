import { Component, OnInit, OnDestroy } from '@angular/core';
// import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
// import { PartDetailService } from '../services/part-detail.service';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';

import { OrderService } from '../services/order.service';
import { Events } from '../../../core/events';
import { StorageService } from '../../../core/services/storage.service';


@Component({
    selector: 'mmd-where-used',
    templateUrl: './where-used.component.html',
    styleUrls: ['./where-used.component.scss']
})
export class WhereUsedComponent implements OnInit, OnDestroy {
    data: any;
    formSubmitSubscribe: any;
    // sortBy: any;
    // sortOrder: any;
    result: any;
    // data: any;
    hearder: any;
    currentFilter = [];
    operationsData: any;
    operationsHeader: any;
    filterQuery = "";
    // filterBycolumn = "";
    sortBy = "name";
    sortOrder = "name";
    filterBycolumn = "name";
    rowsOnPage = 50;
    constructor(
        private orderService: OrderService,
        private partDetailService: PartDetailService,
        private partAndBomService: PartAndBomService,
        private event: Events,
        public storage: StorageService,
        private router: Router
    ) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.salesOrder(params, true, true);
        });
    }
    ngOnInit() {

    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    partDetailClick() {
        this.unSubscribeAll();
        this.storage.setValue('currentTab', 'whereUsed');
    }

    salesOrder(parameter, force: boolean, isback) {
        parameter.SCHTYPE = 'COMPPARENTS';
        var searchfor = parameter.searchfor;
        isback = false;
        force = false;
        if (this.storage.getValue('currentTab')) {
            isback = false;
            force = false;
        }
        else {
            isback = true;
            force = true;
        }
        this.event.publish('loaderShow', true);
        this.partAndBomService.WhereUsedDetail(parameter, force, 'COMPPARENTS', searchfor, isback)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                if (isback) {
                    // this.
                }

                this.data = result.result;
                this.hearder = result.headers;
                if (isback && result.result.length > 0)
                    this.router.navigate(['/extranet/partsearchdetail/' + searchfor + '/whereUsed']);

            }, error => {

            });
    }
    unSubscribeAll() {
        if (this.formSubmitSubscribe) {
            this.formSubmitSubscribe.unsubscribe();
            this.formSubmitSubscribe = null;
        }
    }

    headerChange(f) {
        this.filterQuery = f;
        this.checkFilter();
    }

    checkFilter() {
        this.operationsHeader.forEach(record => {
            let isValueExist = true;
            this.currentFilter.forEach(filter => {
                if (filter.key == record.name) {
                    if (record.text) {
                        filter.value = record.text;
                        isValueExist = false;
                    }
                    else {
                        filter.value = '';
                        isValueExist = false;
                    }
                }
            });
            if (isValueExist) {
                if (record.text) {
                    this.currentFilter.push(
                        {
                            key: record.name,
                            value: record.text
                        });
                }
            }
        });

        for (var i = 0; i < this.currentFilter.length; i++) {
            if (this.currentFilter[i].value === "") {
                this.currentFilter.splice(i, 1);
            }
        }
    }
}
