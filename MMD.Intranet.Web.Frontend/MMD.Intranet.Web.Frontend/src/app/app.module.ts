import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { TabsModule } from 'ngx-bootstrap/tabs';
import { NAV_DROPDOWN_DIRECTIVES } from './shared/nav-dropdown.directive';

import { ChartsModule } from 'ng2-charts/ng2-charts';
import { CustomFormsModule } from 'ng2-validation'

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { Config } from './config/config';
// Routing Module
import { AppRoutingModule } from './app.routing';
import { environment } from '../environments/environment';

// Layouts
import { FullLayoutComponent } from './layouts/full-layout.component';
import { SimpleLayoutComponent } from './layouts/simple-layout.component';
import { ToastService } from './core/services/toast.service';
import { ToastyModule } from 'ng2-toasty';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    Ng4LoadingSpinnerModule.forRoot(),
    NgxSpinnerModule,
    ToastyModule.forRoot(),
    BsDropdownModule.forRoot(),
    // BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    CustomFormsModule,
    CoreModule,//Singleton objects (services, components that are loaded only once, etc.)
    SharedModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    FullLayoutComponent,
    SimpleLayoutComponent,
    NAV_DROPDOWN_DIRECTIVES,

  ],
  providers: [
    ToastService,
    NgxSpinnerService,
    { provide: 'Constant', useValue: environment.constant },
    { provide: 'apiUrls', useValue: Config.apiUrls },
    { provide: 'guestUser', useValue: Config.guestUser },
    { provide: 'tenant', useValue: Config.tenant },
    { provide: 'rowCount', useValue: Config.count },
    { provide: 'appVersion', useValue: Config.appVersion },
    { provide: LocationStrategy, useClass: HashLocationStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule { }
