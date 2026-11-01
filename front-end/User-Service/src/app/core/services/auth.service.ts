import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/auth.models';
import { finalize, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  [x: string]: any;
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Auth`;
  private readonly TOKEN_KEY = 'access_token';
  private readonly REFRESH_KEY = 'refresh_token';

  register(body: RegisterRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, body);
  }

  login(body: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, body).pipe(
      tap((res) => {
        if (res.success && res.token && res.refreshToken) {
          this.saveTokens(res.token, res.refreshToken);
        }
      }),
    );
  }
  refresh(): Observable<AuthResponse> {
    const refreshToken = this.getRefreshToken();
    return this.http.post<AuthResponse>(`${this.apiUrl}/refresh`, { refreshToken }).pipe(
      tap((res) => {
        if (res.success && res.token && res.refreshToken) {
          this.saveTokens(res.token, res.refreshToken);
        }
      }),
    );
  }

  logout(): Observable<unknown> {
    const refreshToken = this.getRefreshToken();
    return this.http
      .post(`${this.apiUrl}/logout`, { refreshToken })
      .pipe(finalize(() => this.clearTokens()));
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getToken() {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  private getRefreshToken() {
    return localStorage.getItem(this.REFRESH_KEY);
  }

  private saveTokens(token: string, refreshToken: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem(this.REFRESH_KEY, refreshToken);
  }
  clearTokens() {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_KEY);
  }
}
