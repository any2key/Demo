import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { LoginRequest } from '../../../models/loginData';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  constructor(private user: UserService) { }


  ngOnInit(): void {

  }


  tiles: Tile[] = [
    { text: 'One', cols: 3, rows: 1, color: 'lightblue' },
    { text: 'Two', cols: 1, rows: 2, color: 'lightgreen' },
    { text: 'Three', cols: 1, rows: 1, color: 'lightpink' },
    { text: 'Four', cols: 2, rows: 1, color: '#DDBDF1' },
  ];

  login = new FormGroup(
    {
      user: new FormControl(''),
      password: new FormControl('')
    });

  keyUpFunction(res: any) {
    console.log(res);
  }
  loginMe() {
    this.user.login(new LoginRequest(this.login.get('user')?.value, this.login.get('password')?.value));
  }
}
export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}
