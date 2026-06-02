import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  standalone: true,
  imports: [RouterModule, MatToolbarModule, MatButtonModule],
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent {

  constructor(private auth: AuthService, private router: Router) { }

  get isLoggedIn() {
    return this.auth.isLoggedIn;
  }

  get isAdmin() {
    return this.auth.isAdmin;
  }

  get userName() {
    return localStorage.getItem('username');
  }

  get userId() {
    return localStorage.getItem('quiz-user-id');
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
