import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../core/services/api.service';
import { ContactSubmissionCreate } from '../../core/models/contact.model';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent {
  contactForm: FormGroup;
  submitting = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService
  ) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: [''],
      phone: [''],
      message: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.contactForm.valid) {
      this.submitting = true;
      this.successMessage = null;
      this.errorMessage = null;

      const payload: ContactSubmissionCreate = {
        ...this.contactForm.value,
        sourcePage: 'contact'
      };

      this.apiService.submitContact(payload).subscribe({
        next: () => {
          this.successMessage = 'Thank you for your message. We will get back to you soon.';
          this.contactForm.reset();
          this.submitting = false;
        },
        error: (err) => {
          this.errorMessage = err.message || 'Failed to submit your message. Please try again.';
          this.submitting = false;
        }
      });
    }
  }
}

