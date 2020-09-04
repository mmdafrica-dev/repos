import { Component, OnInit, Inject, ViewChild } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PlatformLocation } from '@angular/common'
import { SearchService } from '../services/search-service';
import { HttpParams } from '@angular/common/http';
import { DataFilterPipe } from '../../../shared/pipe/searchPipe';
import { Events } from '../../../core/events';
import { ExcelService } from '../../../core/services/excel.service';
import { SettingService } from '../../../core/services/setting.service';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import { ToastService, TostType } from '../../../core/services/toast.service';
import { StorageService } from '../../../core/services/storage.service';
import { Paginator, DataTable } from 'angular2-datatable';

import * as _ from "lodash";
import * as moment from 'moment';
import { DataPanelFilterPipe } from 'app/shared/pipe/searchPanelPipe';

declare var localStorage;
@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  providers: [DataFilterPipe, DataPanelFilterPipe]
})
export class SearchComponent implements OnInit {
  // searchForm: FormGroup;
  searchOptions: any;
  mainMenu: string;
  subMenu: string;
  isFormValid = false;

  header = [];
  filterQuery = "";
  rowsOnPage = 50;
  sortBy = "parT1_PART";
  sortOrder = "asc";
  filterBycolumn = "parT1_PART";
  data = [];
  apiLink: string;
  dataTable = false;
  currentFilter = [];
  excelData: any;
  autoCompleteValue: any;
  previousPage = 0;
  search = {
    searchfor: { searchfor: '', required: false },
    numberrange: { from: 0, to: 0, required: false },
    date: { date: '', required: false },
    daterange: { from: '', to: '', required: false },
    textrange: { from: '', to: '', required: false },
    selection: { selection: '', required: false }
  };
  currentIndex: any;

  isPartNarritive: boolean;
  childheader: any;
  childdata: any;
  childResult: any;
  isback = false;
  isFxRate: boolean = false;
  @ViewChild('mf') private mf: DataTable;
  constructor(
    @Inject('apiUrls') private apiUrls,
    @Inject('rowCount') private rowCount, private searchService: SearchService,
    private fb: FormBuilder,
    public storage: StorageService,
    private activatedRoute: ActivatedRoute,
    private event: Events,
    private excelService: ExcelService,
    private setting: SettingService,
    private partAndBomService: PartAndBomService,
    private router: Router,
    private toast: ToastService,
    private location: PlatformLocation
  ) {
    this.rowsOnPage = this.rowCount.rowCount;
    this.apiLink = 'PartsAndBom/SearchPart';
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.mainMenu = params['key'];
      this.subMenu = params['subMenuKey'];
      this.storage.removeItem('currentTab');

      if (!this.storage.getObject('searchOptionsData')) {
        if (this.mainMenu)
          this.getsubroute(this.mainMenu, this.subMenu);

      }
      else {
        this.searchOptions = this.storage.getObject('searchOptionsData');
        this.fillTable(this.partAndBomService.getPartData(), false);
        this.isPartNarritive = this.storage.getObject('isPartNarritive');

      }
    });
  }

  ngAfterViewInit() {
    this.mf.onPageChange.subscribe((x) => {
      if (x.activePage != this.previousPage) {
        this.currentIndex = null;
        this.previousPage = x.activePage;
      }
    });
  }

  getsubroute(key, subMenuKey) {
    this.event.publish('loaderShow', true);
    // if (this.storage.getValue('isFxrate') == 'true') {
    //   subMenuKey = 'fxrateSearch';
    //   this.isFxRate = true;
    // }
    this.searchService.getSearchOptions(key, subMenuKey, 'date').subscribe(result => {
      this.event.publish('loaderShow', false);
      if (this.storage.getObject('setPartData')) {
        this.autoCompleteValue = this.storage.getObject('setPartParamsData');// this.partAndBomService.getPartParamsData();
        this.fillTable(this.partAndBomService.getPartData(), false);//  this.partAndBomService.getPartData());
        this.event.publish('loaderShow', false);
      }
      this.storage.setObject('searchOptionsData', result);
      this.searchOptions = result;

    }, error => {

    })
  }


  handleFormSubmit(params) {
    this.header = [];
    this.data = [];
    this.event.publish('loaderShow', true);
    // if (this.storage.getValue('isFxrate') == 'true') {
    //   this.isPartNarritive = false;
    //   this.isFxRate = true;
    //   params.DateFrom = moment().format("DD-MMM-YYYY");;
    //   this.partAndBomService.getPartDetailData(params, true, params, this.apiUrls.stock.getFxRates, false).subscribe(res => {
    //     this.fillTable(res, true);
    //     this.event.publish('loaderShow', false);
    //   });
    // }
    // else {
    if (params.searchBy == 'PARTNARRSEARCH') {
      this.isPartNarritive = true;
    }
    else {
      if (localStorage.currentType == 'PARTNARRSEARCH') {
        this.isPartNarritive = true;
      }
      else {
        this.isPartNarritive = false;
      }
    }
    this.storage.setObject('isPartNarritive', this.isPartNarritive);

    if (params.searchFor) {
      this.searchService.getSearch(this.apiLink, params)
        .subscribe(res => {
          this.event.publish('loaderShow', false);
          this.fillTable(res, true);
          this.partAndBomService.setPartData(res);
        });
    }
    else {
      this.event.publish('loaderShow', false);
    }
    // }
  }

  fillTable(res, val) {
    this.dataTable = true;
    if (res) {
      this.header = res.headers;
      this.data = res.result;
      if (val) {
        if (res.result.length == 0)
          this.toast.tostMessage(TostType.info, 'Record', 'No record Found');
        else if (res.result.length == 1)
          this.router.navigate(['/extranet/partsearchdetail/' + res.result[0].parT1_PART + '/partDetail']);
      }
    }
  }
  exportToExcel() {
    var listData = _.map(this.data, (item) => {
      var returnObj = {};
      for (let i = 0; i < this.header.length; i++) {
        if (item.hasOwnProperty(this.header[i].name)) {
          returnObj[this.header[i].displayText] = item[this.header[i].name];
        }
      }
      return returnObj;
    });
    this.excelService.exportAsExcelFile(listData, 'searchExportFile', false, null);
  }
  headerChange(f) {
    this.currentIndex = null;
    this.filterQuery = f;
    this.checkFilter();
  }

  clear(val) {
    this.currentIndex = null;
    // if (this.currentFilter.length == 1) {
    //   this.filterQuery = val.text;
    //   val.text = "";
    //   this.filterQuery = "";
    //   this.checkFilter();
    // }
    // else {
    //   val.text = "";
    //   this.checkFilter();
    // }
    val.text = "";
    this.filterQuery = "";
    this.checkFilter();
  }

  sortClicked() {
    this.currentIndex = null;
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

  onClickRow(item, index) {
    if (this.currentIndex == index) {
      this.currentIndex = null;
    }
    else if (!this.currentIndex) {
      this.currentIndex = index;
      this.getNaritiveDate(item.parT1_PART);
    }
    else {
      this.currentIndex = index;
      this.getNaritiveDate(item.parT1_PART)
    };
  }

  getNaritiveDate(id) {
    var params: any = {};
    this.childheader = [];
    this.childdata = [];
    params.Searchfor = id;
    params.SearchBy = 'PARTNARRDETAIL';
    this.searchService.getSearch('PartsAndBom/PartNarrSearch', params)
      .subscribe(res => {
        this.childResult = res;
        this.childheader = this.childResult.headers;
        this.childdata = this.childResult.result;
      });
  }

}
