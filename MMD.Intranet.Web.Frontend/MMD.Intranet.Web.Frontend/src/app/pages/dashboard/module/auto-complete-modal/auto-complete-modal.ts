import { Component, OnInit, Input, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { EventEmitter } from 'events';

@Component({
    selector: 'auto-complete-modal',
    templateUrl: 'auto-complete-modal.html',
    styleUrls: ['auto-complete-modal.scss']
})
export class AutoCompleteModalComponent implements OnInit {
    @Input() title: any;
    @Input() autoComplete: any = {};
    setValueParent: any;
    innerWidth: any;
    configHeight = 1920;
    configFontSize = 14;
    rowsOnPage = 10;
    currentFilter = [];
    filterQuery = "";
    filterBycolumn = "";
    filterIndex: any;
    constructor(public activeModal: BsModalRef) {

    }

    ngOnInit() {
        this.innerWidth = window.innerWidth;
    }

    ngAfterContentInit() {

    }

    hide() {
        this.activeModal.hide();
    }

    headerChange(f, index) {
        this.filterQuery = f;
        this.filterIndex = index;
        this.checkFilter();
    }

    clear(val) {
        val.text = "";
        this.filterQuery = "";
        this.checkFilter();
    }

    setValue(value) {
        this.setValueParent = value;
        this.hide();
    }

    checkFilter() {
        this.autoComplete.headers.forEach(record => {
            let isValueExist = true;
            this.currentFilter.forEach(filter => {
                if (filter.key == record.namefilter) {
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
                            index: this.filterIndex,
                            key: record.namefilter,
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