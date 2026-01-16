import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { Trustee } from '../../core/models/trustee.model';

@Component({
  selector: 'app-trustees',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './trustees.component.html',
  styleUrl: './trustees.component.scss'
})
export class TrusteesComponent implements OnInit {
  trustees: Trustee[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private apiService: ApiService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.apiService.getTrustees().subscribe({
      next: (trustees) => {
        console.log('Trustees loaded:', trustees);
        this.trustees = trustees.sort((a, b) => a.displayOrder - b.displayOrder);
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Trustees loading set to:', this.loading);
      },
      error: (err) => {
        console.error('Error loading trustees:', err);
        this.error = err?.message || 'Failed to load trustees';
        this.loading = false;
        this.cdr.detectChanges();
        console.log('Trustees loading set to (error):', this.loading);
      },
      complete: () => {
        console.log('Trustees subscription completed');
      }
    });
  }
}

