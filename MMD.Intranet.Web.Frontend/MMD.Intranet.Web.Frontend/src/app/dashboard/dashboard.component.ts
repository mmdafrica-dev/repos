import { Component, OnInit } from '@angular/core';
import { DashboardService } from './dashboard.service';

import { IHome } from '../shared/interfaces/IHome';
import { TrackByService } from '../core/services/trackby.service';

import { Subscription } from 'rxjs/Subscription';
@Component({
  moduleId: module.id,
  selector: 'mmd-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  homeItems: IHome[] = [];
  sub: Subscription;
  constructor(public dashboardService: DashboardService,
    public trackbyService: TrackByService) {

  }

  ngOnInit() {
    this.sub = this.dashboardService.getHomeItem().subscribe((res: IHome[]) => {
      this.homeItems = res;
    });
  }


  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
