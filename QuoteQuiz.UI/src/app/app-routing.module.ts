import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // quiz
  {
    path: 'quiz',
    loadComponent: () =>
      import('./features/quiz-main/quiz-main.component')
        .then(m => m.QuizMainComponent)
  },

  // settings
  {
    path: 'settings',
    loadComponent: () =>
      import('./features/settings-page/settings-page.component')
        .then(m => m.SettingsPageComponent)
  },

  // user
  {
    path: 'users',
    loadComponent: () =>
      import('./features/list-users/list-users.component')
        .then(m => m.ListUsersComponent)
  },
  {
    path: 'users/create',
    loadComponent: () =>
      import('./features/create-user/create-user.component')
        .then(m => m.CreateUserComponent)
  },
  {
    path: 'users/edit/:id',
    loadComponent: () =>
      import('./features/edit-user/edit-user.component')
        .then(m => m.EditUserComponent)
  },

  // quote
  {
    path: 'quotes',
    loadComponent: () =>
      import('./features/quotes/list-quote.component')
        .then(m => m.ListQuotesComponent)
  },
  {
    path: 'quotes/create',
    loadComponent: () =>
      import('./features/quotes/create-quote.component')
        .then(m => m.CreateQuoteComponent)
  },
  {
    path: 'quotes/edit/:id',
    loadComponent: () =>
      import('./features/quotes/edit-quote.component')
        .then(m => m.EditQuoteComponent)
  },

  //Reviews
  {
    path: 'reviews/:userId',
    loadComponent: () =>
      import('./features/reviews/list-user-sessions.component')
        .then(m => m.ListUserSessionsComponent)
  },
  {
    path: 'reviews/session/:sessionId',
    loadComponent: () =>
      import('./features/reviews/game-details.component')
        .then(m => m.GameDetailsComponent)
  },

  // default
  { path: '', redirectTo: 'quiz', pathMatch: 'full' },
  { path: '**', redirectTo: 'quiz' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
