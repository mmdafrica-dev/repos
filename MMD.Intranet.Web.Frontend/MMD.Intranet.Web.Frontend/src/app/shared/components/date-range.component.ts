import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
@Component({
    selector: 'mmd-date-range',
    template: `  <input type="date" style="height: 33px;margin-bottom: 1%" [(ngModel)]="fromDate" (ngModelChange)="onChange()" class="form-control"/>
                 <input type="date" style="height: 33px;margin-bottom: 1%" [(ngModel)]="toDate" (ngModelChange)="onChange()" class="form-control"/>
                 `,
})
export class MMDDateRangeComponent implements OnInit {
    @Input() searchOptions: any;
    @Input() colmd: any;
    fromDate: any;
    toDate: any;
    constructor() {

    }
    @Output() dateRangeUpdated = new EventEmitter();
    onChange() {
        // if (new Date(this.fromDate) > new Date(this.toDate) || this.fromDate == undefined) {
        //     if (this.fromDate == undefined)
        //         this.fromDate = new Date(this.toDate).toJSON().split('T')[0];
        //     else
        //         this.toDate = new Date(this.fromDate).toJSON().split('T')[0];

        //     // console.log(new Date(this.fromDate) < new Date(this.toDate))
        //     // // if (new Date(this.fromDate) < new Date(this.toDate)) {
        //     // //     alert();
        //     // //     this.fromDate = new Date(this.toDate).toJSON().split('T')[0];
        //     // // }

        // }
        // else {

        // this.dateRangeUpdated.emit({ from: this.fromDate, to: this.toDate });
        this.dateRangeUpdated.emit({ from: moment(this.fromDate).format("DD-MMM-YYYY"), to: moment(this.toDate).format("DD-MMM-YYYY") });
        // }
    }

    ngOnInit() {
        this.fromDate = new Date().toJSON().split('T')[0];
        this.toDate = new Date().toJSON().split('T')[0];
        this.onChange();
    }
}