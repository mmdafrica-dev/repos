import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
@Component({
    selector: 'mmd-date',
    template: `<input type="date" [(ngModel)]="date" (ngModelChange)="onChange()"  class="form-control"/>`,
})
export class MMDDateComponent {
    @Input() date: any;
    @Input('autoCompleteValue') autoCompleteValue: any;
    @Output() dateUpdated = new EventEmitter();
    onChange() {
        this.dateUpdated.emit(moment(this.date).format("DD-MMM-YYYY"));
    }

    ngOnInit() {
        if (this.autoCompleteValue) {
            this.date = new Date(this.autoCompleteValue).toJSON().split('T')[0];
            this.onChange();
        }
    }
}