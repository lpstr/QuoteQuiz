import { Component } from '@angular/core';
import { QuizSettingsService } from '../../../services/quiz-settings.service';
import { GameMode } from '../../../shared/game.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-settings-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.css']
})
export class SettingsPageComponent {
  mode: GameMode;

  constructor(private settings: QuizSettingsService) {
    this.mode = this.settings.getMode();
  }

  onModeChange(mode: GameMode): void {
    this.mode = mode;
    this.settings.setMode(mode);
  }
}
