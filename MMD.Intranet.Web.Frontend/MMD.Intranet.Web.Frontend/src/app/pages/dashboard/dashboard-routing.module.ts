import { NgModule } from '@angular/core';
import { RouterModule, Routes, RouterLink } from '@angular/router';

import { DashboardComponent } from './dashboard-component';
import { DashboardGuard } from './dashboard-guard';
import { SubmenuComponent } from './sub-Menu/sub-menu-component';
import { MainMenuComponent } from './main-menu/main-menu.component';
import { SearchComponent } from './search/search.component';
import { UserService } from '../../superAdmin/services/user.service';

import {
    MMDDateComponent,
    MMDDateRangeComponent,
    MMDInputNumberComponent,
    MMDInputTextComponent,
    MMDPartNumberComponent,
    MMDSelectionComponent,
    HeaderSearchComponent,
    MMDCheckboxComponent,
    MMDPartsNumberComponent,
    MMDInputTextRangeComponent,
    MMDTableComponent,
    MMDMultiLevelTableComponent,
    GenricTableComponent,
    Pannel1Component,
    Pannel2Component
} from '../../shared/components/components';
import { DetailComponent } from '../part-and-bom/detail/detail.component';
import { PartAndBomService } from '../part-and-bom/services/part-and-bom.service';
import { BomService } from '../bom/services/bom.service';
import { BomDetailComponent } from '../bom/detail/detail.component';
import { PartDetailComponent } from './part-detail/part-detail.component';
import { PartNarrtiveComponent } from '../part-and-bom/part-narrative/part-narrative.component';
import { MultiLevelBomComponent } from '../bom/multi-level-bom/multi-level-bom.component';
import { PurchaseOrderLinesComponent } from './purchase-order-lines/purchase-order-lines.component';
import { WhereUsedComponent } from './where-used/where-used.component';
import { PartAllocationComponent } from '../part-and-bom/part-allocation/part-allocation.component';
import { StockMovementsComponent } from './stock-movements/stock-movements.component';
import { StandardCostHistoryComponent } from './standard-cost-history/standard-cost-history.component';
import { StockComponent } from './stock/stock.component';
import { WorkOrdersComponent } from './work-orders/work-orders.component';
import { BomComponent } from './bom/bom.component';
import { SupplierPriceComponent } from './supplier-price/supplier-price.component';
import { CostSetDetailComponent } from './cost-set-details/cost-set-details.component';
import { ModuleComponent } from './module/module.component';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { FXRatesComponent } from './fx-rates/fx-rates.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { GenricComponent } from './genric/genric.component';
import { GernericColDisplayComponent } from '../../shared/components/genric-table/gerneric-col-display/gerneric-col-display.component';

const routes: Routes = [
    {
        path: '', component: DashboardComponent, children: [
            { path: 'dashboard', component: ModuleComponent },
            { path: 'resetPassword', component: ResetPasswordComponent },
            { path: ':id/resetPassword', component: ResetPasswordComponent },
            { path: 'mainmenu/:key', component: MainMenuComponent },
            { path: 'submenu/:key/:menukey', component: SubmenuComponent },
            { path: 'search/:key/bomdetail', component: BomDetailComponent },
            { path: 'search/:key/:subMenuKey', component: SearchComponent },
            { path: 'salesOrder', component: SalesOrderComponent },
            { path: 'generic', component: GenricComponent },
            { path: 'genericDetail/:name/:value', component: GenricComponent },
            { path: 'fxrates', component: FXRatesComponent },
            {
                path: 'partsearchdetail/:partNumber', component: PartDetailComponent, children: [
                    { path: 'partDetail', component: DetailComponent },
                    { path: 'partnarrtive', component: PartNarrtiveComponent },
                    { path: 'multilevelbom', component: MultiLevelBomComponent },
                    { path: 'polines', component: PurchaseOrderLinesComponent },
                    { path: 'whereUsed', component: WhereUsedComponent },
                    { path: 'partallocation', component: PartAllocationComponent },
                    { path: 'stockmovements', component: StockMovementsComponent },
                    { path: 'stock', component: StockComponent },
                    { path: 'standardcosthistory', component: StandardCostHistoryComponent },
                    { path: 'workorders', component: WorkOrdersComponent },
                    { path: 'bom', component: BomComponent },
                    { path: 'supplierprice', component: SupplierPriceComponent },
                    { path: 'costsetdetail', component: CostSetDetailComponent }
                ]
            }
        ],
        canActivate: [DashboardGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    declarations: [],
    providers: [DashboardGuard, PartAndBomService, BomService, UserService]
})
export class DashboardRoutingModule {
    static components = [
        DashboardComponent,
        MainMenuComponent,
        SubmenuComponent,
        SearchComponent,
        MMDDateComponent,
        MMDDateRangeComponent,
        MMDInputNumberComponent,
        MMDInputTextComponent,
        MMDPartNumberComponent,
        MMDSelectionComponent,
        DetailComponent,
        HeaderSearchComponent,
        MMDCheckboxComponent,
        MMDPartsNumberComponent,
        MMDInputTextRangeComponent,
        BomDetailComponent,
        SalesOrderComponent,
        FXRatesComponent,
        ResetPasswordComponent,
        PartDetailComponent,
        PartNarrtiveComponent,
        MultiLevelBomComponent,
        PurchaseOrderLinesComponent,
        WhereUsedComponent,
        PartAllocationComponent,
        StockMovementsComponent,
        StockComponent,
        StandardCostHistoryComponent,
        WorkOrdersComponent,
        BomComponent,
        SupplierPriceComponent,
        CostSetDetailComponent,
        ModuleComponent,
        MMDTableComponent,
        MMDMultiLevelTableComponent,
        GenricComponent,
        GenricTableComponent,
        Pannel1Component,
        Pannel2Component,
        GernericColDisplayComponent

    ];
}
