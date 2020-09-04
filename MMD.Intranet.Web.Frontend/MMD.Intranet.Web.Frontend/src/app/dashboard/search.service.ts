import { Injectable, Inject } from '@angular/core';
import { URLSearchParams } from '@angular/http';
import { ApiService } from '../core/services/api.service';


//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class SearchService {

  constructor( @Inject('apiUrls') public apiUrls, public apiService: ApiService) { }

  getPartSearch(searchOptions: any[]): Observable<any> {
    var params = new URLSearchParams();
    searchOptions.forEach((item) => {
      if (item.name === 'InSearch') {
        params.set('Order', item.value);
      } else {
        params.set(item.name, item.value);
      }
    });
    return this.apiService.get(this.apiUrls.partSearch, { search: params }).map((res: any) => {
      let response = res.json();
      return response.result;
    });
  }

}
