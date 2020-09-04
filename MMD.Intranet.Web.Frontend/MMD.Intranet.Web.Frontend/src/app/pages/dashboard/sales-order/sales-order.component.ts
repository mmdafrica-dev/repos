import { Component, OnInit, Inject, ViewChild } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PlatformLocation } from '@angular/common'
import { SearchService } from '../services/search-service';
import { HttpParams } from '@angular/common/http';
import { DataFilterPipe } from '../../../shared/pipe/searchPipe';
import { Events } from '../../../core/events';
import { ExcelService } from '../../../core/services/excel.service';
import { SettingService } from '../../../core/services/setting.service';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { ToastService, TostType } from '../../../core/services/toast.service';
import { StorageService } from '../../../core/services/storage.service';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
// import { DataTable, PageEvent } from "./DataTable";

import { Paginator, DataTable } from 'angular2-datatable';

import * as _ from "lodash";
import * as moment from 'moment';
import { DataPanelFilterPipe } from 'app/shared/pipe/searchPanelPipe';

declare var localStorage;
@Component({
    selector: 'app-sales-order',
    templateUrl: './sales-order.component.html',
    styleUrls: ['./sales-order.component.scss'],
    providers: [DataFilterPipe,DataPanelFilterPipe]
})
export class SalesOrderComponent implements OnInit {
    // searchForm: FormGroup;
    searchOptions: any;
    mainMenu: string;
    subMenu: string;
    isFormValid = false;

    header = [];
    filterQuery = "";
    rowsOnPage = 50;
    sortBy = "parT1_PART";
    sortOrder = "asc";
    filterBycolumn = "parT1_PART";
    data = [];
    apiLink: string;
    dataTable = false;
    currentFilter = [];
    excelData: any;
    autoCompleteValue: any;

    currentIndex: any;
    currentIndex2: any;
    isPartNarritive: boolean;
    childheader: any;
    childdata: any;
    childResult: any;
    isback = false;
    isFxRate: boolean = false;
    deatilData: any;
    fisrtable: any;
    secondtable: any;
    @ViewChild('mf') private mf: DataTable;
    previousPage = 0;
    constructor(
        @Inject('apiUrls') private apiUrls,
        @Inject('rowCount') private rowCount, private searchService: SearchService,
        private fb: FormBuilder,
        public storage: StorageService,
        private activatedRoute: ActivatedRoute,
        private event: Events,
        private excelService: ExcelService,
        private setting: SettingService,
        private partDetailService: PartDetailService,
        private partAndBomService: PartAndBomService,
        private router: Router,
        private toast: ToastService,
        private location: PlatformLocation
    ) {
        this.setting.emitChildRender();
        this.rowsOnPage = this.rowCount.rowCount;
        this.apiLink = 'PartsAndBom/SearchPart';
    }

    ngOnInit() {
        let userData = this.storage.getObject('userItem');
        let roles = JSON.parse(userData.roles);
        // console.log(roles);
        if (roles.indexOf('sales') == -1) {
            this.router.navigate(['/extranet/dashboard']);
        }
        var data = this.partAndBomService.getPartData();
        if (data)
            this.fillTable(data, false);
        else
            this.salesOrderFormSubmit();
    }

    ngAfterViewInit() {
        this.mf.onPageChange.subscribe((x) => {
            if (x.activePage != this.previousPage) {
                this.currentIndex = null;
                this.previousPage = x.activePage;
            }
        });
    }

    salesOrderFormSubmit() {
        this.event.publish('loaderShow', true);
        this.partAndBomService.getPartDetailData(null, true, null, this.apiUrls.salesOrder.getSales, false).subscribe(res => {
            this.event.publish('loaderShow', false);
            this.partAndBomService.setPartData(res);
            this.fillTable(res, false);
        });
    }

    fillTable(res, val) {
        this.dataTable = true;
        if (res) {
            this.header = res.headers;
            this.fisrtable = this.header.length - 1;
            this.data = res.result;

        }
    }

    onClickRow(item, index) {
        if (this.currentIndex == index) {
            this.currentIndex = null;
        }
        else if (!this.currentIndex) {
            this.currentIndex = index;
            this.getSalesOrder(item);
        }
        else {
            this.currentIndex = index;
            this.getSalesOrder(item);
        };
        this.currentIndex2 = null;
    }

    getSalesOrder(item) {
        var params: any = {};
        this.childheader = [];
        this.childdata = [];
        params.Searchfor = item.gphC_SALESORDER;
        params.SearchBy = 'PARTNARRDETAIL';
        this.event.publish('loaderShow', true);
        this.partAndBomService.getPartDetailData(params, true, '', this.apiUrls.salesOrder.getSalesDetail, false)
            .subscribe(res => {
                this.childResult = res;
                this.event.publish('loaderShow', false);
                this.childheader = this.childResult.headers;
                this.secondtable = this.childheader.length - 1;
                this.childdata = this.childResult.result;
            });
    }

    onClickInternalRow(item, index) {
        if (this.currentIndex2 == index) {
            this.currentIndex2 = null;
        }
        else if (!this.currentIndex2) {
            this.currentIndex2 = index;
        }
        else {
            this.currentIndex2 = index;
        };
        this.deatilData = item;
    }

    headerChange(f) {
        this.currentIndex = null;
        this.filterQuery = f;
        this.checkFilter();
    }

    clear(val) {
        this.currentIndex = null;
        val.text = "";
        this.filterQuery = "";
        this.checkFilter();
    }

    checkFilter() {
        this.header.forEach(record => {
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

    sortClicked() {
        this.currentIndex = null;
    }

    onPageChange() {
    }
}


