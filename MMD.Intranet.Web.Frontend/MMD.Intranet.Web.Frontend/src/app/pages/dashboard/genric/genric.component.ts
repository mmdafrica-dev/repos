import { Component, OnInit, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { StorageService } from '../../../core/services/storage.service';
import { SettingService } from '../../../core/services/setting.service';
import { GenericService } from '../services/generic.service';
import { Events } from '../../../core/events';
import * as _ from "lodash";
import { filter } from 'rxjs/operators';
import * as moment from 'moment';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-genric',
  templateUrl: './genric.component.html',
  styleUrls: ['./genric.component.scss']
})
export class GenricComponent implements OnInit, OnDestroy {
  data: any;//= [{ test: 'test', testing: 'testing', testing1: 'testing1', testing2: 'testing1' }, { test: 'testing', testing: 'testing', testing1: 'testing1', testing2: 'testing1' }];
  header: any;// [{ displayText: 'test', name: 'test' }, { displayText: 'testing', name: 'testing' }, { displayText: 'testing1', name: 'testing1' }, { displayText: 'testing2', name: 'testing2' }];
  showResult: boolean = false;
  panel1hearder: any;
  panelhearder: any = [];
  selectedGenricData: any;
  panel: any;
  ispageload: boolean = false;
  currentPage: string;
  title: any;
  key: any;
  currentValue: any;
  isPageLoaded = false;
  functionCalled = false;
  loadError: boolean;
  searchResultHeader = [];
  constructor(
    public storage: StorageService,
    public location: Location,
    public genericService: GenericService,
    private event: Events,
    private setting: SettingService,
    private activatedRoute: ActivatedRoute) {
    let curr = this.location.path().split('/');
    this.currentPage = curr[curr.length - 1];
    this.key = null;

    this.setting.childRender.subscribe(() => {

      if ((this.currentPage == "generic" || this.key) && !this.isPageLoaded) {
        this.getGenericFileData();
      }
    });
    this.activatedRoute.params.subscribe((params: Params) => {
      this.key = params['name'];
      this.currentValue = params['value'];
      if (this.key) {
        this.getGenericFileData();
      }
    });
    if ((this.currentPage == "generic")) {
      this.getGenericFileData();
    }
  }


  ngOnInit() {
    this.data = [];
    let userData = this.storage.getObject('userItem');
    let roles = JSON.parse(userData.roles);
    let isSuperAdmin = false;
    if (roles.indexOf('superadmin') > -1) {
      isSuperAdmin = true;
    }
    if (roles.indexOf('generic') == -1 && !isSuperAdmin) {
      this.goback();
    }
    else {
      // this.getGenericFileData();
    }
  }

  setValueToParams(model, label, params, property) {
    let key = model;
    if (label) {
      key += label;
    }
    let value = this.storage.getValue(key);
    if (value) {
      params[property] = value;
    }
  }

  getParams(filters, params, nonGeneric?) {
    this.searchResultHeader = [];
    filters.forEach(filter => {
      switch (filter.type) {
        case 'date':
          this.setValueToParams('selectedGenricDateFrom', filter.overrideDisplayLabel1, params, 'DateFrom');
          this.searchResultHeader.push({ label: filter.overrideDisplayLabel1 || 'From', value: params['DateFrom'] });
          if (filter.isRangeSelectable) {
            this.setValueToParams('selectedGenricDateTo', filter.overrideDisplayLabel2, params, 'DateTo');
            this.searchResultHeader.push({ label: filter.overrideDisplayLabel2 || 'To', value: params['DateTo'] });
          }
          break;
        case 'text':
          if (nonGeneric) {
            this.setValueToParams('textFromModel', filter.overrideDisplayLabel1, params, 'Searchfor');
            this.searchResultHeader.push({ label: filter.overrideDisplayLabel1 || 'From', value: params['Searchfor'] });
            if (filter.isRangeSelectable) {
              this.setValueToParams('textToModel', filter.overrideDisplayLabel2, params, 'SearchTo');
              this.searchResultHeader.push({ label: filter.overrideDisplayLabel2 || 'To', value: params['SearchTo'] });
            }
          }
          break;
        case 'period':
          params.DateFrom = this.storage.getValue('datepickerPeriodYearFromModel' + (filter.overrideDisplayLabel1 || '')) + this.storage.getValue('periodFrom' + (filter.overrideDisplayLabel1 || ''));
          this.searchResultHeader.push({ label: filter.overrideDisplayLabel1 || 'From', value: this.storage.getValue('datepickerPeriodYearFromModel' + (filter.overrideDisplayLabel1 || '')) + ' - ' + this.storage.getValue('periodFrom' + (filter.overrideDisplayLabel1 || '')) });
          if (filter.isRangeSelectable) {
            params.DateTo = this.storage.getValue('datepickerPeriodYearToModel' + (filter.overrideDisplayLabel2 || '')) + this.storage.getValue('periodTo' + (filter.overrideDisplayLabel2 || ''));
            this.searchResultHeader.push({ label: filter.overrideDisplayLabel2 || 'To', value: this.storage.getValue('datepickerPeriodYearToModel' + (filter.overrideDisplayLabel2 || '')) + ' - ' + this.storage.getValue('periodTo' + (filter.overrideDisplayLabel2 || '')) });
          }
          break;
      }
    });
    return params;
  }

  getNotShowColumns(Icons) {
    let notShowColumns = [];
    if (!Icons) {
      return notShowColumns;
    }
    for (let key in Icons) {
      let fromColumn = Number(Icons[key].FromColumn);
      let keyAsNumber = Number(key);
      if (fromColumn === keyAsNumber) {
        continue;
      }
      let foundColumn = _.find(notShowColumns, fromColumn);
      if (!foundColumn) {
        notShowColumns.push(fromColumn);
      }

    }

    return notShowColumns;
  }

  getGenericFileData() {

    this.selectedGenricData = this.storage.getObject('selectedGenric');

    let data = this.storage.getObject('selectedLinkGenric');
    if (this.selectedGenricData || this.key) {

      this.event.publish('loaderShow', true);
      this.event.publish('loadingText', 'Gathering Data.....');
      this.showResult = false;
      let params = null;

      if (this.key && this.currentPage != "generic") {
        if (data) {
          this.selectedGenricData = data;
        }

        let GenricAllData = this.storage.getGenericData();
        let currentDataResult = _.find(GenricAllData, { name: decodeURI(this.key) });
        if (currentDataResult) {
          this.selectedGenricData = currentDataResult;
        }
        params = {
          SearchType: '',
          Searchfor: (decodeURI(this.currentValue)).replace(new RegExp('%2F', 'g'), '/'),
          SearchTo: '',
          DateFrom: this.storage.getValue('selectedGenricDateFrom'),
          DateTo: this.storage.getValue('selectedGenricDateTo')
        }

        if (this.selectedGenricData && this.selectedGenricData.filters) {
          this.getParams(this.selectedGenricData.filters, params, false);
        }
      }
      else {
        if (this.selectedGenricData && this.selectedGenricData.filters) {
          params = {};
          this.getParams(this.selectedGenricData.filters, params, true);
        }
      }
      this.genericService.getGenericData(this.selectedGenricData.sp, params, this.selectedGenricData.name)
        .subscribe((res: any) => {
          this.event.publish('loadingText', 'Formatting Display .....');
          // this.event.publish('loaderShow', false);
          let hearders;
          let panelhearders = [];
          let dataResult = [];
          if (this.selectedGenricData.uiLayout) {
            if (this.selectedGenricData.uiLayout.panel) {
              hearders = _.take(res.Hearders, this.selectedGenricData.uiLayout.panel.colEnd);
            }
            else {
              hearders = res.Hearders;
            }

            if (this.selectedGenricData.uiLayout && this.selectedGenricData.uiLayout.panelDetails) {
              let currentObject = this.selectedGenricData.uiLayout.panelDetails;
              this.panel = currentObject.panel;
              this.title = currentObject.Title;
              panelhearders = _.slice(res.Hearders, currentObject.colStart, currentObject.colEnd);
            }
            else {
              this.panel = 0;
            }
          }
          else {
            hearders = res.Hearders;
            this.panel = 0;
          }
          var linkCount = 0;
          this.header = _.map(hearders, (item) => {
            let data = item.split('|');
            linkCount += 1;
            //console.log(this.selectedGenricData.Icons ? (this.selectedGenricData.Icons[linkCount]) : '');
            return {
              name: item,
              namefilter: item + 'filter',
              displayText: data[0],
              width: Number(data[1]),
              type: data[2],
              justification: data[3],
              isDisplay: data[4] == 0 ? false : true,
              isLink: this.selectedGenricData.linkedColumns ? (this.selectedGenricData.linkedColumns[linkCount] ? true : false) : false,
              link: this.selectedGenricData.linkedColumns ? (this.selectedGenricData.linkedColumns[linkCount]) : '',
              isIcon: this.selectedGenricData.Icons ? (this.selectedGenricData.Icons[linkCount] ? true : false) : false,
              icon: this.selectedGenricData.Icons ? (this.selectedGenricData.Icons[linkCount]) : ''
            };
          });
          var pannellinkCount = 0;
          if (this.selectedGenricData.uiLayout && this.selectedGenricData.uiLayout.panelDetails) {
            dataResult = _.map(panelhearders, (item) => {
              let data = item.split('|');
              pannellinkCount += 1;
              return {
                name: item,
                displayText: data[0],
                width: Number(data[1]),
                type: data[2], justification: data[3],
                isDisplay: data[4] == 0 ? false : true,
                isLink: this.selectedGenricData.pannelLinkedColumns ? (this.selectedGenricData.pannelLinkedColumns[pannellinkCount] ? true : false) : false,
                link: this.selectedGenricData.pannelLinkedColumns ? (this.selectedGenricData.pannelLinkedColumns[pannellinkCount]) : ''
              };
            });
          }
          let count = 0;
          this.data = _.map(res.JsonData[0], (item) => {
            let data = _.pick(item, hearders);
            _.each(this.header, (currentItem) => {
              let value = data[currentItem.name];
              let currentData = "";
              if (value && typeof value === 'string') {
                if (currentItem.type == 'D') {
                  currentData = value.trim();
                } else {
                  currentData = value.trim();
                }
              }
              else {
                currentData = value;
              }

              data[currentItem.name] = this.getFormattedDate(currentItem.type, currentData, false);
              data[currentItem.namefilter] = this.getFormattedDate(currentItem.type, currentData, true);

            });
            count += 1;
            if (this.selectedGenricData.uiLayout) {
              if (this.selectedGenricData.uiLayout.panelDetails) {
                let panelData = _.pick(item, panelhearders);
                _.each(panelhearders, (currentpanelItem) => {
                  let value = data[currentpanelItem.name];
                  let currentPanelData = "";
                  if (value && typeof value === 'string') {
                    if (currentpanelItem.type == 'D') {
                      currentPanelData = value.trim();
                    } else {
                      currentPanelData = value.trim();
                    }
                  }
                  else {
                    currentPanelData = value;
                  }
                  panelData[currentpanelItem.name] = this.getFormattedDate(currentpanelItem.type, currentPanelData, false);
                });
                data.paneldata = this.mapPropertiesWithDefault(panelhearders, item);// _.pick(item, panelhearders);
              }
            }
            return data;
          });

          this.panelhearder = dataResult;//_.filter(dataResult, { isDisplay: true });
          this.showResult = true;
          this.loadError = false;

        }, (error) => {
          this.showResult = false;
          this.panelhearder = null;
          this.header = null;
          this.data = null;
          this.event.publish('loaderShow', false);
          this.loadError = true;
        });
    }
    else {
      this.goback();
    }
  }


  mapPropertiesWithDefault(properties, obj) {
    var new_obj = {};
    properties.forEach((k) => {
      new_obj[k] = obj[k] || '';
    });
    return new_obj;
  }

  goback() {
    this.location.back();
  }
  ngOnDestroy() {
    this.currentPage = "";
    this.isPageLoaded = true;
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
        // let currdate = currentData.replace('-', ' ')
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
}
