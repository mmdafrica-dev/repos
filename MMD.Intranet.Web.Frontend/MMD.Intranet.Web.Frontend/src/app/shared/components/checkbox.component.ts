import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'mmd-checkbox',
    template: `  
        <div class="checkbox" style="margin-top: -8px;">              
                    <input type="checkbox" id="checkbox1" [(ngModel)]="currentValue" (ngModelChange)="onChange()" />
                    <label for="checkbox1"></label>
              </div>
                `,
    styleUrls: ['./checkbox.component.scss']
})
export class MMDCheckboxComponent {
    currentValue: boolean = false;
    @Output() checkboxUpdated = new EventEmitter();

    onChange() {
        this.checkboxUpdated.emit(this.currentValue);
    }
}