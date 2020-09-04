import { Inject, Injectable } from '@angular/core';
import { RequestOptions, Headers } from '@angular/http';
import {
    HttpRequest, HttpHandler, HttpEvent, HttpResponse,
    HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpInterceptor
} from '@angular/common/http';
// import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StorageService } from './storage.service';
import { UUID } from 'angular2-uuid';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Events } from '../events';
// import { CustomHttpInterceptor } from './custom-http-interceptor.service';
@Injectable()
export class ApiProvider {
    constructor(@Inject('Constant') public constant,
        @Inject('tenant') public tenant,
        public storage: StorageService,
        public http: HttpClient,
        private router: Router,
        private event: Events) {

    }

    getRemote(overrrideRemote) {
        let selectedCompany = this.storage.getValue('selectedCompany');
        if (overrrideRemote && selectedCompany && selectedCompany.onTrigger && selectedCompany.onTrigger.remote) {
            return selectedCompany.onTrigger.remote;
        }
        return this.constant.remote;
    }

    get(url: string, params?: {}, overrrideRemote = true) {
        let config: any = this.getRequestHeader(params);
        return this.http.get(`${this.getRemote(overrrideRemote)}${url}`, config);
    }

    post(url: string, data?: {}, params?: {}, overrrideRemote = true) {
        let config: any = this.getRequestHeader(params);
        return this.http.post(`${this.getRemote(overrrideRemote)}${url}`, data, config);
    }

    put(url: string, data: {}, params?: {}, overrrideRemote = true) {
        let config: any = this.getRequestHeader(params);
        return this.http.put(`${this.getRemote(overrrideRemote)}${url}`, data, config);
    }

    delete(url: string, params?: {}, overrrideRemote = true) {
        let config: any = this.getRequestHeader(params);
        return this.http.delete(`${this.getRemote(overrrideRemote)}${url}`, config);
    }


    getRequestHeader(params): HttpHeaders {
        if (!params) params = {};
        let headers = new HttpHeaders();
        let userData: any = this.storage.getObject('userItem');
        if (userData) {
            headers = headers.set('Authorization', 'Bearer ' + userData.access_token);
        }

        if (params.contentType) {
            headers = headers.set('Content-Type', params.contentType);
        } else {
            headers = headers.set('Content-Type', 'application/json');
        }

        var httpParams = new HttpParams();
        httpParams = this.getHttpParams(httpParams, params);
        httpParams = this.createHttpParams(httpParams, 'TenantKey', this.tenant.key);

        var data = this.storage.getValue('selectedCompany');
        if (data) {
            httpParams = this.createHttpParams(httpParams, 'SelectedDB', data.key);
        }
        else {
            httpParams = this.createHttpParams(httpParams, 'SelectedDB', '01');
        }
        httpParams = this.createHttpParams(httpParams, 'UniqueRequestId', UUID.UUID().toString());



        let config: any = { headers: headers, params: httpParams };
        return config;
    }

    getHttpParams(httpParams, params) {

        for (var key in params) {
            if (params.hasOwnProperty(key)) {
                if (key != 'contentType')
                    httpParams = this.createHttpParams(httpParams, key, params[key]);
            }
        }
        return httpParams;
    }

    createHttpParams(params: HttpParams, key, value) {
        return params.set(key, value);
    }

}