import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RootComponent } from './root/root.component';
import { SharedmaterialModule } from '../shared-material/shared-material.module';
import { routing } from './admin.routing';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [DashboardComponent, RootComponent],
  imports: [
    CommonModule,
    SharedmaterialModule, routing
  ]
})
export class AdminModule { }
