import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { DashboardSearchComponent } from './dashboard-search/dashboard-search.component';
import { DashboardSelectionComponent } from './dashboard-selection/dashboard-selection.component';

const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'selection', component: DashboardSelectionComponent },
    { path: 'search', component: DashboardSearchComponent }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardRoutingModule {
    static components = [DashboardComponent, DashboardSearchComponent, DashboardSelectionComponent];
}