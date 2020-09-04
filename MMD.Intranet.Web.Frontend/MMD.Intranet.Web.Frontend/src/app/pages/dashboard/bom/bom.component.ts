
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { Events } from '../../../core/events';
import { StorageService } from '../../../core/services/storage.service';
import { ExcelService } from '../../../core/services/excel.service';

import * as _ from "lodash";
@Component({
    selector: 'mmd-bom',
    templateUrl: './bom.component.html',
    styleUrls: ['./bom.component.scss']
})
export class BomComponent implements OnInit, OnDestroy {
    bomDetail: any;
    operationsData: any;
    operationsHeader: any;
    formSubmitSubscribe: any;
    // sortBy: any;
    // sortOrder: any;
    result: any;
    bomData: any;
    bomHeader: any;
    filterQuery = "";
    rowsOnPage = 50;
    sortBy = "name";
    sortOrder = "name";
    filterBycolumn = "name";

    apiLink: string;
    dataTable = false;
    currentFilter = [];
    total = '';
    searchForResult = '';
    // isRedirect: boolean = false;
    partData: any;

    constructor(
        private partDetailService: PartDetailService,
        private partAndBomService: PartAndBomService,
        private event: Events,
        private router: Router,
        public storage: StorageService,
        private excelService: ExcelService,
    ) {
        // this.isRedirect = false;
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            // if (!this.isRedirect)
            this.bom(params, true, true);
        });
    }
    ngOnInit() {
        let params = this.partDetailService.params;
        if (params) {
            this.bom(params, false, false);
        }
    }
    ngOnDestroy() {
        this.unSubscribeAll();
    }

    bom(parameter, force: boolean, isback) {
        if (!parameter) return;
        this.getpartDetail(parameter, true, false);
        this.event.publish('loaderShow', true);
        parameter.SCHTYPE = 'COMPCHILDREN';
        var searchfor = parameter.searchfor;
        this.searchForResult = searchfor;
        if (this.storage.getValue('currentTab')) {
            isback = false;
            force = false;
        }
        this.bomData = [];

        this.partAndBomService.partBomDetail(parameter, force, 'COMPCHILDREN', searchfor, isback)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                if (result) {
                    this.bomData = [];
                    var temp = this;
                    this.total = '';
                    _.each(result.result, function (item, index) {
                        item.number = index + 1;
                        temp.bomData.push(item);
                        temp.total = item.wsTotalMass;

                    });// result.result;
                    this.bomHeader = result.headers;

                    if (isback && result.result.length > 0)
                        this.router.navigate(['/extranet/partsearchdetail/' + searchfor + '/bom']);

                }
            }, error => {

            });
        var searchfor = parameter.searchfor;
        this.partAndBomService.partBomOpertaionDetail(parameter, force, searchfor, isback)
            .subscribe(result => {
                if (result) {
                    this.operationsData = result.result;
                    this.operationsHeader = result.headers;
                }
            }, error => {

            });
    }

    partDetailClick() {
        this.formUnSubscribe();
        this.storage.setValue('currentTab', 'bom');
    }

    partClick(value) {
        this.formUnSubscribe();
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
    unSubscribeAll() {
        // if (this.formSubmitSubscribe) {
        //     this.formSubmitSubscribe.unsubscribe();
        //     this.formSubmitSubscribe = null;
        // }
        this.formUnSubscribe();
    }
    formUnSubscribe() {
        if (this.formSubmitSubscribe) {
            this.formSubmitSubscribe.unsubscribe();
            this.formSubmitSubscribe = null;
        }
    }

    downloadPdf() {
        this.excelService.downloadPdf(this.bomData, this.bomHeader, this.total, this.searchForResult, this.partData.partdesc);
    }

    getpartDetail(parameter, force, isback) {
        var parameters = this.partDetailService.params;
        if (parameters) {
            parameter = parameters;
        }
        parameter.searchBy = "1";
        localStorage.removeItem('IsFirstLoadPartDetail');
        var searchfors = parameter.searchfor;

        this.partAndBomService.search(parameter, force, searchfors, isback)
            .subscribe(result => {
                if (result) {
                    this.partData = result.result[0];
                }
            }, error => {
            });
    }

}
