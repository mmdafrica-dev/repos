import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DashboardService } from '../dashboard.service';

import { IHome } from '../../shared/interfaces/IHome';

@Component({
  selector: 'mmd-dashboard-selection',
  templateUrl: './dashboard-selection.component.html',
  styleUrls: ['./dashboard-selection.component.css']
})
export class DashboardSelectionComponent implements OnInit {

  selectedItem: IHome;

  constructor(private route: ActivatedRoute, public dashboardService: DashboardService) {

  }

  ngOnInit() {
    let key = this.route.snapshot.params['selectionKey'];
    if (key) {
      this.dashboardService.getSelectedItem(key).subscribe((res: IHome) => {
        if (res) {
          this.selectedItem = res;
        }
      });
    }
  }
}