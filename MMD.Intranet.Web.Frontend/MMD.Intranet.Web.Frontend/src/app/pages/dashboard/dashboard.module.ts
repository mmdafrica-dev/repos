import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';

import { MainMenuService } from './services/main-menu.service';
import { SubMenuService } from './services/sub-menu.service';
import { SearchService } from './services/search-service';
import { DataFilterPipe } from '../../shared/pipe/searchPipe';
import { DataPanelFilterPipe } from '../../shared/pipe/searchPanelPipe';
import { PartDetailService } from './services/part-detail.service';
import { OrderService } from './services/order.service';
import { StockService } from './services/stock.services';
import { CostService } from './services/cost.services';
import { SupplierService } from './services/supplier.services';
import { GenericService } from './services/generic.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AutoCompleteModalComponent } from './module/auto-complete-modal/auto-complete-modal';
import { AutoCompleteLookUpService } from './module/auto-complete-lookup.service';
import { SafeHtmlsPipe } from 'app/shared/pipe/safe-html.pipe';
import { AdditionalFilterModalComponent } from 'app/shared/components/genric-table/additional-filter-modal/additional-filter-modal';
import { MiniGennyComponent } from './module/mini-genny/mini-genny';


@NgModule({
  imports: [
    SharedModule, DashboardRoutingModule, BsDatepickerModule.forRoot(), ModalModule.forRoot()
  ],
  declarations: [DashboardRoutingModule.components, AutoCompleteModalComponent, MiniGennyComponent, AdditionalFilterModalComponent, DataFilterPipe, DataPanelFilterPipe, SafeHtmlsPipe],
  providers: [
    MainMenuService,
    SubMenuService,
    SearchService,
    PartDetailService,
    OrderService,
    StockService,
    CostService,
    SupplierService,
    GenericService,
    AutoCompleteLookUpService
  ],
  entryComponents: [AutoCompleteModalComponent, AdditionalFilterModalComponent]
})
export class DashboardModule { }
