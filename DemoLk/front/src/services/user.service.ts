import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { APIResponse, DataResponse } from '../models/apiModel';
import { LoginData, LoginRequest, RefreshRequest } from '../models/loginData';
import { ApiService } from './api.service';
import { UiService } from './ui.service';
const LS_ROLE = 'role';
const LS_USER = 'user';
const LS_TOKEN = 'token';
const LS_REFRESH_TOKEN = 'refresh_token';
@Injectable({
  providedIn: 'root'
})
export class UserService {


  constructor(private api: ApiService, private router: Router, private ui: UiService) { }

  login(req: LoginRequest) {
    this.api.postData<DataResponse<LoginData>, LoginRequest>('Authorize/login', req, { headers: { 'Content-Type': 'application/json' } }).subscribe(res => {
      if (!res.IsOk) {
        this.ui.show(res.Message);
      } else {
        localStorage.setItem(LS_TOKEN, res.Data.Token);
        localStorage.setItem(LS_REFRESH_TOKEN, res.Data.RefreshToken);
        localStorage.setItem(LS_USER, res.Data.User);
        localStorage.setItem(LS_ROLE, res.Data.Role);
        this.router.navigate(['/']);
      }
    });
  }

  refresh_token() {
    let req = new RefreshRequest();
    req.Refresh = localStorage.getItem(LS_REFRESH_TOKEN);
    return this.api.postData<DataResponse<LoginData>, RefreshRequest | null>('Authorize/Refresh', req);
  }

  logout() {
    localStorage.removeItem(LS_REFRESH_TOKEN);
    localStorage.removeItem(LS_ROLE);
    localStorage.removeItem(LS_TOKEN);
    localStorage.removeItem(LS_USER);
    this.router.navigate(['/']);
  }


  checkLogin() {
    this.api.getData<APIResponse>('Authorize/CheckAuth').subscribe(res => {
      if (res.IsOk) { }
    }, () => {

    });
  }
}

