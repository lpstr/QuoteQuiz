import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuizMainComponent } from './features/quiz/quiz-main/quiz-main.component';

const routes: Routes = [
  {
    path: 'quiz',
    loadComponent: () => import('./features/quiz/quiz-main/quiz-main.component')
      .then(m => m.QuizMainComponent)
  },
  {
    path: 'settings', loadComponent: () =>
      import('./features/settings/settings-page/settings-page.component')
        .then(m => m.SettingsPageComponent) }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
