import { Injectable, Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { ApiProvider } from '../../../core/services/api.provider';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Events } from '../../../core/events';
import { ToastService, TostType } from '../../../core/services/toast.service';
import { StorageService } from '../../../core/services/storage.service';

@Injectable()
export class PartAndBomService {

  cachedData: any = {};
  cachedPartNumber: any = {};
  partData: any;
  partParamsData: any;
  data: any;
  partList: any;
  constructor(@Inject('apiUrls') private apiUrls,
    public apiService: ApiProvider,
    private tostService: ToastService,
    private event: Events,
    private router: Router,
    public storage: StorageService) {

  }

  search(params, force, partNumber, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partsAndBom.search, isback);
  }

  partNarrSearch(params) {
    return this.apiService.get(this.apiUrls.partsAndBom.partNarrSearch, params).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));

  }

  resetPassword(data, params) {
    if (data.userId)
      return this.apiService.post(this.apiUrls.resetPassword.setPassword, data, params);
    else
      return this.apiService.post(this.apiUrls.resetPassword.changePassword, data, params);
  }

  partSearchDetailOptions(menuKey: string) {
    var params: any = {};//new HttpParams();
    params.menuKey = menuKey;
    return this.apiService.get(this.apiUrls.partsAndBom.partSearchDetailOptions, params).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }
  partDetailTabs() {
    var params: any = {};//new HttpParams();
    params.menuKey = 'partEnqueries';
    params.key = 'partsandbom';
    params.subKey = 'partdetail';
    return this.apiService.get(this.apiUrls.partDetail.getTabs, params).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }
  partDetail(params, force = false) {
    let key = 'partDetail';
    let partData = this.cachedData ? this.cachedData[key] : null;

    if (partData && !force) {
      return this._returnObserables(partData);
    }
    else {
      return this.apiService.get(this.apiUrls.partsAndBom.partDetail, params).pipe().map(res => {
        let partData = res;
        this.cachedData[key] = partData;
        return partData;
      }).catch(this.handleError.bind(this));
    }
  }

  partBrowse(partNUmber: string) {
    var params: any = {};
    params.partNUmber = partNUmber;
    return this.apiService.get(this.apiUrls.partsAndBom.partBrowse, params).pipe().map(res => {
      return res;
    }).catch(this.handleError.bind(this));
  }
  partNarrDeatail(params, force = false, partNumber, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partsAndBom.partNarrSearch, isback);
  }
  bomMultiLevel(params, force = false, partNumber, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partsAndBom.bomMultiLevel, isback);
  }
  partBomDetail(params, force = false, key: string, partNumber: any, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partDetail.partBomDetail, isback);
  }

  WhereUsedDetail(params, force = false, key: string, partNumber: any, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partDetail.whereUsedDetail, isback);
  }

  partBomOpertaionDetail(params, force, partNumber, isback) {
    return this.getPartDetailData(params, force, partNumber, this.apiUrls.partDetail.bomOperationDetail, isback);
  }

  getPartDetailData(params, force, partNumber, api, isback) {
    let partData = this.cachedData ? this.cachedData[api] : null;
    if (partData && !force && this.cachedPartNumber[api] == partNumber) {
      return this._returnObserables(partData);
    }
    else {
      return this.apiService.get(api, params).pipe().map(res => {
        this.data = res;
        this.cachedPartNumber[api] = partNumber;
        this.cachedData[api] = this.data;
        if (this.data.result.length > 0) {
          return this.data;
        }
        else {
          if (isback) {
            this.sendBackToSearchPage(params);
            // if (params.get('searchfor'))
            //   localStorage.setItem('searchfor', params.get('searchfor'));
            // this.event.publish('loaderShow', false);
            // localStorage.setItem('currentType', 'PARTLIST');
            //this.router.navigate(['/pages/dashboard/']);
          }
          return this.data;
        }
      }).catch(error => {
        return this.handleError.bind(error, isback, params)
      });
    }
  }


  partAllocation(params, force = false) {
    let key = 'partAllocation';
    let partData = this.cachedData ? this.cachedData[key] : null;
    if (partData && !force) {
      return this._returnObserables(partData);
    }
    else {
      return this.apiService.get(this.apiUrls.partDetail.partAllocation, params).map(res => {
        return res;
      }).catch(error => {
        return this.handleError.bind(error, force, params)
      });
    }
  }

  setPartData(data) {
    this.partData = data;
  }

  getPartData() {
    return this.partData;
  }

  setPartParamsData(data) {
    this.partParamsData = data;
  }
  getPartParamsData() {
    return this.partParamsData;
  }


  handleError(error: any, isback, params) {

    if (error) {
      console.log(error);

      if (error.status != 401) {
        if (isback) {
          this.sendBackToSearchPage(params);

        }
        return Observable.empty(error);
      }
      else {
        //this.router.navigate(['/auth/login']);
        Observable.throw(error);
      }
    }
    else {
      return Observable.throw(error);
    }
    //   console.log('server error:', error);
    //   if (error instanceof Response) {
    //     let errMessage = '';
    //     try {
    //       // errMessage = error.e;
    //     } catch (err) {
    //       errMessage = error.statusText;
    //     }
    //     this._errorAlert(errMessage);
    //     return Observable.throw(errMessage);
    //   }
    //   this._errorAlert(error);
    //   return Observable.throw(error || 'server error');
    // }
    // _errorAlert(message) {
    //   this.tostService.tostMessage(TostType.error, "Error", message);
  }

  sendBackToSearchPage(params) {
    localStorage.setItem('searchfor', params.searchfor);
    this.event.publish('loaderShow', false);
    localStorage.setItem('currentType', 'PARTLIST');
    this.router.navigate(['/extranet/dashboard/']);
  }

  _returnObserables(data: any) {
    return Observable.create((observer) => {
      observer.next(data);
      observer.complete();
    });

  }

}
