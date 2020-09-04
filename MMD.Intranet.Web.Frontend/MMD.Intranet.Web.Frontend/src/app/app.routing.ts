import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

// Layouts
import { FullLayoutComponent } from './layouts/full-layout.component';
import { SimpleLayoutComponent } from './layouts/simple-layout.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth/login',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    component: SimpleLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: './auth/auth.module#AuthModule',
      }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'superAdmin',
    component: FullLayoutComponent,
    data: {
      adminMenuItems: [
        {
          name: 'Users',
          path: 'users'
        }, {
          name: 'Roles',
          path: 'roles'
        }
      ],
      menuItems: [
        {
          name: 'Dashboard',
          parent: 'extranet',
          path: 'dashboard'
        }
      ]
    },
    children: [
      {
        path: '',
        loadChildren: './superAdmin/super-admin.module#SuperAdminModule',
      }
    ]
  }, {
    path: 'extranet',
    component: FullLayoutComponent,
    data: {
      menuItems: [
        {
          name: 'Dashboard',
          parent: 'extranet',
          path: 'dashboard'
        }
      ]
    },
    children: [
      {
        path: '',
        loadChildren: './pages/dashboard/dashboard.module#DashboardModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
