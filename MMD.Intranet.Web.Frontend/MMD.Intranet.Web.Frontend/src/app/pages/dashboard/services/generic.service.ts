
import { Injectable, Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { ApiProvider } from '../../../core/services/api.provider';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { ToastService, TostType } from '../../../core/services/toast.service';
import { StorageService } from '../../../core/services/storage.service';
import { UserService } from '../../..//superAdmin/services/user.service';

import * as _ from 'lodash';
import { Events } from '../../../core/events';

@Injectable()
export class GenericService {
    cachedData: any = {};
    constructor(@Inject('apiUrls') private apiUrls,
        public apiService: ApiProvider, private storageService: StorageService,
        private userService: UserService, private events: Events,
        private tostService: ToastService) {

        this.events.subscribe('auth:logined', (res) => {
            let roles = JSON.parse(res.roles);
            let superAdmin = false;
            if (roles.indexOf('superadmin') > -1) {
                superAdmin = true;
            } else {
                superAdmin = false;
            }

            this.getGenericDropDownFormatData(superAdmin, true);
        });


    }
    getGenericFile() {
        return this.apiService.get(this.apiUrls.generic.genericfile, null, false).pipe().map(res => {
            return res;
        }).catch(this.handleError.bind(this));

    }

    getGenericData(spname, params, spDisplayName = '') {
        let strToContact = '?spname=' + spname;
        if (spDisplayName) {
            strToContact += `&spDisplayName=${spDisplayName}`;
        }
        return this.apiService.get(this.apiUrls.generic.genericData + strToContact, params).pipe().map(res => {
            return res;
        }).catch(this.handleError.bind(this));

    }

    getGenericDropDownData(spname, params) {
        return this.apiService.get(this.apiUrls.generic.genericDropDownData + '?spname=' + spname, params)
            .pipe().map(res => {
                return res;
            }).catch(this.handleError.bind(this));

    }

    getGenericFileData(claims, isSuperAdmin, forceRefresh, resolve, reject) {
        let genericList = [];
        this.getGenericFile().subscribe((res: any) => {
            let data = JSON.parse(res).result;
            if (!isSuperAdmin) {
                let defaultAccessAllUsers = _.filter(data, { defaultAccessAllUsers: true });
                let defaultAccessNotAllUsers = _.filter(data, { defaultAccessAllUsers: false });
                let filterResult = _.filter(defaultAccessNotAllUsers, (item) => {
                    var result = _.find(item.claim, (claim) => {
                        if (claims && claims.indexOf(claim) > -1) {
                            return true;
                        }
                        else
                            return false;
                    });
                    if (result || !item.isActive)
                        return true;
                    else
                        return false;
                });
                let condata = _.concat(filterResult, defaultAccessAllUsers);
                genericList = condata;
            }
            else {
                genericList = data;
            }
            genericList = _.orderBy(genericList, 'name', 'asc');

            this.storageService.setGenericData(genericList);

            genericList = _.filter(genericList, { isActive: true });
            if (forceRefresh) {
                window.location.reload();
            }
            return resolve(genericList);
        }, error => {
            if (forceRefresh) {
                this.events.publish('loaderShow', false);
            }
            return reject(error);
        });
    }

    getGenericDropDownFormatData(isSuperAdmin, forceRefresh = false) {
        return new Promise((resolve, reject) => {

            let genericData: any = this.storageService.getGenericData();
            if (genericData && !forceRefresh) {
                genericData = _.filter(genericData, { isActive: true });
                return resolve(genericData);
            } else {
                if (forceRefresh) {
                    this.events.publish('loaderShow', true);
                }
                this.userService.getUser(this.storageService.getObject('userItem').userId)
                    .subscribe((res: any) => {
                        if (res.claims) {
                            let claims = _.filter(res.claims, (item) => {
                                if (item.value.indexOf('generic') !== -1) {
                                    return item.value;
                                }
                            });
                            claims = _.map(claims, (item) => {
                                return item.value;
                            });
                            return this.getGenericFileData(claims, isSuperAdmin, forceRefresh, resolve, reject);
                        } else {
                            return this.getGenericFileData([], isSuperAdmin, forceRefresh, resolve, reject);
                        }
                    }, error => {
                        return this.getGenericFileData([], isSuperAdmin, forceRefresh, resolve, reject);
                    });
            }
        });
    }


    handleError(error: any) {
        console.error('server error:', error);
        if (error instanceof Response) {
            let errMessage = '';
            try {
                // errMessage = error.json().error;
            } catch (err) {
                errMessage = error.statusText;
            }
            this._errorAlert(errMessage);
            return Observable.throw(errMessage);
        }
        this._errorAlert(error);
        return Observable.throw(error || 'server error');
    }
    _errorAlert(message) {
        if (message && message.error && message.error.Message)
            this.tostService.tostMessage(TostType.error, "Error", message.error.Message);
        else
            this.tostService.tostMessage(TostType.error, "Error", message);
    }

    _returnObserables(data: any) {
        return Observable.create((observer) => {
            observer.next(data);
            observer.complete();
        });
    }

}
