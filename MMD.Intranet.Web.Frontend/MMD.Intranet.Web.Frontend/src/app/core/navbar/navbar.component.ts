import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';

import { AuthService } from '../services/auth.service';
import { GrowlerService, GrowlerMessageType } from '../growler/growler.service';

import { NavService } from '../services/nav.service';
import { IMenu, IMenuItem } from '../../shared/interfaces/IMenu';

@Component({
    moduleId: module.id,
    selector: 'mmd-navbar',
    templateUrl: 'navbar.component.html',
    styleUrls: ['navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
    isCollapsed: boolean;
    loginLogoutText: string = 'Login';
    sub: Subscription;
    menus: IMenu[];
    selectedMenu: IMenu;

    constructor(private router: Router, private authservice: AuthService, private navService: NavService, private growler: GrowlerService) { }

    ngOnInit() {
        this.sub = this.authservice.authChanged
            .subscribe((loggedIn: boolean) => {
                this.setLoginLogoutText();
            },
            (err: any) => console.log(err));

        if (!this.authservice.isAuthenticated) {

        }

        this.navService.getMenu().subscribe((res: IMenu[]) => {
            this.menus = res;
            this.selectedMenu = this.menus[0];
        });

    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    loginOrOut() {
        const isAuthenticated = this.authservice.isAuthenticated;
        if (isAuthenticated) {
            this.authservice.logout()
                .subscribe((status: boolean) => {
                    this.setLoginLogoutText();
                    this.growler.growl('Logged Out', GrowlerMessageType.Info);
                    //  this.router.navigate(['/customers']);
                    return;
                },
                (err: any) => console.log(err));
        }
        this.redirectToLogin();
    }
    selectMenu(menu) {
        this.selectedMenu = menu;
    }

    redirectToLogin() {
        this.router.navigate(['/login']);
    }

    setLoginLogoutText() {
        this.loginLogoutText = (this.authservice.isAuthenticated) ? 'Logout' : 'Login';
    }

}