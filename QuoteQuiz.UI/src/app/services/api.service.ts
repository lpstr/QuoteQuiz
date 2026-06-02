import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  GameMode,
  NextQuestionResponse,
  StartGameResponse,
  SubmitAnswerRequest,
  SubmitAnswerResponse
} from '../models/game.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

@Injectable({ providedIn: 'root' })
export class ApiService {
   
  private apiBaseUrl = environment.apiBaseUrl;// 'https://localhost:7127/api';

  constructor(private http: HttpClient) { }

  startGame(mode: GameMode, userId: number): Observable<StartGameResponse> {
    return this.http.post<StartGameResponse>(`${this.apiBaseUrl}/games/start`, {
      mode,
      userId
    });
  }

  getNextQuestion(
    sessionId: number,
    mode: GameMode,
    userId: number
  ): Observable<NextQuestionResponse> {
    return this.http.get<NextQuestionResponse>(
      `${this.apiBaseUrl}/games/${sessionId}/next`,
      {
        params: {
          mode,
          userId
        }
      }
    );
  }

  submitAnswer(
    req: SubmitAnswerRequest & { userId: number }
  ): Observable<SubmitAnswerResponse> {
    return this.http.post<SubmitAnswerResponse>(
      `${this.apiBaseUrl}/games/answer`,
      req
    );
  }
}
