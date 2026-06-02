import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../services/api.service';
import { QuizSettingsService } from '../../../services/quiz-settings.service';
import { CommonModule } from '@angular/common';
import {
  GameMode,
  NextQuestionResponse,
  SubmitAnswerRequest,
  SubmitAnswerResponse
} from '../../../shared/game.model';

@Component({
  selector: 'app-quiz-main',

  standalone: true,
  imports: [CommonModule],
  templateUrl: './quiz-main.component.html',
  styleUrls: ['./quiz-main.component.css']
})
export class QuizMainComponent implements OnInit {
  mode: GameMode = 'Binary';
  sessionId: number | null = null;
  currentQuestion: NextQuestionResponse | null = null;

  isAnswered = false;
  answerResult: SubmitAnswerResponse | null = null;

  userId = 1; // demo

  constructor(
    private api: ApiService,
    private settings: QuizSettingsService
  ) { }

  ngOnInit(): void {
    this.mode = this.settings.getMode();
    this.startGame();
  }

  startGame(): void {
    this.api.startGame(this.userId, this.mode).subscribe(res => {
      this.sessionId = res.sessionId;
      this.loadNextQuestion();
    });
  }

  loadNextQuestion(): void {
    if (!this.sessionId) return;

    this.isAnswered = false;
    this.answerResult = null;

    this.api.getNextQuestion(this.sessionId, this.mode).subscribe({
      next: q => {
        this.currentQuestion = q;
      },
      error: () => {
        this.currentQuestion = null;
      }
    });
  }

  onBinaryAnswer(answerYes: boolean): void {
    if (!this.currentQuestion || !this.sessionId) return;

    const req: SubmitAnswerRequest = {
      sessionId: this.sessionId,
      quoteId: this.currentQuestion.quoteId,
      mode: 'Binary',
      selectedAuthorId: this.currentQuestion.suggestedAuthorId,
      answerYesNo: answerYes
    };

    this.submit(req);
  }

  onMultipleChoiceAnswer(authorId: number): void {
    if (!this.currentQuestion || !this.sessionId) return;

    const req: SubmitAnswerRequest = {
      sessionId: this.sessionId,
      quoteId: this.currentQuestion.quoteId,
      mode: 'MultipleChoice',
      selectedAuthorId: authorId
    };

    this.submit(req);
  }

  private submit(req: SubmitAnswerRequest): void {
    this.api.submitAnswer(req).subscribe(res => {
      this.isAnswered = true;
      this.answerResult = res;
    });
  }

  next(): void {
    this.loadNextQuestion();
  }
}
