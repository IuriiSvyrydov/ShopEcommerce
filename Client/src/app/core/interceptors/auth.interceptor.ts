import { inject } from '@angular/core';
import {
  HttpInterceptorFn,
  HttpRequest
} from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import {catchError, switchMap, throwError} from 'rxjs';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (req.url.includes('/Auth')) {
    return next(req);
  }

  const token = auth.getToken();

  const authReq = token
    ? req.clone({
      setHeaders: {Authorization: `Bearer ${token}`}
    })
    : req;

  return next(authReq).pipe(
    catchError(err => {
      if (err.status === 401) {
        return auth.refresh().pipe(
          switchMap(() => {
            const newToken = auth.getToken();
            return next(
              req.clone({
                setHeaders: {Authorization: `Bearer ${newToken}`}
              })
            );
          })
        );
      }
      return throwError(() => err);
    })
  );
}
