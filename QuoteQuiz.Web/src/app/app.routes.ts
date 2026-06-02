import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'quiz',
    pathMatch: 'full'
  },
  {
    path: 'quiz',
    loadComponent: () =>
      import('./features/quiz/quiz-main/quiz-main.component')
        .then(m => m.QuizMainComponent)
  },
  {
    path: 'settings',
    loadComponent: () =>
      import('./features/settings/settings-page/settings-page.component')
        .then(m => m.SettingsPageComponent)
  }
];
