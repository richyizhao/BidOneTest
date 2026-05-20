import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { User, UserCreatedResponse } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiURL}/api/user`;

  public create(user: User): Observable<UserCreatedResponse> {
    return this.http.post<UserCreatedResponse>(this.apiUrl, user);
  }
}
