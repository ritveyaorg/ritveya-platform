import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { Initiative } from '../../core/models/initiative.model';

@Component({
  selector: 'app-initiatives',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './initiatives.component.html',
  styleUrl: './initiatives.component.scss'
})
export class InitiativesComponent implements OnInit {
  initiatives: Initiative[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private apiService: ApiService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.apiService.getInitiatives().subscribe({
      next: (initiatives) => {
        console.log('Initiatives loaded:', initiatives);
        this.initiatives = initiatives.sort((a, b) => a.displayOrder - b.displayOrder);
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Initiatives loading set to:', this.loading);
      },
      error: (err) => {
        console.error('Error loading initiatives:', err);
        this.error = err?.message || 'Failed to load initiatives';
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Initiatives loading set to (error):', this.loading);
      },
      complete: () => {
        console.log('Initiatives subscription completed');
      }
    });
  }
}

