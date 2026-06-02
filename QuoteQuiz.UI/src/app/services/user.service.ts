import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

export interface UserDto {
  id: number;
  username: string;
  email: string;
  isDisabled: boolean;
}

export interface CreateUserRequest {
  username: string;
  email: string;
}

export interface UpdateUserRequest {
  id: number;
  username: string;
  email: string;
  isDisabled: boolean;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private readonly key = 'quiz-user-id';
  private readonly apiBaseUrl = environment.apiBaseUrl;// 'https://localhost:7127/api';

  constructor(private http: HttpClient) { }

  getUserId(): number {
    const saved = localStorage.getItem(this.key);
    return saved ? Number(saved) : 1;
  }

  setUserId(id: number): void {
    localStorage.setItem(this.key, String(id));
  }

  getUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(`${this.apiBaseUrl}/users`);
  }

  getUser(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.apiBaseUrl}/users/${id}`);
  }

  createUser(req: CreateUserRequest): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.apiBaseUrl}/users`, req);
  }

  updateUser(req: UpdateUserRequest): Observable<UserDto> {
    return this.http.put<UserDto>(`${this.apiBaseUrl}/users/${req.id}`, req);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/users/${id}`);
  }

  // if you add a disable endpoint later:
  // disableUser(id: number): Observable<void> {
  //   return this.http.patch<void>(`${this.apiBaseUrl}/users/${id}/disable`, {});
  // }
}
