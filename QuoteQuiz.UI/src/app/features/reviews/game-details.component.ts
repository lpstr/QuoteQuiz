import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ReviewService, GameSessionReviewDto } from '../../services/review.service';

@Component({
  selector: 'app-game-details',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.css']
})
export class GameDetailsComponent implements OnInit {
  game!: GameSessionReviewDto;

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService
  ) { }

  ngOnInit(): void {
    const sessionId = Number(this.route.snapshot.paramMap.get('sessionId'));
    const userId = Number(localStorage.getItem('quiz-user-id') ?? 1);

    this.reviewService.getUserSessions(userId).subscribe(sessions => {
      const found = sessions.find(s => s.sessionId === sessionId);
      if (found) {
        this.game = found;
        console.log(this.game);
      }
    });
  }

  correctCount(): number {
    return this.game.questions.filter(q => q.isCorrect).length;
  }
}
