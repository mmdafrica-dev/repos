import { Component, OnInit, Inject } from '@angular/core';

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

import * as _ from "lodash";
import * as moment from 'moment';
import { DataPanelFilterPipe } from 'app/shared/pipe/searchPanelPipe';

declare var localStorage;
@Component({
    selector: 'app-fx-rates',
    templateUrl: './fx-rates.component.html',
    styleUrls: ['./fx-rates.component.scss'],
    providers: [DataFilterPipe, DataPanelFilterPipe]
})
export class FXRatesComponent implements OnInit {
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
    fromDate: any;
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
        this.rowsOnPage = this.rowCount.rowCount;

    }

    ngOnInit() {
        let userData = this.storage.getObject('userItem');
        let roles = JSON.parse(userData.roles);

        if (roles.indexOf('rates') == -1) {
            this.router.navigate(['/extranet/dashboard']);
        }

        this.setting.emitChildRender();
        this.fromDate = moment().format("DD-MMM-YYYY");
        var data = this.partAndBomService.getPartData();
        if (data)
            this.fillTable(data);
        else
            this.getFxRates();
    }

    getFxRates() {
        this.event.publish('loaderShow', true);
        var params: any = {};
        params.DateFrom = moment().format("DD-MMM-YYYY");
        this.partAndBomService.getPartDetailData(params, true, params.DateFrom, this.apiUrls.stock.getFxRates, false).subscribe(res => {
            this.event.publish('loaderShow', false);
            this.fillTable(res);
        });
    }

    fillTable(res) {
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
        }
        else {
            this.currentIndex = index;
        };
        this.currentIndex2 = null;
    }

    headerChange(f) {
        this.currentIndex = null;
        this.filterQuery = f;
        this.checkFilter();
    }

    clear(val) {
        if (this.currentFilter.length == 1) {
            this.filterQuery = val.text;
            val.text = "";
            this.filterQuery = "";
            this.checkFilter();
        }
        else {
            val.text = "";
            this.checkFilter();
        }
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

}


