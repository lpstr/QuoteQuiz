import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { QuoteService, QuoteDto } from '../../services/quote.service';
import { AuthorService, AuthorDto } from '../../services/author.service';

@Component({
  selector: 'app-list-quote',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './list-quote.component.html',
  styleUrls: ['./list-quote.component.css']
})
export class ListQuotesComponent implements OnInit {
  displayedColumns = ['id', 'text', 'author', 'actions'];
  quotes: QuoteDto[] = [];
  authors: AuthorDto[] = [];

  constructor(
    private quoteService: QuoteService,
    private authorService: AuthorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authorService.getAuthors().subscribe(a => this.authors = a);
    this.load();
  }

  load(): void {
    this.quoteService.getQuotes().subscribe(q => this.quotes = q);
  }

  authorName(id: number): string {
    return this.authors.find(a => a.id === id)?.name ?? 'Unknown';
  }

  create(): void {
    this.router.navigate(['/quotes/create']);
  }

  edit(id: number): void {
    this.router.navigate(['/quotes/edit', id]);
  }

  delete(id: number): void {
    if (!confirm('Delete this quote?')) return;
    this.quoteService.deleteQuote(id).subscribe(() => this.load());
  }
}
