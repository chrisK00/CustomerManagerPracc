import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_interfaces/user';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User = new User();

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  register() {
    this.authService.register(this.user).subscribe();
  }
}
