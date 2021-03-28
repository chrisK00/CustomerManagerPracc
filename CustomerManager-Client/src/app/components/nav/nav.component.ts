import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from 'src/app/_interfaces/user';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user: User = new User();
  @ViewChild('loginForm') loginForm: NgForm;

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.user).subscribe(() =>
      this.loginForm.reset());

  }

  logout() {
    this.authService.logout();
  }

}
