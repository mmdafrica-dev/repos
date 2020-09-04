import { Injectable } from '@angular/core';
import { Router, ActivatedRoute, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

//Provider 
import { StorageService } from '../core/services/storage.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private storage: StorageService) {
  }

  canActivate(

    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let userData = this.storage.getObject('userItem');
    if (!userData) {
      this.navigateToLogin(state);
      return true;
    }
    if (!userData.roles) {
      this.navigateToLogin(state);
      return true;
    }
    let roles = JSON.parse(userData.roles);
    if (roles.indexOf('superadmin') > -1) {
      this.router.navigate(['/superAdmin/users']);
      return false;
    } else
      this.router.navigate(['extranet/dashboard']);
    return true;
  }


  navigateToLogin(state) {
    if (state.url != '/auth/login') {
      this.router.navigate(['/auth/login']);
    }
  }
}
