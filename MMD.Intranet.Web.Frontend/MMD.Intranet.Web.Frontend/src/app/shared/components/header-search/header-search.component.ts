import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { SettingService } from '../../../core/services/setting.service';
import { Router, ActivatedRoute, ParamMap, Params } from '@angular/router';
import { Events } from '../../../core/events';
import { StorageService } from '../../../core/services/storage.service';
import { Location } from '@angular/common';
@Component({
    selector: 'header-search',
    templateUrl: './header-search.component.html',
    styleUrls: ['./header-search.component.scss']
})
export class HeaderSearchComponent implements OnInit {
    @Input('searchOptions') searchOptions: any;
    @Input('autoComplete') autoComplete: any;
    @Input('selcetionValue') selcetionValue: any;

    @Output('headerSearchUpdated') headerSearchUpdated = new EventEmitter();
    isFormValid = false;
    selcetedModelValue: any;
    autoCompleteValue: any;
    param = {};
    search = {
        searchfor: { searchfor: '', required: false },
        numberrange: { from: 0, to: 0, required: false },
        date: { date: '', required: false },
        daterange: { dateFrom: '', dateTo: '', required: false },
        // textrange: { from: '', to: '', required: false },
        selection: { selection: '', required: false },
        number: { quantity: 0, required: false },
        checkbox: { checkbox: false, required: false },
        partNumber: { partNumber: '', required: false },
        textrange: { textrange: this.param, required: false }
    };
    constructor(private event: Events,
        private setting: SettingService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        public storage: StorageService,
        public location: Location,
    ) {


    }
    onChange() {

    }

    ngOnInit() {

        this.autoCompleteValue = this.autoComplete;
        let res = this.searchOptions;
        res.forEach(element => {
            let prop = element.type.toLowerCase();

            if (this.search[prop])
                this.search[prop].required = element.required;
        });
        this.validateForm();

        this.setting.emitChildRender();
        this.selcetedModelValue = this.selcetionValue;
        if (this.autoCompleteValue) {
            setTimeout(() => {
                this.submit(true);
            }, 200);
        }
    }

    submit(val) {
        if (!val) {
            this.storage.removeItem('currentTab');
        }
        // this.activatedRoute.params.subscribe((params: Params) => {
        //     console.log(this.location.path());
        //     var location = this.location.path().split('/');
        //     console.log(location[location.length - 1]);
        //     console.log(params['partNumber']);

        // });
        var params = this.setQueryParams();
        this.headerSearchUpdated.emit(params);
    }

    handleSearchUpdate(search) {
        this.search['searchfor'].searchfor = search;
        this.validateForm();
    }

    handleTextRangeUpdate(data) {
        this.search['textrange'].textrange = data;
        this.validateForm();
    }

    handleDateUpdate(date) {
        this.search['date'].date = date;
        this.validateForm();
    }

    handleSelectionUpdate(selectedvalue) {
        this.search['selection'].selection = selectedvalue.value;
        this.validateForm();
    }
    handleDateRangeUpdate(daterange) {
        this.search['daterange'].dateFrom = daterange.from;
        this.search['daterange'].dateTo = daterange.to;
        this.validateForm();
    }

    handleRangeNumberUpdated(rangeNumber) {
        this.search['numberrange'].from = rangeNumber.from;
        this.search['numberrange'].to = rangeNumber.to;
        this.validateForm();

    }

    handleNumberUpdated(number) {
        this.search['number'].quantity = number;
        this.validateForm();
    }

    handleCheckboxUpdated(value) {
        this.search['checkbox'].checkbox = value;
        this.validateForm();
    }
    handlePartNumberUpdated(value) {
        this.search['partNumber'].partNumber = value;
        this.validateForm();
    }
    resetForm() {
        this.search.searchfor.searchfor = '';
        this.search.numberrange.from = 0;
        this.search.numberrange.to = 0;
        this.search.date.date = '';
        this.search.daterange.dateFrom = '';
        this.search.daterange.dateTo = '';
        // this.search.textrange.from = '';
        // this.search.textrange.to = '';
        this.search.selection.selection = '';
        this.search.partNumber.partNumber = '';
    }

    handleError(error) {

    }

    validateForm() {
        var validate = true;
        if (this.search['searchfor'].required) {
            if (this.search['searchfor'].searchfor == '')
                validate = false;
            else
                validate = true;
        }
        if (this.search['date'].required) {
            if (this.search['date'].date == '')
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['selection'].required) {
            if (this.search['selection'].selection == '')
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['daterange'].required) {
            if (this.search['daterange'].dateFrom == '' || this.search['daterange'].dateTo == '')
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['numberrange'].required) {
            if (this.search['numberrange'].from == 0)
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['number'].required) {
            if (this.search['number'].quantity == 0)
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['checkbox'].required) {
            if (this.search['checkbox'].checkbox == false)
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['partNumber'].required) {
            if (this.search['partNumber'].partNumber == '')
                validate = false;
            else {
                if (validate)
                    validate = true;
            }
        }
        if (this.search['textrange'].required) {
            if (this.search['textrange'].textrange == null) {
                validate = false;
            }
            //|| this.search['textrange'].textrange.paramsMap.size == 0
        }
        this.isFormValid = validate;
    }


    setQueryParams() {
        var params: any = {};
        if (this.search['textrange'].textrange) {
            if (this.search['textrange'].textrange != null) {
                params = this.search['textrange'].textrange;
            }
        }
        if (this.search.searchfor.searchfor) {
            params.searchfor = this.search.searchfor.searchfor;
        }
        if (this.search.numberrange.to > 0) {
            params.numberrangeTo = this.search.numberrange.to.toString();
        }
        if (this.search.numberrange.from > 0) {
            params.numberrangeFrom = this.search.numberrange.from.toString();
        }
        if (this.search.date.date) {
            params.date = this.search.date.date;
        }
        if (this.search['daterange'].dateFrom) {
            params.dateFrom = this.search.daterange.dateFrom;
        }
        if (this.search.daterange.dateTo) {
            params.dateTo = this.search.daterange.dateTo;
        }
        if (this.search.selection.selection) {
            params.selection = this.search.selection.selection;
        }

        // if (this.search.textrange.from) {
        //     params.set('textrangeFrom', this.search.textrange.to);
        // }
        // if (this.search.textrange.to) {
        //     params.set('textrangeTo', this.search.textrange.to);
        // }
        if (this.search.number.quantity > 0) {
            params.quantity = this.search.number.quantity.toString();
        }
        if (this.search.checkbox.checkbox) {
            params.checkbox = this.search.checkbox.checkbox.toString();
        }
        if (this.search.partNumber.partNumber != '') {
            params.partNumber = this.search.partNumber.partNumber;
        }

        return params;
    }

}