import { Component, OnInit, Inject, } from '@angular/core';
import { Router } from '@angular/router';
import { MainMenuService } from '../services/main-menu.service';
import { Events } from '../../../core/events';
import { NgForm } from '@angular/forms';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { SettingService } from '../../../core/services/setting.service';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { StorageService } from '../../../core/services/storage.service';
import { GenericService } from '../services/generic.service';
import { ToastService } from '../../../core/services/toast.service';
import { UserService } from '../../../superAdmin/services/user.service';


import * as moment from 'moment';
import * as _ from "lodash";


@Component({
    moduleId: module.id,
    selector: 'mmd-module',
    templateUrl: 'module.component.html',
    styleUrls: ['module.component.scss'],
    providers: []
})
export class ModuleComponent implements OnInit {

    mainMenus = [];
    // searchBy = 1;
    isPartRange = true;
    genericList: any = [];
    genericMiniList: any = [];
    isValidFormSubmitted = false;
    fromgenricDate: any;
    toDate: any;
    part = {
        searchFor: '',
        searchTo: '',
        searchBy: 'PARTLIST'
    };
    fromDate: any;
    errorMessage: any;
    isSerachFirst: Boolean = false;
    isSalesOrderAccessable: Boolean = false;
    isPartAccessable: Boolean = false;
    isRatesAccessable: Boolean = false;
    isGenericAccessable: Boolean = false;

    claims: any;
    isSuperAdmin: boolean = false;
    bsRangeValue: Date[];


    showResult: boolean;
    miniGennyList: any = [];
    constructor(
        @Inject('apiUrls') private apiUrls,
        private event: Events,
        private router: Router,
        private partAndBomService: PartAndBomService,
        private partDetailService: PartDetailService,
        private setting: SettingService,
        public storage: StorageService,
        public genericService: GenericService,
        public toastService: ToastService,

        public userService: UserService) {
        // setTheme('bs3');

    }

    ngOnInit() {

        let userData = this.storage.getObject('userItem');
        let roles = JSON.parse(userData.roles);

        if (roles.indexOf('superadmin') > -1) {
            this.isSuperAdmin = true;
        }
        if (roles.indexOf('sales') > -1 || this.isSuperAdmin) {
            this.isSalesOrderAccessable = true;
            this.setting.emitChildRender();
        }
        if (roles.indexOf('parts') > -1 || this.isSuperAdmin) {
            this.isPartAccessable = true;
            this.setting.emitChildRender();
        }
        if (roles.indexOf('rates') > -1 || this.isSuperAdmin) {
            this.isRatesAccessable = true;
            this.setting.emitChildRender();
        }
        if (roles.indexOf('generic') > -1 || this.isSuperAdmin) {
            this.isGenericAccessable = true;
            this.setting.emitChildRender();
            this.getUserDetail();

        }

        this.setting.emitChildRender();
        this.fromDate = moment().format('DD MMM YYYY').toLocaleUpperCase();
        var type = this.getCurrentType();
        if (type) {
            this.part.searchBy = type;

            if (localStorage.searchfor) {
                this.part.searchFor = localStorage.searchfor;
                this.part.searchTo = localStorage.searchfor;
                this.isSerachFirst = true;
            }

            this.partSearchByChange();
        }
        else {
            this.setCurrentTypeSearch('PARTLIST');
        }
        this.fromgenricDate = new Date().toJSON().split('T')[0];
        this.toDate = new Date().toJSON().split('T')[0];
    }

    onPartFormChange(searchFor) {
        if (this.part.searchBy !== 'PARTSEARCH')
            this.part.searchTo = searchFor.value;
        else
            this.part.searchTo = '';
    }

    partSearchByChange() {
        if (this.part.searchBy == 'PARTLIST') {
            this.isPartRange = true;
            this.part.searchTo = this.part.searchFor;
        }
        else
            this.isPartRange = false

        this.setCurrentTypeSearch(this.part.searchBy);
    }

    partSearchFormSubmit(form: NgForm) {

        this.isValidFormSubmitted = false;
        if (form.invalid) {
            return;
        }
        localStorage.removeItem('searchfor');
        this.errorMessage = null;
        // this.part = form.value;
        var params: any = {};// new HttpParams();
        params.searchFor = this.part.searchFor;
        if (this.part.searchTo) {
            params.searchTo = this.part.searchTo;
        }
        params.searchBy = this.part.searchBy;
        this.event.publish('loaderShow', true);
        localStorage.setItem('IsFirstLoadNarative', 'true');
        localStorage.setItem('IsFirstLoadPartDetail', 'true');
        var searchfor = params.searchfor;
        this.partDetailService.formSubmit(null);
        var requestCall = this.partAndBomService.search(params, true, searchfor, false);
        // if (this.part.searchBy == 3)
        //     requestCall = this.partAndBomService.partNarrSearch(params);

        requestCall.subscribe(res => {
            this.event.publish('loaderShow', false);
            this.storage.removeItem('setPartData');
            this.storage.removeItem('setPartParamsData');
            // this.storage.removeItem('currentTab');
            this.storage.removeItem('searchOptionsData');
            this.storage.removeItem('isPartNarritive');
            if (res.result.length == 0) {
                this.errorMessage = "No Record Found";
            }
            else if (res.result.length == 1) {
                this.storage.setObject('setPartData', res);
                this.router.navigate(['/extranet/partsearchdetail/' + res.result[0].parT1_PART + '/partDetail']);
            }
            else {
                this.storage.setObject('setPartData', "Data");
                this.storage.setValue('isFxrate', 'false');
                this.partAndBomService.setPartData(res);
                this.storage.setObject('setPartParamsData', this.part);
                this.router.navigate(['/extranet/search/partsandbom/partsearch']);
            }

        }, error => {
            console.log(error);
        })
    }

    fxRatesFormSubmit(form: NgForm) {
        this.isValidFormSubmitted = false;
        // if (form.invalid) {
        //     return;
        // }
        this.event.publish('loaderShow', true);
        localStorage.removeItem('searchfor');
        this.errorMessage = null;
        this.part = form.value;
        var params: any = {};
        params.DateFrom = moment().format("DD-MMM-YYYY");
        this.partAndBomService.getPartDetailData(params, true, params.DateFrom, this.apiUrls.stock.getFxRates, false).subscribe(res => {
            this.event.publish('loaderShow', false);
            this.partAndBomService.setPartData(res);
            this.router.navigate(['/extranet/fxrates']);
        });
    }

    salesOrderFormSubmit() {
        this.event.publish('loaderShow', true);
        this.partAndBomService.getPartDetailData(null, true, null, this.apiUrls.salesOrder.getSales, false).subscribe(res => {
            this.event.publish('loaderShow', false);
            this.partAndBomService.setPartData(res);
            this.router.navigate(['/extranet/salesOrder']);
        });
    }


    routeChange() {
        this.partAndBomService.setPartParamsData(null);
        this.partAndBomService.setPartData(null);
    }

    setCurrentTypeSearch(type) {
        localStorage.setItem('currentType', type);
    }


    getCurrentType() {
        return localStorage.getItem('currentType');
    }


    formatMiniGenny() {
        this.miniGennyList = _.chain(this.genericList).map(item => {
            if (!item.groupName) {
                let splitedName = item.name.split('-');
                if (splitedName.length == 1) {
                    splitedName = item.name.split('–');
                }
                item.groupName = splitedName[0].trim();
                if (splitedName.length == 1) {
                    item.displayText = splitedName[0].trim();
                } else {
                    item.displayText = splitedName.slice(1).join('-').trim();
                }
            } else {
                item.displayText = item.name;
            }
            return item;
        }).groupBy('groupName')
            .map((value, key) => ({ groupName: key, items: value }))
            .value();


    }

    getUserDetail() {
        this.genericService.getGenericDropDownFormatData(this.isSuperAdmin)
            .then((res) => {
                this.genericList = res;
                this.formatMiniGenny();
            }, () => {

            });

    }

    dateRangeUpdated(value) {
        console.log(value);
    }
}