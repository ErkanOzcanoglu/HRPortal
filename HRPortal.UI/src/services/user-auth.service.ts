import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserInfo } from 'src/models/userInfo';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  constructor(private httpClient: HttpClient, private router: Router) {}
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('authToken'); // Check if authToken exists in localStorage
  }
  registerUser(user: UserInfo) {
    return this.httpClient.post(
      `${environment.apiUrl}api/Auth/register`,
      user,
      {
        responseType: 'text',
      }
    );
  }

  loginUser(user: UserInfo): Observable<string> {
    return this.httpClient
      .post(`https://localhost:7059/api/Auth/login`, user, {
        responseType: 'text',
      })
      .pipe(
        tap((res: string) => {
          if (res.length > 10) {
            // this.router.navigate(['/employee']);
          }
          localStorage.setItem('token', res);
        })
      );
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !!token;
  }

  isUserLoggedIn() {
    let user = localStorage.getItem('token');
    if (user === null) {
      return false;
    }
    return true;
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  getLoggedInUserName() {
    let user = localStorage.getItem('token');
    if (user === null) {
      return '';
    }
    return user;
  }
}
