import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Events } from '../../core/events';
@Component({
    selector: 'mmd-input-text',
    template: `
              <input style="height: 33px;" type="text" requird  [(ngModel)]="search"  (ngModelChange)="onChange()" class="form-control"/>      
             `,
})
export class MMDInputTextComponent implements OnInit {
    search: string;
    @Output() searchUpdated = new EventEmitter();
    @Input('autoCompleteValue') autoCompleteValue: any;

    // constructor(private event: Events) {
    //     this.event.subscribe('partsearchdetails', (value) => {
    //         this.autoCompleteValue = value;
    //     });
    // }

    onChange() {
        this.searchUpdated.emit(this.search);
    }

    ngOnInit() {
        if (this.autoCompleteValue) {
            this.search = this.autoCompleteValue;
            this.onChange();
        }
    }
}