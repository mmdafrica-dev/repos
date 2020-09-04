import { Component, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';
import { OnChange } from 'ngx-bootstrap/utils/decorators';
import { PartDetailService } from '../../../pages/dashboard/services/part-detail.service';
@Component({
    selector: 'mmd-table',
    templateUrl: './table.html',
    styleUrls: ['./table.scss']
})
export class MMDTableComponent implements OnChanges {
    @Input() data: any = [];
    @Input() header: any = [];
    @Input() isSearch: boolean;
    @Input() islink: boolean;
    filterQuery = "";
    rowsOnPage = 50;
    sortBy = "name";
    sortOrder = "name";
    filterBycolumn = "name";

    apiLink: string;
    dataTable = false;
    currentFilter = [];
    constructor(private partDetailService: PartDetailService) {

    }
    // @Output() dateUpdated = new EventEmitter();
    onChange() {
        // this.dateUpdated.emit(this.date);
    }
    /**
     *
     */
    MMDTableComponent() {
    }

    ngOnChanges(changes: SimpleChanges) {
        // if (changes['header'] && JSON.stringify(changes['hearder'].previousValue) !== JSON.stringify(changes['hearder'].currentValue)) {

        // }
    }

    headerChange(f) {
        this.filterQuery = f;
        this.checkFilter();
    }

    checkFilter() {
        this.header.forEach(record => {
            let isValueExist = true;
            this.currentFilter.forEach(filter => {
                if (filter.key == record.name) {
                    if (record.text) {
                        filter.value = record.text;
                        isValueExist = false;
                    }
                    else {
                        filter.value = '';
                        isValueExist = false;
                    }
                }
            });
            if (isValueExist) {
                if (record.text) {
                    this.currentFilter.push(
                        {
                            key: record.name,
                            value: record.text
                        });
                }
            }
        });

        for (var i = 0; i < this.currentFilter.length; i++) {
            if (this.currentFilter[i].value === "") {
                this.currentFilter.splice(i, 1);
            }
        }
    }
    partClick() {
        this.partDetailService.formSubmit(null);

        // setTimeout(() => {
        //     window.location.reload();
        // }, 200);
    }
}