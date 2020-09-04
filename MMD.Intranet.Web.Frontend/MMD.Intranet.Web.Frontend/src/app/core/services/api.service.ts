// Deprecated ?

import { Inject, Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { StorageService } from './storage.service';
import { UUID } from 'angular2-uuid';
// import { HttpInterceptorService } from 'ng-http-interceptor';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Events } from '../events';

@Injectable()
export class ApiService {

    constructor(@Inject('Constant') public constant,
        @Inject('tenant') public tenant,
        public storage: StorageService,
        // public http: HttpClient,
        // public httpInterceptor: HttpInterceptorService,
        private router: Router,
        private event: Events) {
        this.addInterceptor();

    }

    get(url: string, config?: {}) {

        config = this.getRequestHeader(config);
        return Observable.throw('server error');
        //  return this.http.get(`${this.constant.remote}${url}`, config);
    }
    // getAsJson(url: string, config?: {}, noDataHandler?: any) {
    //     config = this.getRequestHeader(config);
    //     return this.http.get(`${this.constant.remote}${url}`, config).map(res => {
    //         var data = res.json();
    //         if (data.result.result.length) {

    //         }
    //         else {
    //             noDataHandler(data);
    //         }
    //     });
    // }
    // return this.apiService.get(this.apiUrls.partsAndBom.partNarrSearchparams).map(res => {
    //     var data = res.json();

    post(url: string, data?: {}, config?: {}) {
        config = this.getRequestHeader(config);
        // return this.http.post(`${this.constant.remote}${url}`, data, config);
        return Observable.throw('server error');

    }

    put(url: string, data: {}, config?: {}) {
        config = this.getRequestHeader(config);
        //        return this.http.put(`${this.constant.remote}${url}`, data, config);
        return Observable.throw('server error');

    }

    delete(url: string, config?: {}) {
        config = this.getRequestHeader(config);
        // return this.http.delete(`${this.constant.remote}${url}`, config);
        return Observable.throw('server error');

    }


    getRequestHeader(config): Headers {
        if (!config) config = {};
        let headers = new Headers();

        let userData: any = this.storage.getObject('userItem');
        if (userData) {
            headers.append('Authorization', 'Bearer ' + userData.access_token);
        }
        if (config.contentType) {
            headers.set('Content-Type', config.contentType);
        } else {
            headers.set('Content-Type', 'application/json');
        }
        if (config.search) {
            config.search.set('TenantKey', this.tenant.key);
            var data = this.storage.getValue('selectedCompany');
            if (data) {
                config.search.set('SelectedDB', data.key);
            }
            else {
                config.search.set('SelectedDB', '01');
            }
            config.search.set('UniqueRequestId', UUID.UUID().toString());
        }
        else {
            var params = new HttpParams();
            params = params.set('TenantKey', this.tenant.key);
            var data = this.storage.getValue('selectedCompany');
            if (data) {
                config.search.set('SelectedDB', data.key);
            }
            else {
                config.search.set('SelectedDB', '01');
            }
            params = params.set('UniqueRequestId', UUID.UUID().toString());
            config.search = params;
        }

        config.headers = headers;
        return config;
    }

    addInterceptor() {
        // this.httpInterceptor.request().addInterceptor((data, method) => {
        //     // console.log(method, data);
        //     return data;
        // });

        // this.httpInterceptor.response().addInterceptor((observale) => {
        //     return observale.catch(err => {
        //         this.event.publish('loaderShow', false);
        //         if (err) {
        //             console.log(err);
        //             if (err.status != 401)
        //                 return Observable.throw(err);
        //             else {
        //                 this.router.navigate(['/auth/login']);
        //                 return Observable.empty();
        //             }
        //         }
        //         else {
        //             return Observable.throw(err);
        //         }
        //     })
        //     // return res.do(r => {
        //     //     if (r.status == 401) {
        //     //         console.log("UNAUTHORIZE");
        //     //     }
        //     // });

        // });

    }

}
