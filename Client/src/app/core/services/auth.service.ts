import {JwtPayload} from '../../store/models/JwtPayload';
import {decodeJwt} from '../../utils/jwt.util';
import {mapJwtToUser} from '../../utils/jwt.mapper';
import {catchError, switchMap, tap, throwError} from 'rxjs';
import {RegisterRequest} from '../../store/models/RegisterRequest';
import {LoginRequest} from '../../store/models/LoginRequest';
import {computed, inject, Injectable, signal} from '@angular/core';
import {User} from '../../store/models/User';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  private readonly baseUrl = '/api/v1/auth';

   authReady = signal(false);
  readonly isAuthReady = this.authReady.asReadonly();

  private accessToken = signal<string | null>(null);
  private userSignal = signal<User | null>(null);

  readonly user = this.userSignal.asReadonly();
  readonly isLoggedIn = computed(() => !!this.userSignal());

  // ---------- LOGIN ----------
  login(request: LoginRequest) {
    return this.http.post<{ accessToken: string }>(`${this.baseUrl}/login`, request, { withCredentials: true })
      .pipe(
        tap(res => this.setUserFromToken(res.accessToken))
      );
  }

  // ---------- REGISTER ----------
  register(request: RegisterRequest) {
    const payload = {
      firstName: request.firstName,
      lastName: request.lastName,
      email: request.email,
      password: request.password
    };
    return this.http.post<void>(`${this.baseUrl}/register`, payload)
      .pipe(
        switchMap(() => this.login({ email: request.email, password: request.password }))
      );
  }

  // ---------- REFRESH ----------
  refresh() {
    return this.http.post<{ accessToken: string }>(`${this.baseUrl}/refresh-token`, {}, { withCredentials: true })
      .pipe(
        tap(res => this.setUserFromToken(res.accessToken)),
        catchError(err => {
          this.clearAuth();
          return throwError(() => err);
        })
      );
  }

  // ---------- TRY REFRESH ----------
  tryRefresh() {
    return this.refresh();
  }

  // ---------- LOGOUT ----------
  logout() {
    this.clearAuth();
    this.authReady.set(true);
    this.router.navigate(['/auth/login']);
  }

  private clearAuth() {
    this.accessToken.set(null);
    this.userSignal.set(null);
  }

  getToken(): string | null {
    return this.accessToken();
  }

  private setUserFromToken(token: string) {
    this.accessToken.set(token);
    const payload = decodeJwt<JwtPayload>(token);
    this.userSignal.set(payload ? mapJwtToUser(payload, token) : null);
  }
}
