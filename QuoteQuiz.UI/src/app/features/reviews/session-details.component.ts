import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ReviewService, GameSessionReviewDto } from '../../services/review.service';

@Component({
  selector: 'app-session-details',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './session-details.component.html',
  styleUrls: ['./session-details.component.css']
})
export class SessionDetailsComponent implements OnInit {
  session!: GameSessionReviewDto;

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService
  ) { }

  ngOnInit(): void {
    const sessionId = Number(this.route.snapshot.paramMap.get('sessionId'));

    const userId = Number(localStorage.getItem('quiz-user-id') ?? 1);

    this.reviewService.getUserSessions(userId).subscribe(sessions => {
      const found = sessions.find(s => s.id === sessionId);
      if (found) {
        this.session = found;
      }
    });
  }

  correctCount(): number {
    return this.session.questions.filter(q => q.isCorrect).length;
  }
}
