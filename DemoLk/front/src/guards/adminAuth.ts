import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
const LS_ROLE = 'role';

@Injectable({
  providedIn: 'root'
})


export class AdminAuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate() {
    if (localStorage.getItem(LS_ROLE) != "admin") {
      this.router.navigate(['/forbidden']);
      return false;
    }
    else return true;
  }
}
