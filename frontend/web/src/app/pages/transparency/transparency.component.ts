import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentService } from '../../core/services/content.service';

@Component({
  selector: 'app-transparency',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './transparency.component.html',
  styleUrl: './transparency.component.scss'
})
export class TransparencyComponent implements OnInit {
  loading = true;
  content: Record<string, string> = {};
  error: string | null = null;

  constructor(
    private contentService: ContentService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const keys = ['transparency.title', 'transparency.p1', 'transparency.p2'];

    this.contentService.load(keys).subscribe({
      next: (content) => {
        console.log('Transparency content loaded:', content);
        this.content = content || {};
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Transparency loading set to:', this.loading);
      },
      error: (err) => {
        console.error('Error loading transparency content:', err);
        this.error = err?.message || 'Failed to load content';
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Transparency loading set to (error):', this.loading);
      },
      complete: () => {
        console.log('Transparency content subscription completed');
      }
    });
  }
}

