import { Component, OnInit, Input, Output } from '@angular/core';
import { Events } from '../../../../core/events';
import { NgxSpinnerService } from 'ngx-spinner';
import { StorageService } from '../../../../core/services/storage.service';
import { AutoCompleteLookUpService } from '../auto-complete-lookup.service';
import { GenericService } from '../../services/generic.service';
import { BsModalService } from 'ngx-bootstrap';
import { AutoCompleteModalComponent } from '../auto-complete-modal/auto-complete-modal';
import { Router } from '@angular/router';
import { ToastService } from '../../../../core/services/toast.service';
import * as moment from 'moment';
import * as _ from "lodash";

@Component({
    selector: 'mini-genny',
    templateUrl: 'mini-genny.html',
    styleUrls: ['mini-genny.scss']
})
export class MiniGennyComponent implements OnInit {

    constructor(private event: Events, private spinnerService: NgxSpinnerService,
        public storage: StorageService, private modalService: BsModalService,
        private router: Router, public toastService: ToastService,
        public genericService: GenericService, private autoCompleteLookUpService: AutoCompleteLookUpService, ) { }

    @Input() gennyName = '';
    @Input() genericList: any = [];
    selectedGenric: any = null;
    selectedGenricValue: any = '';

    isValidFormSubmitted = false;
    fromgenricDate: any;
    toDate: any;
    datepickerFromModel: Date;
    datepickerPeriodYearFromModel: any;
    datepickerPeriodYearToModel: any;
    datepickerToModel: Date;
    periodTo: any;
    periodFrom: any;
    toFocus: boolean = true;
    toFocusOut: boolean = false;
    fromFocus: boolean = true;
    fromFocusOut: boolean = false;
    textToModel: string = '';
    textFromModel: string = '';

    dropDown1Options: any = [];
    dropDown2Options: any = [];
    fromHeader = {};
    toHeader = {};
    autoCompleteFrom: any = {
        type: 'from',
        isShow: false,
        headers: [],
        items: []
    };
    autoCompleteTo: any = {
        type: 'from',
        isShow: false,
        headers: [],
        items: []
    };
    showResult: boolean;

    periodYears = this.getPeriodYears();

    ngOnInit() {

        let selectedGenric = this.storage.getObject(this.getSelectedGenricKey());
        if (selectedGenric) {
            this.selectedGenricValue = selectedGenric.name;
            this.onChangeSelection();
        } else {
            selectedGenric = this.storage.getObject('selectedGenric');
            if (selectedGenric) {
                let isSameAsOldList = _.find(this.genericList, { name: selectedGenric.name });
                if (isSameAsOldList) {
                    this.selectedGenricValue = selectedGenric.name;
                    this.onChangeSelection();
                }
            }
        }

        let scrollY = this.storage.getObject('selectedGenricScrollPosition');
        if (scrollY) {
            window.scrollTo(0, scrollY);
        }

    }


    getPeriodYears() {
        let periodYears = [];
        let startYear = 2010;
        let currentYear = moment().year() + 1;
        for (let i = startYear; startYear <= currentYear; startYear++) {
            periodYears.push({ text: startYear.toString(), value: startYear.toString() });
        }
        return periodYears;
    }


    getSelectedGenricKey() {
        return 'selectedGenric' + this.gennyName;
    }

    getGenericDropDownData(spName, startAt, fromOrTo) {
        return new Promise((resolve, reject) => {
            let params: any = {};
            if (startAt) {
                params.startAt = startAt;
            } else {
                params.startAt = '';
            }
            this.spinnerService.show();
            this.event.publish('loadingText', 'Gathering Data.....');
            this.genericService.getGenericDropDownData(spName, params).subscribe(res => {
                this.event.publish('loadingText', 'Formatting Display .....');
                if (fromOrTo === 'from') {
                    this.initAutoComplete('from');
                    let result = this.autoCompleteLookUpService.formatOptionResponse(res);
                    this.autoCompleteFrom = result;
                } else {
                    this.initAutoComplete('to');
                    let result: any = this.autoCompleteLookUpService.formatOptionResponse(res);
                    this.autoCompleteTo = result;
                }
                this.spinnerService.hide();
                this.event.publish('initLoadingText');
                return resolve();
            }, err => {
                this.spinnerService.hide();
                this.event.publish('initLoadingText');
                return reject(err);
            });
        });
    }

    initAutoComplete(type) {
        if (type == 'from') {
            this.autoCompleteFrom = {
                type: 'from',
                isShow: false,
                headers: [],
                items: []
            };
        }
        else {
            this.autoCompleteTo = {
                type: 'to',
                isShow: false,
                headers: [],
                items: []
            };
        }
    }



    focusTextFromModel(filter) {
        if (filter.dropDown1) {
            this.dropDown1Options = [];
            this.getGenericDropDownData(filter.dropDown1, null, 'from').then(() => {
                this.autoCompleteFrom.type = 'from';
                this.autoCompleteFrom.isShow = true;
                this.autoCompleteFrom.title = filter.dropDownTitle1;
                let cssClass = 'modal-full';
                const modalRef = this.modalService.show(AutoCompleteModalComponent, { class: cssClass });
                modalRef.content.autoComplete = this.autoCompleteFrom;
                const subscribeModelHidden = this.modalService.onHidden.subscribe(res => {
                    if (subscribeModelHidden) {
                        subscribeModelHidden.unsubscribe();
                    }
                    if (modalRef.content.setValueParent) {
                        this.textFromModel = modalRef.content.setValueParent;
                        // this.textToModel = '';
                    }
                });
            });
        } else {
            this.initAutoComplete('from');
        }

    }



    focusTextToModel(filter) {
        if (filter.dropDown2) {
            this.dropDown2Options = [];
            let startAt = null;
            if (filter.startAt2) {
                startAt = this.textFromModel;
            }
            this.getGenericDropDownData(filter.dropDown2, startAt, 'to').then(() => {
                this.autoCompleteTo.type = 'to';
                this.autoCompleteTo.isShow = true;
                this.autoCompleteTo.title = filter.dropDownTitle2;
                const modalRef = this.modalService.show(AutoCompleteModalComponent, { class: 'modal-full' });
                modalRef.content.autoComplete = this.autoCompleteTo;
                const subscribeModelHidden = this.modalService.onHidden.subscribe(res => {
                    if (subscribeModelHidden) {
                        subscribeModelHidden.unsubscribe();
                    }
                    if (modalRef.content.setValueParent) {
                        this.textToModel = modalRef.content.setValueParent;
                    }
                });
            });
        } else {
            this.initAutoComplete('to');
        }
    }


    getGenericFileData() {
    }

    trimValue(value) {
        if (value && typeof (value) === 'string') {
            return value.trim();
        }
        return value;
    }

    submitGenric() {
        if (this.selectedGenric) {
            if (this.selectedGenric.filters) {
                this.selectedGenric.filters.forEach(filter => {
                    switch (filter.type) {
                        case 'date':
                            this.setValueByKey('selectedGenricDateFrom', filter.overrideDisplayLabel1, moment(this.trimValue(this.datepickerFromModel)).format("DD MMM YYYY"));
                            if (filter.isRangeSelectable)
                                this.setValueByKey('selectedGenricDateTo', filter.overrideDisplayLabel2, moment(this.trimValue(this.datepickerToModel)).format("DD MMM YYYY"));
                            break;
                        case 'text':
                            this.setValueByKey('textFromModel', filter.overrideDisplayLabel1, this.trimValue(this.textFromModel));
                            if (filter.isRangeSelectable)
                                this.setValueByKey('textToModel', filter.overrideDisplayLabel2, this.trimValue(this.textToModel));
                            break;
                        case 'period':
                            this.setValueByKey('datepickerPeriodYearFromModel', filter.overrideDisplayLabel1, this.trimValue(this.datepickerPeriodYearFromModel));
                            this.setValueByKey('periodFrom', filter.overrideDisplayLabel1, this.trimValue(this.periodFrom));
                            if (filter.isRangeSelectable) {
                                this.setValueByKey('datepickerPeriodYearToModel', filter.overrideDisplayLabel2, this.trimValue(this.datepickerPeriodYearToModel));
                                this.setValueByKey('periodTo', filter.overrideDisplayLabel2, this.trimValue(this.periodTo));
                            }
                            break;
                    }
                });
            }

            this.storage.removeItem('Genricfilter');
            this.storage.setValue('currentPage', '1');
            this.storage.removeGenny();
            this.storage.setObject(this.getSelectedGenricKey(), this.selectedGenric);
            this.storage.setObject('selectedGenric', this.selectedGenric);
            this.storage.setObject('selectedGenricScrollPosition', document.documentElement.scrollTop);
            this.storage.removeItem('selectedLinkGenric');
            this.router.navigate(['/extranet/generic']);
        }
        else {
            this.storage.removeItem(this.getSelectedGenricKey());
            this.storage.removeItem('selectedGenric');
            this.toastService.tostMessage(4, "error", "please select Data Stream.");
        }

    }

    initFilters() {
        this.datepickerFromModel = null;
        this.datepickerToModel = null;
        this.datepickerPeriodYearFromModel = '';
        this.datepickerPeriodYearToModel = '';
        this.periodFrom = '';
        this.periodTo = '';
        this.textFromModel = '';
        this.textToModel = '';
    }

    onChangeSelection() {
        if (this.selectedGenricValue) {
            this.selectedGenric = _.find(this.genericList, { name: this.selectedGenricValue });
        } else {
            this.selectedGenric = null;
        }
        if (this.selectedGenric && this.selectedGenric.filters) {
            this.initFilters();
            this.selectedGenric.filters.forEach(filter => {
                switch (filter.type) {
                    case 'date':
                        let dateFrom = this.checkValueExist('selectedGenricDateFrom', filter.overrideDisplayLabel1, null);
                        let dateTo = this.checkValueExist('selectedGenricDateTo', filter.overrideDisplayLabel2, null);
                        if (dateFrom) {
                            this.datepickerFromModel = new Date(dateFrom);
                        }
                        if (dateTo && filter.isRangeSelectable) {
                            this.datepickerToModel = new Date(dateTo);
                        }
                        break;
                    case 'text':
                        this.initAutoComplete('from');
                        this.textFromModel = this.checkValueExist('textFromModel', filter.overrideDisplayLabel1, '');
                        if (filter.isRangeSelectable)
                            this.textToModel = this.checkValueExist('textToModel', filter.overrideDisplayLabel2, '');
                        break;
                    case 'period':
                        this.datepickerPeriodYearFromModel = this.checkValueExist('datepickerPeriodYearFromModel', filter.overrideDisplayLabel1, '')
                        this.periodFrom = this.checkValueExist('periodFrom', filter.overrideDisplayLabel1, '');
                        if (filter.isRangeSelectable) {
                            this.datepickerPeriodYearToModel = this.checkValueExist('datepickerPeriodYearToModel', filter.overrideDisplayLabel2, '');
                            this.periodTo = this.checkValueExist('periodTo', filter.overrideDisplayLabel2, '');
                        }
                        break;
                }
            });
        } else {
            this.initFilters();
        }

    }

    setValueByKey(model, label, value) {
        let key = model;
        if (label) {
            key += label;
        }

        this.storage.setValue(key, value);
    }

    checkValueExist(model, label, defaultReturnValue) {
        let key = model;
        if (label) {
            key += label;
        }
        let value = this.storage.getValue(key);
        if (value) {
            return value;
        } else {
            return defaultReturnValue;
        }
    }



    fromFocusFunction() {
        this.fromFocus = false;
        this.fromFocusOut = true;

    }

    fromFocusOutFunction() {
        setTimeout(() => {
            this.fromFocus = true;
            this.fromFocusOut = false;
        }, 500);
    }

    toFocusOutFunction() {
        setTimeout(() => {
            this.toFocus = true;
            this.toFocusOut = false;
        }, 500);
    }

    toFocusFunction() {
        this.toFocus = false;
        this.toFocusOut = true;
    }
}