import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

import { QuoteService, QuoteDto } from '../../services/quote.service';
import { AuthorService, AuthorDto } from '../../services/author.service';

@Component({
  selector: 'app-edit-quote',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './edit-quote.component.html',
  styleUrls: ['./edit-quote.component.css']
})
export class EditQuoteComponent implements OnInit {
  quote!: QuoteDto;
  authors: AuthorDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private quoteService: QuoteService,
    private authorService: AuthorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.authorService.getAuthors().subscribe(a => this.authors = a);
    this.quoteService.getQuote(id).subscribe(q => this.quote = q);
  }

  save(): void {
    this.quoteService.updateQuote({
      id: this.quote.id,
      text: this.quote.text,
      authorId: this.quote.authorId
    }).subscribe(() => this.router.navigate(['/quotes']));
  }
}
