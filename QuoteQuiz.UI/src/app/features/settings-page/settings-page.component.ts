import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

import { GameMode } from '../../models/game.model';
import { SettingsService } from '../../services/settings.service';
import { UserService, UserDto } from '../../services/user.service';

@Component({
  selector: 'app-settings-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './settings-page.component.html',
  styleUrls: ['./settings-page.component.css']
})
export class SettingsPageComponent implements OnInit {
  GameMode = GameMode;

  mode: GameMode;
  userId: number;

  users: UserDto[] = [];

  constructor(
    private settings: SettingsService,
    private userService: UserService
  ) {
    this.mode = this.settings.getMode();
    this.userId = this.userService.getUserId();
  }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users.filter(u => !u.isDisabled); // hide disabled users
      console.log(this.users);
    });
  }

  save(): void {
    this.settings.setMode(this.mode);
    this.userService.setUserId(this.userId);
    alert('Settings saved');
  }
}
