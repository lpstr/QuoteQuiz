import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { Observable } from 'rxjs';
import { User } from '../shared/user.model';
import { Author } from '../shared/author.model';
import { Quote } from '../shared/quote.model';
import {
  GameMode,
  StartGameRequest,
  StartGameResponse,
  NextQuestionResponse,
  SubmitAnswerRequest,
  SubmitAnswerResponse
} from '../shared/game.model';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Users
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/users`);
  }

  // Authors
  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(`${this.baseUrl}/authors`);
  }

  // Quotes
  getQuotes(): Observable<Quote[]> {
    return this.http.get<Quote[]>(`${this.baseUrl}/quotes`);
  }

  // Games
  startGame(userId: number, mode: GameMode): Observable<StartGameResponse> {
    const body: StartGameRequest = { userId, mode };
    return this.http.post<StartGameResponse>(`${this.baseUrl}/games/start`, body);
  }

  getNextQuestion(sessionId: number, mode: GameMode): Observable<NextQuestionResponse> {
    return this.http.get<NextQuestionResponse>(
      `${this.baseUrl}/games/${sessionId}/next`,
      { params: { mode } }
    );
  }

  submitAnswer(request: SubmitAnswerRequest): Observable<SubmitAnswerResponse> {
    return this.http.post<SubmitAnswerResponse>(`${this.baseUrl}/games/answer`, request);
  }
}
