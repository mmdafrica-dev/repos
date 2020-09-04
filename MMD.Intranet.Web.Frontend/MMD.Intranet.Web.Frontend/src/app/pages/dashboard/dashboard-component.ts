import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'mmd-daashboard',
    templateUrl: 'dashboard-component.html',
    styleUrls: ['dashboard-component.scss']
})
export class DashboardComponent implements OnInit {

    mainMenus = [];
    ngOnInit() {

    }

} 