import { Injectable, Inject } from '@angular/core';
import { Response } from '@angular/http';

/// Services 
import { ApiService } from '../core/services/api.service';
// Interfaces
import { IHome, IHomeItem } from '../shared/interfaces/IHome';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

//THIRD PARTY
import * as _ from 'lodash';

@Injectable()
export class DashboardService {
  dataStorage: IHome[] = [];
  constructor( @Inject('apiUrls') public apiUrls, public apiService: ApiService) {
  }

  getHomeItem(): Observable<IHome[]> {
    if (this.dataStorage.length > 0) {
      return Observable.create(observer => {
        observer.next(this.dataStorage);
        observer.complete();
      });
    } else {
      return this.apiService.get(this.apiUrls.getHome, {}).map((res: Response) => {
        let response = res.json();
        this.dataStorage = response.result;
        return response.result;
      }).catch(this.handleError);
    }
  }

  getSelectedItem(key: string): Observable<IHome> {
    return Observable.create(observer => {
      this.getHomeItem().subscribe((res: IHome[]) => {
        let selectedItem = _.find(res, { menuKey: key });
        observer.next(selectedItem);
        observer.complete();
      });
    });
  }
  getSelectedSearchItem(selectedkey: string, key: string): Observable<IHomeItem> {
    return Observable.create(observer => {
      this.getSelectedItem(selectedkey).subscribe((res: IHome) => {
        let selectedItem = _.find(res.items, { menuKey: key });
        observer.next(selectedItem);
        observer.complete();
      });
    });
  }

  handleError(error: any) {
    console.error('server error:', error);
    if (error instanceof Response) {
      let errMessage = '';
      try {
        errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      return Observable.throw(errMessage);
    }
    return Observable.throw(error || 'server error');
  }

}
