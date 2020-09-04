import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, ChangeDetectorRef } from '@angular/core';

import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { PartAndBomService } from '../../pages/part-and-bom/services/part-and-bom.service';
@Component({
    selector: 'mmd-parts-number',
    template: ` <input [(ngModel)]="asyncSelected"  [typeahead]="dataSource" (typeaheadOnSelect)="typeaheadOnSelect($event)" typeaheadOptionsLimit="7" typeaheadOptionField="parT1_PART" placeholder="Part Number" class="form-control">`,
})
export class MMDPartsNumberComponent implements OnInit {
    @Input() placeholder: string = '';
    @Input('autoCompleteValue') autoCompleteValue: any;
    selectedValue: number;
    @Output() partNumberUpdated = new EventEmitter();
    public asyncSelected: string;
    public typeaheadLoading: boolean;
    public typeaheadNoResults: boolean;
    public dataSource: Observable<any>;
    constructor(private partService: PartAndBomService, private changeDetectorRef: ChangeDetectorRef) {
        this.dataSource = Observable
            .create((observer: any) => {
                // Runs on every search
                observer.next(this.asyncSelected);
            })
            .mergeMap((token: string) => this.getStatesAsObservable(token));

    }
    ngOnInit() {

    }

    getStatesAsObservable(token: string): Observable<any> {
        return this.partService.partBrowse(token);
    };


    typeaheadOnSelect(e: TypeaheadMatch): void {
        this.partNumberUpdated.emit(e.value);
    }


    // ngOnChanges(changes: SimpleChanges) {
    //     if (changes['autoCompleteValue'] && JSON.stringify(changes['autoCompleteValue'].previousValue) != JSON.stringify(changes['autoCompleteValue'].currentValue)) {
    //         this.asyncSelected = this.autoCompleteValue;
    //         console.log('THIS CALLED 2');

    //         this.changeDetectorRef.detectChanges();
    //     }
    // }
}