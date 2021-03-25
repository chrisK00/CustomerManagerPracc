import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUser = new ReplaySubject<User>(1);
  currentUser$ = this.currentUser.asObservable();

  constructor(private http: HttpClient) { }

  login() {
    const userLogin = { username: 'chris' }
    return this.http.post(`${this.baseUrl}auth/login`, userLogin).pipe(
      map((user: User) => {
        if (!user) {
          return;
        }
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.next(user);
      })
    )
  }
}
