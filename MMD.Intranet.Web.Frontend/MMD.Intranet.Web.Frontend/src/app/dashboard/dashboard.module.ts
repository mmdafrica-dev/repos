import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard.component';

import { DashboardRoutingModule } from './dashboard-routing.module';

//Services
import { DashboardService } from './dashboard.service';
import { SearchService } from './search.service';

@NgModule({
  imports: [DashboardRoutingModule, SharedModule],
  declarations: [DashboardRoutingModule.components],
  providers: [DashboardService, SearchService]
})
export class DashboardModule { }
