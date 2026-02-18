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
  private readonly tokenKey = 'access_token';

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
  //-------------------- RESET PASSWORD
  resetPassword(userId:string,token:string,newPassword:string){
    return this.http.post(`${this.baseUrl}/reset-password`,{userId,token,newPassword});
  }

  // ---------- REFRESH ----------
  refresh() {
    const token = localStorage.getItem('refresh_token');
    if (!token) return throwError(() => new Error('No refresh token'));

    return this.http.post<{ jwtToken: string; refreshToken: string }>(
      `${this.baseUrl}/refresh-token`,
      { token }, { withCredentials: true }
    ).pipe(
      tap(res => {
        this.setUserFromToken(res.jwtToken);
        localStorage.setItem('refresh_token', res.refreshToken);
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
  //------------FORGOT PASSWORD----------
  forgotPassword(emailOrPhone: string,clientUrl:string) {
    return this.http.post<{message:string}>(`${this.baseUrl}/forgot-password`, { emailOrPhone, clientUrl });
  }

  private clearAuth() {
    localStorage.removeItem(this.tokenKey);
    this.accessToken.set(null);
    this.userSignal.set(null);
  }

  getToken(): string | null {
    return this.accessToken()
      ??localStorage.getItem(this.tokenKey);
  }
  get userId(): string | null {
    return this.userSignal()?.id ?? null;
  }
  get userEmail(): string | null {
    return this.userSignal()?.email ?? null;
  }
  restoreUser() {
    const token = localStorage.getItem(this.tokenKey);
    if (!token) {
      const refreshToken = localStorage.getItem('refresh_token');
      if (refreshToken) {
        this.refresh().subscribe();
      }
      return;
    }
    this.setUserFromToken(token);
  }



  private setUserFromToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
    this.accessToken.set(token);
    const payload = decodeJwt<JwtPayload>(token);
    this.userSignal.set(payload ? mapJwtToUser(payload, token) : null);
  }
}
