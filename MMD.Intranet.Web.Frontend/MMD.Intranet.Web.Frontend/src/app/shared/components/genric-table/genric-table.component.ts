import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter, ViewChild, Inject } from '@angular/core';
import { Paginator, DataTable } from 'angular2-datatable';
import { Location } from '@angular/common';
import * as _ from "lodash";
import { ExcelService } from '../../../core/services/excel.service';
import { StorageService } from '../../../core/services/storage.service';
import * as moment from 'moment';
import { Router } from '@angular/router';
import { Events } from '../../../core/events';
import { BsModalService } from 'ngx-bootstrap';
import { AdditionalFilterModalComponent } from './additional-filter-modal/additional-filter-modal';

@Component({
  selector: 'mmd-genric-table',
  templateUrl: './genric-table.component.html',
  styleUrls: ['./genric-table.component.scss']
})
export class GenricTableComponent implements OnInit, AfterViewInit {
  @Input() data: any = [];
  @Input() header: any = [];
  @Input() panel: any;
  @Input() panelhearder: any;
  @Input() title: any;
  @Input() links: any;
  @Output() valueChange = new EventEmitter();
  @Input() selectedGenricData: any;
  @Input() currentPage: any;
  @Input() key: any;
  @Input() searchResultHeader: any = [];

  rowsOnPage = 50;
  rowSet = [];
  showTemplate: boolean = false;
  currentIndex;
  currentFilter = [];
  currentPanelFilter = [];
  filterQuery = "";
  sortBy = "";
  sortOrder = "asc";
  filterBycolumn = "";
  previousPage = 0;
  activePage: any;
  colspan: any;
  hearderList: any;
  widthofheader: any;
  innerWidth: any;
  panelTableWidth: any;
  tableWidth: any;
  customHerderWidth: any;
  widthForPanel: any;

  @ViewChild('mf') private mf: DataTable;
  constructor(
    @Inject('Constant') public constant,
    private excelService: ExcelService,
    public storageService: StorageService,
    public storage: StorageService,
    public location: Location,
    private router: Router,
    private modalService: BsModalService,
    private event: Events) {
    this.activePage = Number(this.storageService.getValue('currentPage'));

  }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    if (this.header && this.header.length > 0) {
      this.filterBycolumn = this.header[0].name;
      //, this.data.length
      this.rowSet = [50, 100, 200];
      // if (this.selectedGenricData && this.selectedGenricData.autoexpand) {
      //   this.currentIndex = 0;
      // }
    }

    if (this.panelhearder) {
      this.colspan = Math.ceil((this.panelhearder.length / this.panel));
      this.tableWidth = 33 * this.panel;
      this.widthForPanel = (this.innerWidth - 55);
      this.hearderList = _.chunk(this.panelhearder, this.colspan);// Math.floor((this.header.length / this.pannel))
    }
    let rowset = this.storageService.getValue('rowsOnPageGenric');
    if (rowset) {
      this.rowsOnPage = rowset;
    }
    // this.doubleScroll(document.getElementById('table-responsive'));
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.event.publish('loaderShow', false);
    }, 500);
    this.mf.onPageChange.subscribe((x) => {
      if (x.activePage != this.previousPage) {
        this.currentIndex = null;
        this.previousPage = x.activePage;
        this.storageService.setValue('currentPage', x.activePage.toString());
      }

      this.storageService.setValue('rowsOnPageGenric', x.rowsOnPage.toString());
    });


    if (this.key) {
      this.currentFilter = this.storageService.getGennyObject(encodeURI(window.location.href));
      this.currentPanelFilter = this.storageService.getGennyObject(encodeURI(window.location.href) + '_PanelData') || [];
    } else {
      this.currentFilter = this.storageService.getGennyObject(this.selectedGenricData.name);
      this.currentPanelFilter = this.storageService.getGennyObject(this.selectedGenricData.name + '_PanelData') || [];
    }
    if (!this.currentFilter) {
      this.currentFilter = [];
    }
    else {
      this.header.forEach(record => {
        let isValueExist = true;
        this.currentFilter.forEach(filter => {
          if (filter.key == record.namefilter) {
            if (filter.value) {
              record.text = filter.value;
              isValueExist = false;
            }
          }
        });
      });
    }
    // this.mf.setPage(this.activePage, this.rowsOnPage);
  }

  doubleScroll(element) {
    let scrollbar: any = document.createElement('div');
    scrollbar.appendChild(document.createElement('div'));
    scrollbar.style.overflow = 'auto';
    scrollbar.style.overflowY = 'hidden';
    scrollbar.style.width = '100% !imporatnt';
    scrollbar.firstChild.style.width = element.scrollWidth - 30 + 'px';
    scrollbar.firstChild.style.paddingTop = '1px';
    scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
    scrollbar.onscroll = () => {
      element.scrollLeft = scrollbar.scrollLeft;
    };
    element.onscroll = () => {
      scrollbar.scrollLeft = element.scrollLeft;
    };
    element.parentNode.insertBefore(scrollbar, element);
  }


  onClickRow(item, index) {
    if (this.currentIndex == index) {
      this.currentIndex = null;
    }
    else if (!this.currentIndex) {
      this.currentIndex = index;
    }
    else {
      this.currentIndex = index;
    };
  }

  headerChange(f) {
    this.currentIndex = null;
    this.filterQuery = f;
    this.checkFilter();
  }

  clear(val) {
    this.currentIndex = null;
    val.text = "";
    this.filterQuery = "";
    this.checkFilter();
  }

  checkFilter() {
    this.header.forEach(record => {
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
    if (this.key)
      this.storageService.setGennyObject(encodeURI(window.location.href), this.currentFilter);
    else
      this.storageService.setGennyObject(this.selectedGenricData.name, this.currentFilter);


  }

  clearFilter() {
    _.each(this.header, (item) => {
      item.text = '';
    });
    this.checkFilter();

    this.currentPanelFilter = [];

    _.each(this.hearderList, (headerItem) => {
      _.each(headerItem, (item) => {
        item.text = '';
      });
    });
    this.updatePanelFilter();
  }

  sortClicked() {
    this.currentIndex = null;
  }

  additionalFilter(filter) {
    const modalRef = this.modalService.show(AdditionalFilterModalComponent, { class: 'modal-full' });
    modalRef.content.additionalFilter = {
      isShow: true,
      hearderList: this.hearderList,
      headers: this.panelhearder,
      panelsFilters: this.currentPanelFilter
    };

    const subscribeModelHidden = this.modalService.onHidden.subscribe(res => {
      if (subscribeModelHidden) {
        subscribeModelHidden.unsubscribe();
      }
      if (modalRef.content.setValueParent) {
        this.currentPanelFilter = modalRef.content.setValueParent;
        this.updatePanelFilter();
      }
    });
  }


  updatePanelFilter() {
    if (this.key) {
      this.storageService.setGennyObject(encodeURI(window.location.href) + '_PanelData', this.currentPanelFilter);
    } else {
      this.storageService.setGennyObject(this.selectedGenricData.name + '_PanelData', this.currentPanelFilter);
    }
  }

  exportToExcel() {
    let data = this.storageService.FilterData;
    if (!data) {
      data = this.data;
    }
    let href = window.location.href.split('#')[0];

    let returnExcelArray = [];
    let returnExcelHyperArray = [];
    _.each(data, (item) => {
      let returnObj = {};
      let returnRObj = [];
      this.formatPanelHeader(item, this.header, returnObj, returnRObj, href);
      if (item.paneldata) {
        this.formatPanelHeader(item.paneldata, this.panelhearder, returnObj, returnRObj, href);
      }
      returnExcelHyperArray.push(returnRObj);
      returnExcelArray.push(returnObj);
    });

    if (this.sortBy) {
      let sort = this.sortBy.split('|');
      returnExcelArray = _.orderBy(returnExcelArray, [sort[0]], [this.sortOrder]);
      returnExcelHyperArray = _.orderBy(returnExcelHyperArray, [sort[0]], [this.sortOrder]);
    }
    let selectedSP = this.storageService.getObject('selectedGenric')
    this.excelService.exportAsExcelFile(returnExcelArray, selectedSP.name, true, returnExcelHyperArray);
  }


  formatPanelHeader(item, headers, returnObj, returnRObj, href) {
    for (let i = 0; i < headers.length; i++) {
      if (!headers[i].isDisplay) {
        continue;
      }
      if (item.hasOwnProperty(headers[i].name)) {
        if (headers[i].isLink) {
          if (item[headers[i].name] instanceof Date) {
            returnObj[headers[i].displayText] = (moment(item[headers[i].name]).format('DD-MMM-YYYY')).toLocaleUpperCase();
          }
          else {
            returnObj[headers[i].displayText] = item[headers[i].name];
          }
          let link = this.getSearchParams(headers[i].link, item)
          link = link.replace(new RegExp('/', 'g'), '%2F');
          returnRObj.push(href + encodeURI('#/extranet/genericDetail/' + headers[i].link.name + '/' + link));
        }
        else {
          if (item[headers[i].name] instanceof Date) {
            returnObj[headers[i].displayText] = item[headers[i].name];
            ///returnObj[headers[i].displayText] = (moment(item[headers[i].name]).format('DD-MMM-YYYY')).toLocaleUpperCase();
            // returnRObj.push((moment(item[headers[i].name]).format('DD-MMM-YYYY')).toLocaleUpperCase());
            returnRObj.push(item[headers[i].name]);
          }
          else {
            returnObj[headers[i].displayText] = item[headers[i].name];
            returnRObj.push(item[headers[i].name]);
          }
        }
      }
    }
  }

  getWidth(width) {
    return (((this.innerWidth - 120) * width / 100) - 15);
  }

  linkDetail(linkdata, linkValue, hearder, value, curentValue) {
    let link = Object.assign({}, linkdata);

    let SearchFrom = this.getSearchParams(link, value);
    if (typeof SearchFrom === 'string') {
      link.Searchfor = SearchFrom.trim();
    } else {
      link.Searchfor = SearchFrom;
    }

    this.storageService.setObject('selectedLinkGenric', link);
    this.router.navigate(['/extranet/genericDetail', link.name, encodeURI(link.Searchfor)]);
  }

  getSearchParams(link, value) {
    let SearchFrom = '';
    _.each(link.searchForParameters, (item) => {
      if (item.valueType == 'ColHeading') {
        SearchFrom = SearchFrom + '|' + this.header[item.colIndex - 1].displayText;
      }
      else {
        SearchFrom = SearchFrom + '|' + value[this.header[item.colIndex - 1].name];
      }
    });

    return SearchFrom.substr(1);
  }



}
