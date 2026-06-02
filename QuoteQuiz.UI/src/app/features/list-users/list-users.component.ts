import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { UserService, UserDto } from '../../services/user.service';

@Component({
  selector: 'app-list-users',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit {
  displayedColumns = ['id', 'username', 'email', 'status', 'actions'];
  users: UserDto[] = [];

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.userService.getUsers().subscribe(u => this.users = u);
  }

  create(): void {
    this.router.navigate(['/users/create']);
  }

  edit(id: number): void {
    this.router.navigate(['/users/edit', id]);
  }

  delete(id: number): void {
    if (!confirm('Delete this user?')) return;
    this.userService.deleteUser(id).subscribe(() => this.load());
  }
}
