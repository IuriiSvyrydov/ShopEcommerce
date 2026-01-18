import {ApplicationConfig, inject, provideAppInitializer} from '@angular/core';
import { provideRouter } from '@angular/router';
import {
  provideHttpClient,
  withInterceptors
} from '@angular/common/http';

import { routes } from './app.routes';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';

import { AuthService } from './core/services/auth.service';
import { catchError, of } from 'rxjs';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),

    provideHttpClient(
      withInterceptors([
        AuthInterceptor,
        ErrorInterceptor,
        LoadingInterceptor
      ])
    ),

    // ✅ СОВРЕМЕННЫЙ ИНИЦИАЛИЗАТОР
    provideAppInitializer(() => {
      const auth = inject(AuthService);

      return auth.refresh().pipe(
        catchError(() => of(null))
      );
    })
  ]
};
