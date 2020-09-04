import { Inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Response } from '@angular/http';


//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { IMenu } from '../../shared/interfaces/IMenu';

@Injectable()
export class NavService {

    constructor( @Inject('apiUrls') public apiUrls, public apiService: ApiService) {
    }

    getMenu(): Observable<IMenu[]> {
        return this.apiService.get(this.apiUrls.getHome, {}).map((res: Response) => {
            let response = res.json();
            return response.result;
        }).catch(this.handleError);
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