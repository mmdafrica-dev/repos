import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DashboardService } from '../dashboard.service';
import { SearchService } from '../search.service';

import { IHomeItem } from '../../shared/interfaces/IHome';

@Component({
  selector: 'mmd-dashboard-search',
  templateUrl: './dashboard-search.component.html',
  styleUrls: ['./dashboard-search.component.css']
})
export class DashboardSearchComponent implements OnInit {
  selectedItem: IHomeItem;
  searchResult = [];
  constructor(private route: ActivatedRoute,
    public searchService: SearchService,
    public dashboardService: DashboardService) {

  }

  ngOnInit() {
    let selectionKey = this.route.snapshot.params['selectionKey'];
    let key = this.route.snapshot.params['key'];
    if (key) {
      this.dashboardService.getSelectedSearchItem(selectionKey, key).subscribe((res: IHomeItem) => {
        if (res) {
          this.selectedItem = res;
        }
      });
    }
  }


  search() {
    let selectedValues = [];
    this.selectedItem.searchOption.options.forEach(item => {
      if (item.value) {
        let data = {
          name: item.name,
          value: item.value
        };
        selectedValues.push(data);
      }
    });
    if (selectedValues.length != this.selectedItem.searchOption.options.length) {
      alert('Please check required fields');
    }
    this.searchService.getPartSearch(selectedValues).subscribe((res: any) => {
        this.searchResult = res;
        console.log(res);
      });
  }

}
