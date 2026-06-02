import { Injectable } from '@angular/core';
import { GameMode } from '../shared/game.model';

@Injectable({ providedIn: 'root' })
export class QuizSettingsService {
  private readonly key = 'quiz_mode';

  getMode(): GameMode {
    const stored = localStorage.getItem(this.key);
    if (stored === 'Binary' || stored === 'MultipleChoice') {
      return stored;
    }
    return 'Binary';
  }

  setMode(mode: GameMode): void {
    localStorage.setItem(this.key, mode);
  }
}
