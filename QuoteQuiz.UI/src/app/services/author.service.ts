import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

export interface AuthorDto {
  id: number;
  name: string;
  description: string;
  isDisabled: boolean;
}

@Injectable({ providedIn: 'root' })
export class AuthorService {
  private readonly apiBaseUrl = environment.apiBaseUrl;// 'https://localhost:7127/api';

  constructor(private http: HttpClient) { }

  getAuthors(): Observable<AuthorDto[]> {
    return this.http.get<AuthorDto[]>(`${this.apiBaseUrl}/authors`);
  }
}
