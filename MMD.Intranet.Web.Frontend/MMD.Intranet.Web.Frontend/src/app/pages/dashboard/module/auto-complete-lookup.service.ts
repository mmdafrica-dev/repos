import { Injectable } from '@angular/core';
import * as moment from 'moment';
import * as _ from "lodash";
@Injectable()
export class AutoCompleteLookUpService {

    constructor() { }

    formatOptionResponse(response) {
        let headers = response.Hearders;
        let resultHeaders = _.map(headers, (item) => {
            let data = item.split('|');
            return {
                name: item,
                namefilter: item + 'filter',
                displayText: data[0],
                width: Number(data[1]),
                type: data[2],
                justification: data[3],
                isDisplay: data[4] == 0 ? false : true,
                isLink: false,
                link: ''
            };
        });
        let dataResult = _.map(response.JsonData[0], (item) => {
            let data = _.pick(item, headers);
            _.each(resultHeaders, (currentItem) => {
                let value = data[currentItem.name];
                let currentData = "";
                if (value && typeof value === 'string') {
                    currentData = value.trim();
                }
                else {
                    currentData = value;
                }

                data[currentItem.name] = this.getFormattedDate(currentItem.type, currentData, false);
                data[currentItem.namefilter] = this.getFormattedDate(currentItem.type, currentData, true);

            });
            return data;
        });


        let autoCompleteData = {
            isShow: false,
            headers: resultHeaders,
            items: dataResult,
            width: _.sum(_.map(resultHeaders, 'width')) + 50
        };

        return autoCompleteData;

    }
    getFormattedDate(type, currentData, isFilter) {
        if (type && currentData) {

            let dataType = type.toLowerCase();

            if (_.startsWith(dataType, 't')) {
                return currentData;
            }
            else if (_.startsWith(dataType, 'n')) {
                if (!currentData) return currentData;
                currentData = ((currentData != '' && currentData != undefined && typeof currentData != 'number') ? currentData.replace(/,/g, '') : currentData);
                return Number(currentData);
            }
            else if (_.startsWith(dataType, 'd')) {
                if (isFilter) {
                    return ((moment(currentData).format('DD MMM YYYY')).toLocaleUpperCase());
                }
                return new Date(currentData);
            }
            else {
                return currentData;
            }
        }
        else {
            return currentData;
        }

    }

    tryTrim(value) {
        if (value && typeof (value) === 'string') {
            return value.trim();
        }
        return value;
    }

}