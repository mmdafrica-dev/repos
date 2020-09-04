import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { StorageService } from 'app/core/services/storage.service';
import * as _ from 'lodash';
@Component({
    selector: 'additional-filter-modal',
    templateUrl: './additional-filter-modal.html',
    styleUrls: ['./additional-filter-modal.scss']
})
export class AdditionalFilterModalComponent implements OnInit {
    @Input() title: any;
    @Input() additionalFilter: any = {};
    setValueParent: any;
    innerWidth: any;
    configHeight = 1920;
    configFontSize = 14;
    rowsOnPage = 10;
    currentFilter = [];
    filterQuery = "";
    filterBycolumn = "";
    filterIndex: any;

    constructor(public activeModal: BsModalRef, private storageService: StorageService) {

    }

    ngOnInit() {
        this.innerWidth = window.innerWidth;
        setTimeout(() => {
            this.getInitFilter();
            this.calculateWidth();
        });
    }



    getInitFilter() {

        if (this.additionalFilter.panelsFilters && this.additionalFilter.panelsFilters.length > 0) {
            this.additionalFilter.headers.forEach(record => {
                this.additionalFilter.panelsFilters.forEach(filter => {
                    if (filter.key == record.name) {
                        if (filter.value) {
                            record.text = filter.value;
                        }
                    }
                });
            });
            this.currentFilter = this.additionalFilter.panelsFilters;
        }
    }

    calculateWidth() {
        let maxWidth = (this.additionalFilter.hearderList.length * 30)
        this.additionalFilter.width = !this.additionalFilter.hearderList.length ? 30 : (maxWidth > 100 ? 100 : maxWidth);
        this.additionalFilter.pannelWidth = !this.additionalFilter.hearderList.length ? 100 : (100 / this.additionalFilter.hearderList.length);
    }

    ngAfterContentInit() {

    }

    hide() {
        _.each(this.additionalFilter.hearderList, (headerItem) => {
            _.each(headerItem, (item) => {
                item.text = '';
            });
        });
        this.activeModal.hide();
    }

    headerChange(f, index) {
        this.filterQuery = f;
        this.filterIndex = index;
    }

    clear(val) {
        val.text = '';
        this.filterQuery = '';
    }

    setValue() {
        this.checkFilter();
        this.setValueParent = this.currentFilter;
        this.hide();
    }


    checkFilter() {

        this.additionalFilter.headers.forEach(record => {
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
                    this.currentFilter.push({
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

    getFontSize() {
        if (this.innerWidth && this.innerWidth === this.configHeight) {
            return this.configFontSize;
        } else {
            return ((this.innerWidth / this.configHeight) * this.configFontSize);
        }
    }

    getWidth(width, addWidth = 0) {
        if (this.innerWidth && this.innerWidth === this.configHeight) {
            return width;
        } else {
            return ((this.innerWidth / this.configHeight) * width) + addWidth;
        }
    }


}