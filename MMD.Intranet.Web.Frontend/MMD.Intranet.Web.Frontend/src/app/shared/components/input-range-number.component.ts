import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'mmd-input-range-number',
    template: `
    <input style="height: 40px;" type="number" class="form-control"  [(ngModel)]="startNumber" (ngModelChange)="onChange()"/>
    <input style="height: 40px;" type="number" class="form-control" min="{{startNumber}}" [(ngModel)]="endNumber" (ngModelChange)="onChange()"/>
    `,
})
export class MMDInputNumberComponent {
    @Input() startNumber: number = 0;
    @Input() endNumber: number = 0;
    @Output() rangeNumberUpdated = new EventEmitter();

    onChange() {
        if (this.startNumber < this.endNumber)
            this.rangeNumberUpdated.emit({ from: this.startNumber, to: this.endNumber });
        else
            this.endNumber = this.startNumber + 1;
    }
}