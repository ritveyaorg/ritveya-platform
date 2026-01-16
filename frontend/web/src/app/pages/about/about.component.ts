import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentService } from '../../core/services/content.service';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss'
})
export class AboutComponent implements OnInit {
  loading = true;
  content: Record<string, string> = {};
  error: string | null = null;

  constructor(
    private contentService: ContentService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const keys = ['about.title', 'about.p1', 'about.p2'];

    this.contentService.load(keys).subscribe({
      next: (content) => {
        console.log('About content loaded:', content);
        this.content = content || {};
        this.loading = false;
        this.cdr.detectChanges();
        console.log('About loading set to:', this.loading);
      },
      error: (err) => {
        console.error('Error loading about content:', err);
        this.error = err?.message || 'Failed to load content';
        this.loading = false;
        this.cdr.detectChanges();
        console.log('About loading set to (error):', this.loading);
      },
      complete: () => {
        console.log('About content subscription completed');
      }
    });
  }
}

