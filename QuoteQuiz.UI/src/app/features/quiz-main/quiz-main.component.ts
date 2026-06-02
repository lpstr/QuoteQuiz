import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { UserService } from '../../services/user.service';
import { SettingsService } from '../../services/settings.service';
import {
  GameMode,
  NextQuestionResponse,
  SubmitAnswerRequest,
  SubmitAnswerResponse
} from '../../models/game.model';

@Component({
  selector: 'app-quiz-main',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './quiz-main.component.html',
  styleUrls: ['./quiz-main.component.css']
})
export class QuizMainComponent {
  GameMode = GameMode;

  mode: GameMode;
  userId: number;
  username: string;

  sessionId: number | null = null;
  currentQuestion: NextQuestionResponse | null = null;

  isAnswered = false;
  answerResult: SubmitAnswerResponse | null = null;

  constructor(
    private api: ApiService,
    private userService: UserService,
    private settings: SettingsService
  ) {
    this.userId = this.userService.getUserId();
    this.username = 'Testing';
    //this.username = this.userService.getUser(this.userId); // NEW
    console.log(this.userService.getUser(this.userId));
    this.mode = this.settings.getMode();
  }

  ngOnInit(): void {

  }

  get selectedUserName(): string {
    return this.userService.getUserName();
  }

  startGame(): void {
    this.api.startGame(this.mode, this.userId).subscribe(res => {
      this.sessionId = res.sessionId;
      this.loadNext();
    });
  }

  loadNext(): void {
    if (!this.sessionId) return;

    this.isAnswered = false;
    this.answerResult = null;

    this.api.getNextQuestion(this.sessionId, this.mode, this.userId).subscribe({
      next: q => this.currentQuestion = q,
      error: () => this.currentQuestion = null
    });
  }

  onBinaryAnswer(answerYes: boolean): void {
    if (!this.currentQuestion || !this.sessionId) return;

    const req: SubmitAnswerRequest & { userId: number } = {
      sessionId: this.sessionId,
      quoteId: this.currentQuestion.quoteId,
      mode: GameMode.Binary,
      selectedAuthorId: this.currentQuestion.suggestedAuthorId!,
      answerYesNo: answerYes,
      userId: this.userId
    };
    console.log(this.currentQuestion.suggestedAuthorId!);
    this.submit(req);
  }

  onMultipleChoiceAnswer(authorId: number): void {
    if (!this.currentQuestion || !this.sessionId) return;

    const req: SubmitAnswerRequest & { userId: number } = {
      sessionId: this.sessionId,
      quoteId: this.currentQuestion.quoteId,
      mode: GameMode.MultipleAnswer,
      selectedAuthorId: authorId,
      userId: this.userId
    };

    this.submit(req);
  }

  private submit(req: SubmitAnswerRequest & { userId: number }): void {
    this.api.submitAnswer(req).subscribe(res => {
      this.isAnswered = true;
      this.answerResult = res;
    });
  }

  next(): void {
    this.loadNext();
  }
}

