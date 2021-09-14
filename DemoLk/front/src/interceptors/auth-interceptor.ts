import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpEvent,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, filter, map } from 'rxjs/operators';
import { UserService } from '../services/user.service';

const LS_ROLE = 'role';
const LS_USER = 'user';
const LS_TOKEN = 'token';
const LS_REFRESH_TOKEN = 'refresh_token';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private user: UserService, private route: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const authToken = localStorage.getItem(LS_TOKEN);
    return next.handle(
      !authToken ? req : req.clone({
        headers: req.headers.set('Authorization', `Bearer ${authToken}`)
      })
    ).pipe(
      filter(event => !!event.type),
      map(
        event => {
          console.log('event--->>>', event);
          return event;
        }
      ),
      catchError((err: HttpErrorResponse) => this.handleError(err))
    );
  }

  handleError(error: HttpErrorResponse): Observable<never> {
    if (localStorage.getItem(LS_REFRESH_TOKEN) == null) {
      this.user.logout();
      this.route.navigate(['/login']);
    }
    if (error.status == 403) {
      this.route.navigate(['forbidden']);
    }
    this.user.refresh_token().subscribe(res => {
      if (res.IsOk) {
        localStorage.setItem(LS_TOKEN, res.Data.Token);
        localStorage.setItem(LS_REFRESH_TOKEN, res.Data.RefreshToken);
        localStorage.setItem(LS_USER, res.Data.User);
        localStorage.setItem(LS_ROLE, res.Data.Role);
      } else {
        this.user.logout();
        this.route.navigate(['/login']);
      }
    }, () => {
      localStorage.removeItem(LS_TOKEN);
      localStorage.removeItem(LS_REFRESH_TOKEN);
      localStorage.removeItem(LS_USER);
      localStorage.removeItem(LS_ROLE);
      this.route.navigate(['/login']);
    });
    return throwError(error);
  }
}


