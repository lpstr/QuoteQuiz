import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { QuoteService } from '../../services/quote.service';
import { AuthorService, AuthorDto } from '../../services/author.service';

@Component({
  selector: 'app-create-quote',
  standalone: true,
  imports: [
    CommonModule, FormsModule,
    MatFormFieldModule, MatInputModule,
    MatSelectModule, MatButtonModule
  ],
  templateUrl: './create-quote.component.html',
  styleUrls: ['./create-quote.component.css']
})
export class CreateQuoteComponent implements OnInit {
  text = '';
  authorId!: number;
  authors: AuthorDto[] = [];

  constructor(
    private quoteService: QuoteService,
    private authorService: AuthorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authorService.getAuthors().subscribe(a => this.authors = a);
  }

  save(): void {
    this.quoteService.createQuote({
      text: this.text,
      authorId: this.authorId
    }).subscribe(() => this.router.navigate(['/quotes']));
  }
}
