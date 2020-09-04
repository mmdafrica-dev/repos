import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { MainMenuService } from '../services/main-menu.service';
import { Events } from '../../../core/events';

@Component({
    moduleId: module.id,
    selector: 'mmd-main-menu',
    templateUrl: 'main-menu.component.html',
    styleUrls: ['main-menu.component.scss']
})
export class MainMenuComponent implements OnInit {

    mainMenus = [];
    mainMenu: string;
    constructor(private _submenuService: MainMenuService,
        private activatedRoute: ActivatedRoute,
        private event: Events) { }

    ngOnInit() {
        this.activatedRoute.params.subscribe((params: Params) => {
            let key = params['key'];
            if (key) {
                this.mainMenu = key;
                // this._getModule(key);
            }
        });

    }

    _getModule(key) {
        this.event.publish('loaderShow', true);
        this._submenuService.getMainMenu(key).subscribe(result => {
            this.event.publish('loaderShow', false);
            this.mainMenus = result.result;
        }, error => {

        })
    }
}