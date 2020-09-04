import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs/Observable';


declare var localStorage;
@Component({
    selector: 'mmd-input-text-range',
    templateUrl: './input-text-range.html',
    styleUrls: ['./input-text-range.scss']
})
export class MMDInputTextRangeComponent implements OnInit {
    // @Input() startNumber: number = 0;
    // @Input() endNumber: number = 0;
    part = {
        searchFor: '',
        searchTo: '',
        searchBy: 'PARTLIST'
    };
    isPartRange: boolean = true;

    /**
     *
     */
    constructor(private element: ElementRef) {


    }
    @Input('autoCompleteValue') autoCompleteValue: any;

    @Output() textRangeUpdated = new EventEmitter();

    partSearchFormSubmit(event) {
        if (event) {
            let observable = Observable.fromEvent(event.target, 'keyup')
                .map(value => event.target.value)
                .debounceTime(500)
                .distinctUntilChanged();
            observable.subscribe((data) => {
                this.submitForm();
            });
        }
    }

    submitForm() {
        if (this.part.searchFor) {
            var params: any = {};

            params.searchFor = this.part.searchFor;
            if (this.part.searchTo)
                params.searchTo = this.part.searchTo;
            params.searchBy = this.part.searchBy;
            this.textRangeUpdated.emit(params);
        }
        else {
            this.textRangeUpdated.emit(null);
        }
    }
    partSearchByChange() {
        if (this.part.searchBy === 'PARTLIST') {
            this.isPartRange = true;
            // this.part.searchTo = ;
        }
        else
            this.isPartRange = false;

        this.submitForm();

        localStorage.setItem('currentType', this.part.searchBy.toString());
    }
    ngOnInit() {

        if (this.autoCompleteValue) {
            this.part = this.autoCompleteValue;
            if (this.part.searchBy == 'PARTLIST') {
                this.part.searchBy = 'PARTLIST';
            }
        }
        else {
            this.textRangeUpdated.emit(null);
        }

        var data = localStorage.currentType;
        if (data) {
            this.part.searchBy = data;
            this.partSearchByChange();
        }

    }
}