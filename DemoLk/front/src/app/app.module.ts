import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmComponent } from './confirm/confirm.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { routing } from './app.routing';
import { SharedmaterialModule } from '../shared-material/shared-material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../interceptors/auth-interceptor';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { AccountModule } from './account/account.module';
import { AdminModule } from '../admin/admin.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    AppComponent,
    ConfirmComponent,
    ForbiddenComponent,
    NotFoundComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    routing,
    SharedmaterialModule,
    HttpClientModule,
    AccountModule,
    AdminModule,
    RouterModule,
  ],
  exports: [SharedmaterialModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: MAT_DATE_LOCALE, useValue: 'ru-RU' },
  ],
  entryComponents: [ConfirmComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
