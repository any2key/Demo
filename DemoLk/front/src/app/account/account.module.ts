import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { SharedmaterialModule } from '../../shared-material/shared-material.module';
import { routing } from './account.routing';



@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    SharedmaterialModule,
    routing
  ]
})
export class AccountModule { }
