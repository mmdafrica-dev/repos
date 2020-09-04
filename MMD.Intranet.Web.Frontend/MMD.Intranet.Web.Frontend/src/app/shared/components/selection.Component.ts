import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'mmd-selection',
    template: `<select style="height: 33px;"  class="form-control" [(ngModel)]="selectedModelValue" (ngModelChange)="onChange()">
                    <option [ngValue]="item.key" *ngFor="let item of data">{{item.value}}</option>
            </select>`,
})
export class MMDSelectionComponent implements OnInit {
    @Input() data: any;
    @Input() selcetionName: any;
    @Input('selcetValue') selcetValue: any;
    selectedModelValue: any;
    @Output() selectionUpdated = new EventEmitter();

    onChange() {
        this.selectionUpdated.emit({ value: this.selectedModelValue, name: this.selcetionName });
    }

    ngOnInit() {
        if (this.selcetValue) {
            this.selectedModelValue = this.selcetValue.toString();
            this.onChange();
        }
    }

}