import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SuperAdminRoutingModule } from './super-admin-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TreeviewModule } from './../shared/modules/tree-view';
///PROVIDERS
import { UserService } from './services/user.service';
import { RoleService } from './services/role.service';
import { ModalModule } from 'ngx-bootstrap/modal';
//// THIRD party 
import { DataTableModule } from 'angular2-datatable';

@NgModule({
  imports: [SharedModule, ReactiveFormsModule, SuperAdminRoutingModule, DataTableModule, TreeviewModule.forRoot(), ModalModule.forRoot()],
  declarations: [SuperAdminRoutingModule.components],
  providers: [UserService, RoleService]
})
export class SuperAdminModule {

}
