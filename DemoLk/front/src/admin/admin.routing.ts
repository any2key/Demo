import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminAuthGuard } from '../guards/adminAuth';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RootComponent } from './root/root.component';




export const routing: ModuleWithProviders<any> = RouterModule.forChild([
  {
    path: 'admin',
    component: RootComponent, canActivate: [AdminAuthGuard],

    children: [
      { path: '', component: DashboardComponent },
      { path: 'home', component: DashboardComponent },
    ]
  }
]);
