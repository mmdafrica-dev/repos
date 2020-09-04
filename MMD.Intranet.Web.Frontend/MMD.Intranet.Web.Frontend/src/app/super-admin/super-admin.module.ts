import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SuperAdminRoutingModule } from './super-admin-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

///PROVIDERS
import { UserService } from './services/user.service';
import { RoleService } from './services/role.service';
@NgModule({
  imports: [SharedModule, ReactiveFormsModule, SuperAdminRoutingModule],
  declarations: [SuperAdminRoutingModule.components],
  providers: [UserService, RoleService]
})
export class SuperAdminModule { }
