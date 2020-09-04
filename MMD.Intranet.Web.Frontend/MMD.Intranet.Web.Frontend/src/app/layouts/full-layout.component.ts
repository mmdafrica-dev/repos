import { Component, OnInit, Inject, ElementRef, Renderer, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DOCUMENT } from '@angular/platform-browser';
import { StorageService } from '../core/services/storage.service';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';
import { ApiProvider } from '../core/services/api.provider';
import { SettingService } from '../core/services/setting.service';
import { Events } from '../core/events';
import { Observable } from 'rxjs/Observable';
import { Location } from '@angular/common';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-dashboard',
  templateUrl: './full-layout.component.html',
  styleUrls: ['./full-layout.component.scss']
})
export class FullLayoutComponent implements OnInit {
  adminMenuItems = [];
  menuItems = [];
  searchOptions: any;
  searchOptionsData: any;
  public disabled = false;
  userName: string;
  selectedColorValue: any;
  selectedColor: string = "red";
  loading: boolean = false;
  applicationVersion: string;
  public status: { isopen: boolean } = { isopen: false };
  currentPage: string = '';
  isGuestUser: boolean = false;
  footerlinks: any;
  constructor(@Inject('apiUrls') private apiUrls,
    @Inject(DOCUMENT) private document: Document,
    @Inject('tenant') public tenant,
    @Inject('appVersion') public appVersion,
    private route: ActivatedRoute,
    public storage: StorageService,
    private router: Router,
    private authService: AuthService,
    public apiService: ApiProvider,
    public el: ElementRef,
    private _renderer: Renderer,
    public setting: SettingService,
    private event: Events,
    private cdr: ChangeDetectorRef,
    public location: Location,
    private spinnerService: NgxSpinnerService) {

    this.setting.childRender.subscribe(() => {
      this.changeColor();

    });

    event.subscribe('loaderShow', (value) => {
      //  console.log(value);
      this.loading = value;
      if (value) {
        this.spinnerService.show();
      }
      else {
        this.event.publish('initLoadingText');
        this.spinnerService.hide();
      }
      this.changeColor();
      // this.cdr.detectChanges();
    });
    this.applicationVersion = this.appVersion.version;

  }

  public toggled(open: boolean): void {
    // console.log('Dropdown is now: ', open);
  }

  public toggleDropdown($event: MouseEvent): void {
    $event.preventDefault();
    $event.stopPropagation();
    this.status.isopen = !this.status.isopen;
  }

  ngOnInit(): void {
    var userName = this.storage.getValue('userName');
    if (userName) {
      this.userName = userName;
    }

    this.apiService.get(this.apiUrls.menu.advancedSearchOptions, null, false).subscribe(result => {
      this.searchOptionsData = result;
      this.searchOptions = this.searchOptionsData.result;
      if (this.searchOptions) {
        this.searchOptions.forEach(value => {
          var data = this.storage.getValue('selectedCompany');
          if (data && data.key === value.key) {
            // if (this.tenant.defaultDatabase.key === value.key)
            this.selectedColorValue = value;
          }
        });
      }
    }, error => {
      //if (error.status == 401)
      //this.router.navigate(['/auth/login']);
    })

    this.apiService.get(this.apiUrls.menu.getFooterLink, null, false).subscribe(result => {
      // debugger
      // console.log(result);
      this.footerlinks = result;
    }, error => {
      //if (error.status == 401)
      //this.router.navigate(['/auth/login']);
    })
    let selectedColor = this.getSelectedColorFromLocal();
    if (selectedColor)
      this.changeColor();
    else
      this.changeColor();

    this.adminMenuItems = this.route.snapshot.data['adminMenuItems'];
    this.menuItems = this.route.snapshot.data['menuItems'];
    if (!this.adminMenuItems) {
      this.checkSuperAdmin();
    }
  }


  isSuperAdmin() {
    let userData = this.storage.getObject('userItem');
    let roles = JSON.parse(userData.roles);
    if (roles.indexOf('superadmin') > -1) {
      return true;
    } else {
      false;
    }
  }

  refreshGennyData() {
    this.event.publish('auth:logined', this.storage.getObject('userItem'));
  }

  checkSuperAdmin() {

    let userData = this.storage.getObject('userItem');
    var userName = this.storage.getObject('userName');

    if (userName == "guest@mmdgphc.com") {
      this.isGuestUser = true;
    }

    if (userData) {
      let roles = JSON.parse(userData.roles);
      if (roles.indexOf('superadmin') > -1) {
        let adminItem = this.menuItems.find((item) => {
          return item.name === 'SuperAdmin';
        });

        // let resetPassword = this.menuItems.find((item) => {
        //   return item.name === 'Reset Password';
        // });
        // this.menuItems.forEach((item, index) => {
        //   if (item.name === 'Reset Password') {
        //     this.menuItems.splice(index, 1);
        //   }
        // });

        if (!adminItem) {
          let adminMenuItem = {
            name: 'SuperAdmin',
            parent: 'superAdmin',
            path: 'users'
          };
          this.menuItems.push(adminMenuItem);
        }

        // if (!resetPassword) {
        //   let resetPasswordItem = {
        //     name: 'Reset Password',
        //     parent: 'extranet',
        //     path: 'resetPassword'
        //   };
        //   this.menuItems.push(resetPasswordItem);
        // }

      } else {
        var exist = false;
        this.menuItems.forEach((item, index) => {
          if (item.name === 'SuperAdmin') this.menuItems.splice(index, 1);
        });
        // if (userName != "guest@mmdgphc.com" && !exist) {
        //   let adminMenuItem = {
        //     name: 'Reset Password',
        //     parent: 'extranet',
        //     path: 'resetPassword'
        //   };
        //   this.menuItems.push(adminMenuItem);
        // }
      }
    }
  }

  //on  change event
  onCountryChange(item) {
    this.setSelectedColorToLocal(item.onTrigger.value);
    this.setSelectedCompanyToLocal(item);
    this.changeColor();
    this.setting.emitChildRender();
  }

  changeColor() {
    var location = this.location.path().split('/');
    this.currentPage = this.getHearder(location[location.length - 1]);
    if (location.length > 2 && location[2] === 'genericDetail') {
      this.currentPage = location[3].replace(/%20/g, ' ');
    }
    let item = this.getSelectedColorFromLocal();
    let elements = this.document.getElementsByClassName('borderColor');
    for (var i = 0; i < elements.length; i++) {
      this._renderer.setElementStyle(elements[i], 'border-color', item);
    }

    let backgroundelements = this.document.getElementsByClassName('backgroundColor');
    for (var i = 0; i < backgroundelements.length; i++) {
      this._renderer.setElementStyle(backgroundelements[i], 'background', item);
    }
  }

  setSelectedColorToLocal(item) {
    this.storage.setValue('selectedColor', item);
  }
  setSelectedCompanyToLocal(item) {
    this.storage.setValue('selectedCompany', item);
  }


  getSelectedColorFromLocal() {
    return this.storage.getValue('selectedColor') || 'red';
  }

  getHearder(val: string) {
    switch (val) {
      case 'resetPassword': {
        return 'Reset Password';
      }
      case 'users': {
        return 'Users';
      }
      case 'roles': {
        return 'Roles';
      }
      case 'dashboard': {
        return 'Dashboard';
      }
      case 'partDetail': {
        return 'Part Detail';
      }
      case 'partnarrtive': {
        return 'Part Detail - Part Narrative';
      }
      case 'bom': {
        return 'Part Detail - Bom';
      }
      case 'multilevelbom': {
        return 'Part Detail - Multi Level Bom';
      }
      case 'whereUsed': {
        return 'Part Detail - Where Used';
      }
      case 'fxrates': {
        return 'FX Rates';
      }
      case 'salesOrder': {
        return 'Sales Order and Order Status';
      }
      case 'partsearch': {
        return 'Part Search';
      }
      case 'generic': {
        var data = this.storage.getObject('selectedGenric');
        if (data) {
          return data.name;
        }
        return '';
      }

    }
  }

  logOut() {
    this.authService.logout().subscribe(res => {
      this.storage.removeItem('userName');
      this.storage.removeItem('userItem');
      this.storage.removeGenericData();
      this.router.navigate(['/auth/login']);

    }, error => {
      this.storage.removeItem('userName');
      this.storage.removeItem('userItem');
      this.storage.removeGenericData();
      this.router.navigate(['/auth/login']);
    });
  }

  mouseOver(event) {
    console.log(event);
  }
}
