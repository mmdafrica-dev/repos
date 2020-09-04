import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { EnsureModuleLoadedOnceGuard } from './ensureModuleLoadedOnceGuard';
import { AuthService } from './services/auth.service';
import { StorageService } from './services/storage.service';
import { GrowlerModule } from './growler/growler.module';
import { ApiService } from './services/api.service';
import { ApiProvider } from './services/api.provider';
import { SettingService } from './services/setting.service';
import { Events } from './events';
import { ExcelService } from './services/excel.service';
import { CustomHttpInterceptor } from './services/custom-http-interceptor.service';

@NgModule({
  imports: [CommonModule, RouterModule, HttpClientModule, GrowlerModule],
  exports: [RouterModule, HttpClientModule],
  declarations: [],
  providers: [
    ApiService, ApiProvider, AuthService, StorageService, SettingService, Events, ExcelService, {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
    }] // these should be singleton
})
export class CoreModule extends EnsureModuleLoadedOnceGuard {    //Ensure that CoreModule is only loaded into AppModule

  //Looks for the module in the parent injector to see if it's already been loaded (only want it loaded once)
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    super(parentModule);
  }

  //{ provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptor, multi: true },

}



