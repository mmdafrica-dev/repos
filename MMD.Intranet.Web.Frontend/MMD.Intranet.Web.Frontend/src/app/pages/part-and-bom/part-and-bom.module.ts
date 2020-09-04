import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { PartAndBomRoutingModule } from './part-and-bom-routing.module';

import { PartAndBomService } from './services/part-and-bom.service';

//// THIRD party 
import { DataTableModule } from 'angular2-datatable';

@NgModule({
  imports: [
    SharedModule, PartAndBomRoutingModule,DataTableModule
  ],
  declarations: [PartAndBomRoutingModule.components],
  providers: [PartAndBomService]
})
export class PartAndBomModule { }
