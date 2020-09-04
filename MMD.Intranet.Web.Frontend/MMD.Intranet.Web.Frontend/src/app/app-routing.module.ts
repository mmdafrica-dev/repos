import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules, NoPreloading } from '@angular/router';

import { PreloadModulesStrategy } from './core/strategies/preload-modules.strategy';

const app_routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/dashboard' },
    { path: 'dashboard', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
    { path: 'dashboard/:selectionKey', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
    { path: 'dashboard/:selectionKey/:key', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
    /// Super Admin
    { path: 'superAdmin', loadChildren: 'app/super-admin/super-admin.module#SuperAdminModule' },
    { path: 'about', loadChildren: 'app/about/about.module#AboutModule' },
    { path: '**', pathMatch: 'full', redirectTo: '/dashboard' } //catch any unfound routes and redirect to home page
];

@NgModule({
    imports: [RouterModule.forRoot(app_routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})
export class AppRoutingModule { }

