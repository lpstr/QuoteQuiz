import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

export interface QuoteDto {
  id: number;
  text: string;
  authorId: number;
}

export interface CreateQuoteRequest {
  text: string;
  authorId: number;
}

export interface UpdateQuoteRequest {
  id: number;
  text: string;
  authorId: number;
}

@Injectable({ providedIn: 'root' })
export class QuoteService {
  private readonly apiBaseUrl = environment.apiBaseUrl;// 'https://localhost:7127/api';

  constructor(private http: HttpClient) { }

  getQuotes(): Observable<QuoteDto[]> {
    return this.http.get<QuoteDto[]>(`${this.apiBaseUrl}/quotes`);
  }

  getQuote(id: number): Observable<QuoteDto> {
    return this.http.get<QuoteDto>(`${this.apiBaseUrl}/quotes/${id}`);
  }

  createQuote(req: CreateQuoteRequest): Observable<QuoteDto> {
    return this.http.post<QuoteDto>(`${this.apiBaseUrl}/quotes`, req);
  }

  updateQuote(req: UpdateQuoteRequest): Observable<QuoteDto> {
    return this.http.put<QuoteDto>(`${this.apiBaseUrl}/quotes/${req.id}`, req);
  }

  deleteQuote(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/quotes/${id}`);
  }
}
