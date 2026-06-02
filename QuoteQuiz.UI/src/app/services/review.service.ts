import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments';

export interface GameQuestionReviewDto {
  id: number;
  quoteText: string;
  correctAuthor: string;
  mode: number;
  isCorrect: boolean;
  answerYesNo: boolean | null;
  selectedAuthor: string | null;
  suggestedAuthor: string | null;
  answeredAt: string;
}

export interface GameSessionReviewDto {
  sessionId: number;
  startedAt: string;
  finishedAt: string;
  totalQuestions: number;
  correctAnswers: number;
  questions: GameQuestionReviewDto[];
}

@Injectable({ providedIn: 'root' })
export class ReviewService {
  private readonly apiBaseUrl = environment.apiBaseUrl;// 'https://localhost:7127/api';

  constructor(private http: HttpClient) { }

  getUserSessions(userId: number): Observable<GameSessionReviewDto[]> {
    return this.http.get<GameSessionReviewDto[]>(
      `${this.apiBaseUrl}/games/user/${userId}/sessions`
    );
  }
}
