import { inject } from '@angular/core';
import {
  HttpInterceptorFn
} from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { catchError, switchMap, throwError } from 'rxjs';

const PUBLIC_URLS = [
  '/auth/login',
  '/auth/register',
  '/auth/forgot-password',
  '/auth/reset-password',
  '/auth/refresh-token'
];

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);


  if (PUBLIC_URLS.some(url => req.url.includes(url))) {
    return next(req);
  }

  const token = auth.getToken();

  if (!token) {
    return next(req);
  }

  const authReq = req.clone({
    setHeaders: { Authorization: `Bearer ${token}` }
  });

  return next(authReq).pipe(
    catchError(err => {

      if (err.status !== 401) {
        return throwError(() => err);
      }

      if (!auth.getToken()) {
        auth.logout();
        router.navigate(['/auth/login']);
        return throwError(() => err);
      }

      // ✅ пробуем refresh
      return auth.refresh().pipe(
        switchMap(() => {
          const newToken = auth.getToken();
          if (!newToken) {
            auth.logout();
            router.navigate(['/auth/login']);
            return throwError(() => err);
          }

          return next(
            req.clone({
              setHeaders: { Authorization: `Bearer ${newToken}` }
            })
          );
        })
      );
    })
  );
};
