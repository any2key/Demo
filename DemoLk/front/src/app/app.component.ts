import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenav } from '@angular/material/sidenav';
import { } from '@angular/core';
import { ViewChild } from '@angular/core';
import { UserService } from '../services/user.service';

const LS_ROLE = 'role';
const LS_USER = 'user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'smc';
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;

  constructor(private user: UserService, private router: Router) { }

  get Role(): string | null {
    return localStorage.getItem(LS_ROLE);
  }

  get User(): string | null {
    return localStorage.getItem(LS_USER);
  }

  logout() {
    this.user.logout();
  }

  route(path: string) {
    this.router.navigate([path]);
  }

}


