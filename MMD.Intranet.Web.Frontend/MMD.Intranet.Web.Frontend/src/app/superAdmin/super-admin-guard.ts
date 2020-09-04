import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

//Provider 

import { StorageService } from '../core/services/storage.service';


@Injectable()
export class SuperAdminGuard implements CanActivate {
  constructor(private router: Router, private storage: StorageService) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let userData = this.storage.getObject('userItem');
    if (!userData) {
      this.router.navigate(['/auth/login']);
      return false;
    }
    if (!userData.roles) {
      this.router.navigate(['/auth/login']);
      return false;
    }
    let roles = JSON.parse(userData.roles);
    if (roles.indexOf('superadmin') > -1) {
      // this.router.navigate(['/superAdmin/users']);
      return true;
    } else
      this.router.navigate(['extranet/dashboard']);
    return true;
  }
}
