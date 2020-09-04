import { Component, OnInit, OnDestroy } from '@angular/core';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgStyle, NgClass } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import * as moment from 'moment';
import * as _ from "lodash";
import { StorageService } from '../../../core/services/storage.service';
import { forEach } from '@angular/router/src/utils/collection';
import { Events } from '../../../core/events';
@Component({
    selector: 'multi-level-bom',
    templateUrl: 'multi-level-bom.component.html',
    styleUrls: ['multi-level-bom.component.scss']
})
export class MultiLevelBomComponent implements OnInit, OnDestroy {
    data: any = [];
    header: any = [];
    formSubmitSubscribe: any;
    // sortBy: any;
    // sortOrder: any;
    operationsData: any;
    operationsHeader: any;
    result: any;
    filterQuery = "";
    rowsOnPage = 200;
    sortBy = "name";
    sortOrder = "name";
    filterBycolumn = "name";
    apiLink: string;
    dataTable = false;
    currentFilter = [];
    constructor(
        private partAndBomService: PartAndBomService,
        private partDetailService: PartDetailService,
        public storage: StorageService,
        private event: Events,
        private router: Router,
    ) {
        this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
            this.multiLevelBom(params, true, true);
        });
    }
    ngOnInit() {
    }

    ngOnDestroy() {
        this.unSubscribeAll();
    }

    multiLevelBom(parameter, force: boolean, isback) {
        var searchfor = parameter.searchfor;
        localStorage.setItem('searchfor', searchfor);
        if (this.storage.getValue('currentTab')) {
            isback = false;
            force = false;
        }
        this.header = [];
        this.data = [];
        this.event.publish('loaderShow', true);
        this.partAndBomService.bomMultiLevel(parameter, force, searchfor, isback)
            .subscribe(result => {
                this.event.publish('loaderShow', false);
                if (result) {
                    this.header = result.headers;
                    // this.data = result.result;
                    this.get_children(result.result, null);
                    if (this.data.length > 0) {
                        this.onClickRow(this.data[0], true);
                    }
                    if (isback)
                        this.router.navigate(['/extranet/partsearchdetail/' + searchfor + '/multilevelbom']);
                }
                else {
                    localStorage.setItem('currentType', 'PARTLIST');
                    this.router.navigate(['/extranet/dashboard/']);
                }
            }, error => {
                this.event.publish('loaderShow', false);
            });
    }
    partDetailClick() {
        this.formUnSubscribe();
        this.storage.setValue('currentTab', 'multilevelbom');
    }


    headerChange(f) {
        this.filterQuery = f;
        this.checkFilter();
    }

    checkFilter() {
        this.operationsHeader.forEach(record => {
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



    unSubscribeAll() {
        this.formUnSubscribe();
    }
    formUnSubscribe() {
        if (this.formSubmitSubscribe) {
            this.formSubmitSubscribe.unsubscribe();
            this.formSubmitSubscribe = null;
        }
    }

    get_children(data, parent) {
        let i = 0
        while (i < data.length) {
            let current_parent = data[i];
            let currentItem = _.clone(data[i]);
            delete currentItem.children;
            currentItem.childrenLength = current_parent.children.length;
            if (parent) {
                currentItem.parent = parent['id'];
                currentItem.class = 'indent' + currentItem.level;
                currentItem.isOpen = false;
            } else {
                currentItem.parent = null;
                currentItem.isOpen = true;
            }
            this.data.push(currentItem)
            if (current_parent.children.length < 0) {
                continue;
            }
            else if (current_parent.children.length > 0) {
                this.get_children(current_parent.children, current_parent);
            }
            i++
        }
    }

    onClickRow(item, status) {
        let items = _.filter(this.data, { parent: item['id'] });

        item.isopen = status;
        _.each(items, (iteme) => {
            iteme.isOpen = status;
            if (status) {
                iteme.class = 'indent' + item.gtT_LEVEL;
            }
            if (!status)
                this.onClickRow(iteme, status)
        });
    }


}
