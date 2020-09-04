import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { PartDetailService } from '../services/part-detail.service';
import { Events } from '../../../core/events';
import * as _ from "lodash";
import { StorageService } from '../../../core/services/storage.service';

@Component({
    selector: 'part-detail',
    templateUrl: 'part-detail.component.html',
    styleUrls: ['part-detail.component.scss']
})
export class PartDetailComponent implements OnInit {
    searchOptions: any;
    searchOptionsData: any;
    isSearchOptions: any;
    partNumber: any;
    autoCompleteValue: any;
    selcetedValue: any;
    tabList: any;
    tabListData: any;
    paramsData: any;
    result: any;
    tabResult: any;
    tabName: string = 'partDetail';
    constructor(
        @Inject('apiUrls') private apiUrls,
        @Inject('tenant') public tenant,
        private partAndBomService: PartAndBomService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private partDetailService: PartDetailService,
        private event: Events,
        public storage: StorageService
    ) {
        this.router.routeReuseStrategy.shouldReuseRoute = function () {
            return false;
        }
        this._init();
    }

    ngOnInit() {
        // var currentTab = this.storage.getValue('currentTab');
        // if (currentTab) {
        //     this.tabName = currentTab;
        //     var temp = this;
        //     setTimeout(function () {
        //         temp.storage.removeItem('currentTab');
        //     }, 2000);
        // }
        // else {
        //     this.tabName = 'partDetail';
        // }
    }

    handleFormSubmit(params) {
        this.paramsData = params;

        this._handleFormSubmit(params);
    }

    _init() {
        this.setvalueForTab("value");
        if (this.storage.getObject('tabListData')) {
            this.tabList = this.storage.getObject('tabListData');
        }
        else {
            this._getPartTabs();
        }
        this.partAndBomService.partSearchDetailOptions('partsearchdetail')
            .subscribe(res => {
                this.result = res;
                if (this.result.result.options) {
                    this.searchOptionsData = this.result.result.options;
                    this.searchOptions = this.result.result.options;

                }
            }, error => {

            });

        this.partNumber = this.activatedRoute.snapshot.params.partNumber;
        this.autoCompleteValue = this.partNumber;
        this.selcetedValue = 1;
    }

    getData(value) {
        this.setvalueForTab(value);
    }

    _handleFormSubmit(params) {
        this.partDetailService.formSubmit(params);
        if (this.tenant.key != 'extranetmenu') {
            this.partAndBomService.getPartDetailData(params, true, '', this.apiUrls.partDetail.getPartDropDown, false).subscribe(result => {
                if (result.result) {
                    _.map(this.searchOptions, (item) => {
                        if (item.type == "Selection") {
                            var data = [];
                            for (let i = 0; i < result.result.length; i++) {
                                data.push({
                                    'key': result.result[i].patH1_REF,
                                    'value': result.result[i].patH1_REF
                                })
                                item.data = data;
                            }
                        }
                    });
                }
            }, error => {

            });
        }
    }

    _getPartTabs() {
        this.event.publish('loaderShow', true);
        this.partAndBomService.partDetailTabs()
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                this.tabListData = result;
                this.tabList = this.tabListData.result;
                this.storage.setObject('tabListData', this.tabListData.result);
                this.isSearchOptions = true;
                this.event.publish('tabLoaded', true);
            }, error => {

            });
    }

    setvalueForTab(value) {
        var temp = this;
        temp.storage.setValue('currentTab', value);
        // setTimeout(function () {
        //     temp.storage.removeItem('currentTab');
        // }, 1500);
    }

}
