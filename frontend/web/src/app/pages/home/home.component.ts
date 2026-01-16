import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentService } from '../../core/services/content.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  loading = true;
  content: Record<string, string> = {};
  error: string | null = null;

  constructor(
    private contentService: ContentService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const keys = [
      'home.heroTitle',
      'home.heroSubtitle',
      'home.focus1',
      'home.focus2',
      'home.focus3'
    ];

    this.contentService.load(keys).subscribe({
      next: (content) => {
        console.log('Content loaded:', content);
        this.content = content || {};
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Loading set to:', this.loading);
        console.log('Content object:', this.content);
      },
      error: (err) => {
        console.error('Error loading content:', err);
        this.error = err?.message || 'Failed to load content';
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Loading set to (error):', this.loading);
      },
      complete: () => {
        console.log('Content subscription completed');
      }
    });
  }
}

