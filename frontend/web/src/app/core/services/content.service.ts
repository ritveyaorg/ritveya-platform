import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService } from './api.service';
import { ContentMap } from '../models/content.model';

@Injectable({
  providedIn: 'root'
})
export class ContentService {
  constructor(private apiService: ApiService) {}

  load(keys: string[]): Observable<ContentMap> {
    return this.apiService.getContent(keys).pipe(
      map(res => res.content || {})
    );
  }
}

