import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Trustee } from '../models/trustee.model';
import { Initiative } from '../models/initiative.model';
import { ContentMap } from '../models/content.model';
import { ContactSubmissionCreate } from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getTrustees() {
    return this.http.get<Trustee[]>(
      `${environment.apiBaseUrl}/api/public/trustees`
    );
  }

  getInitiatives() {
    return this.http.get<Initiative[]>(
      `${environment.apiBaseUrl}/api/public/initiatives`
    );
  }

  getContent(keys: string[]) {
    return this.http.get<{ content: Record<string, string> }>(
      `${environment.apiBaseUrl}/api/public/content`,
      { params: { keys: keys.join(',') } }
    );
  }

  submitContact(payload: ContactSubmissionCreate): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(
      `${environment.apiBaseUrl}/api/public/contact`,
      payload
    )
      .pipe(
        catchError(error => {
          return throwError(() => new Error('Failed to submit contact form. Please try again later.'));
        })
      );
  }
}

