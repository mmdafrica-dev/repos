import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';


@Component({
    selector: 'mmd-part-number',
    template: `<input style="height: 33px;" type="number" class="form-control"   [(ngModel)]="selectedValue" (ngModelChange)="onChange()"/>`,
})
export class MMDPartNumberComponent implements OnInit {
    @Input() placeholder: string = '';
    selectedValue: number;
    @Output() numberUpdated = new EventEmitter();

    onChange() {
        this.numberUpdated.emit(this.selectedValue);
    }
    ngOnInit() {
        this.selectedValue = 1;
        this.numberUpdated.emit(this.selectedValue);
    }

}