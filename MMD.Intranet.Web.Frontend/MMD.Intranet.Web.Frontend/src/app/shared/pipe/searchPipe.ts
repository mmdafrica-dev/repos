import * as _ from "lodash";
import { Pipe, PipeTransform } from "@angular/core";
import { StorageService } from '../../core/services/storage.service';
@Pipe({
    name: "dataFilter",
    pure: false
})

export class DataFilterPipe implements PipeTransform {
    /**
     *
     */
    constructor(public storageService: StorageService) {

    }
    transform(array: any[], currentFilter?: any[], query?: string, filterBycolumn?: string): any {
        if (currentFilter.length > 0) {
            if (array) {
                let data = _.filter(array, (row) => {
                    if (row) {
                        let isExist = false;
                        let filterlength = currentFilter.length;
                        let i = 0;

                        currentFilter.forEach(column => {
                            if (row[column.key] == null) return false;
                            var data = row[column.key].toString().toLowerCase();
                            if (column.value)
                                if (typeof (column.index) !== 'undefined' && column.index === 0) {
                                    if (_.startsWith(data, column.value.toLowerCase())) {
                                        // isExist = true;
                                        i++;
                                    }
                                } else {
                                    if (data.indexOf(column.value.toLowerCase()) > -1) {
                                        // isExist = true;
                                        i++;
                                    }
                                }
                        });
                        if (filterlength == i)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                });
                this.storageService.FilterData = data;
                return data;
            }
        }
        else {
            this.storageService.FilterData = array;
        }


        return array;
    }
}