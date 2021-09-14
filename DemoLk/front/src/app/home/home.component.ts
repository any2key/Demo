import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
const LS_ROLE = 'role';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router, private user: UserService) {
    this.user.checkLogin();
  }

  ngOnInit(): void {

    if (localStorage.getItem(LS_ROLE) == null) {
      this.router.navigate(['/login']);
    } else {
      if (localStorage.getItem(LS_ROLE) == 'student')
        this.router.navigate(['/student/home']);
      else if (localStorage.getItem(LS_ROLE) == 'admin')
        this.router.navigate(['/admin/home']);
      else if (localStorage.getItem(LS_ROLE) == 'teacher')
        this.router.navigate(['/teacher/home']);
      else this.router.navigate(['/login']);
    }
  }


}
