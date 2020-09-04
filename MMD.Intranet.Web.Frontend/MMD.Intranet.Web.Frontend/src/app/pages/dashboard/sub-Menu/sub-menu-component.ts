import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { SubMenuService } from '../services/sub-menu.service';
import { Events } from '../../../core/events';

@Component({
    moduleId: module.id,
    selector: 'mmd-sub-menu',
    templateUrl: 'sub-menu-component.html',
    styleUrls: ['sub-menu-component.scss']
})
export class SubmenuComponent implements OnInit {

    subMenus = [];
    mainMenu: any;
    constructor(private _subMenuService: SubMenuService,
        private activatedRoute: ActivatedRoute,
        private event: Events) { }

    ngOnInit() {

        this.activatedRoute.params.subscribe((params: Params) => {
            let key = params['key'];
            let menukey = params['menukey'];
            if (key) {
                // this.getsubroute(key, menukey);
                this.mainMenu = key;
            }
        });
    }

    getsubroute(key, menukey) {
        this.event.publish('loaderShow', true);
        this._subMenuService.getSubMenu(key, menukey).subscribe(result => {
            this.subMenus = result.result;
            this.event.publish('loaderShow', false);
        }, error => {

        })
    }



}