import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './services/auth.guard';
import { AdminGuard } from './services/admin.guard';

const routes: Routes = [

  // PUBLIC
  { path: 'login', loadComponent: () => import('./features/login/login.component').then(m => m.LoginComponent) },

  // PROTECTED ROUTES
  {
    path: 'quiz',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/quiz-main/quiz-main.component')
        .then(m => m.QuizMainComponent)
  },

  {
    path: 'settings',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/settings-page/settings-page.component')
        .then(m => m.SettingsPageComponent)
  },

  {
    path: 'users',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/list-users/list-users.component')
        .then(m => m.ListUsersComponent)
  },
  {
    path: 'users/create',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/create-user/create-user.component')
        .then(m => m.CreateUserComponent)
  },
  {
    path: 'users/edit/:id',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/edit-user/edit-user.component')
        .then(m => m.EditUserComponent)
  },

  {
    path: 'quotes',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/quotes/list-quote.component')
        .then(m => m.ListQuotesComponent)
  },
  {
    path: 'quotes/create',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/quotes/create-quote.component')
        .then(m => m.CreateQuoteComponent)
  },
  {
    path: 'quotes/edit/:id',
    canActivate: [AuthGuard, AdminGuard],
    loadComponent: () =>
      import('./features/quotes/edit-quote.component')
        .then(m => m.EditQuoteComponent)
  },

  // REVIEWS
  {
    path: 'reviews/:userId',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/reviews/list-user-sessions.component')
        .then(m => m.ListUserSessionsComponent)
  },
  {
    path: 'reviews/session/:sessionId',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/reviews/game-details.component')
        .then(m => m.GameDetailsComponent)
  },

  // DEFAULT
  { path: '', redirectTo: 'quiz', pathMatch: 'full' },
  { path: '**', redirectTo: 'quiz' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
