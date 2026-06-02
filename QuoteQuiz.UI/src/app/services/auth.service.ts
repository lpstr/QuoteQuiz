import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { environment } from '../../environments';

interface LoginResponse {
  token: string;
  username: string;
  roles: string[];
  userId: number;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiBaseUrl;;

  constructor(private http: HttpClient) { }

  login(email: string) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/auth/login`, { email }).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('username', res.username);
        localStorage.setItem('quiz-user-name', res.username);
        localStorage.setItem('roles', JSON.stringify(res.roles));
        localStorage.setItem('quiz-user-id', res.userId.toString());
      })
    );
  }

  get token(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.clear();
  }

  get roles(): string[] {
    return JSON.parse(localStorage.getItem('roles') ?? '[]');
  }

  get isAdmin(): boolean {
    return this.roles.includes('Admin');
  }

  get isLoggedIn(): boolean {
    return !!this.token;
  }
}
