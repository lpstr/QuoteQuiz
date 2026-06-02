import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { ReviewService, GameSessionReviewDto } from '../../services/review.service';

@Component({
  selector: 'app-list-user-sessions',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './list-user-sessions.component.html',
  styleUrls: ['./list-user-sessions.component.css']
})
export class ListUserSessionsComponent implements OnInit {
  displayedColumns = ['id', 'date', 'score', 'actions'];
  sessions: GameSessionReviewDto[] = [];
  userId!: number;

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('userId'));
    this.reviewService.getUserSessions(this.userId)
      .subscribe(s => this.sessions = s);
  }

  open(sessionId: number): void {
    this.router.navigate(['/reviews/session', sessionId]);
  }
}
