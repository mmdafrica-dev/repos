import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SuperAdminGuard } from './super-admin-guard';

// Components
import { SuperAdminComponent } from './super-admin.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserComponent } from './user/user.component';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleComponent } from './role/role.component';

const routes: Routes = [
  {
    path: '', component: SuperAdminComponent, children: [
      { path: 'users', component: UsersListComponent },
      { path: 'user/:id', component: UserComponent },
      { path: 'user', component: UserComponent },
      { path: 'roles', component: RoleListComponent },
      { path: 'role/:id', component: RoleComponent },
      { path: 'role', component: RoleComponent }
    ],
    canActivate: [SuperAdminGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [SuperAdminGuard]
})

export class SuperAdminRoutingModule {
  static components = [SuperAdminComponent, UsersListComponent, UserComponent, RoleListComponent, RoleComponent];
}
