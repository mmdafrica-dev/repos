import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

//Provider 

import { StorageService } from '../core/services/storage.service';


@Injectable()
export class SuperAdminGuard implements CanActivate {


  constructor(private storage: StorageService) {

  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let userData = this.storage.getObject('userItem');
    if (!userData) {
      return false;
    }
    if (!userData.roles) {
      return false;
    }
    let roles = JSON.parse(userData.roles);
    if (roles.indexOf('superadmin') < 0) {
      return false;
    } else
      return true;


  }
}
