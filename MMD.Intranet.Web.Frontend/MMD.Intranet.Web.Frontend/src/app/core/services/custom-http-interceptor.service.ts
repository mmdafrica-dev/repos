import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import {
    HttpRequest, HttpHandler, HttpEvent, HttpResponse,
    HttpErrorResponse, HttpInterceptor
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Events } from '../events';
import { StorageService } from './storage.service';
@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {
    constructor(private router: Router,
        private event: Events,
        private storage: StorageService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).do((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
                // do stuff with response if you want
            }
        }, (err: any) => {
            this.event.publish('loaderShow', false);
            if (err instanceof HttpErrorResponse) {
                if (err.status === 401) {
                    this.storage.removeItem('userItem');
                    this.storage.removeItem('userName');
                    this.storage.removeGenericData();
                    this.router.navigate(['/auth/login']);
                }
            }
        });
    }

}