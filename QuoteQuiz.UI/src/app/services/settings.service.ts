import { Injectable } from '@angular/core';
import { GameMode } from '../models/game.model';
import { environment } from '../../environments';

@Injectable({ providedIn: 'root' })
export class SettingsService {
  private readonly key = 'quiz-mode';

  getMode(): GameMode {
    const saved = localStorage.getItem(this.key);
    return saved ? Number(saved) as GameMode : GameMode.Binary;
  }

  setMode(mode: GameMode): void {
    localStorage.setItem(this.key, String(mode));
  }
}
