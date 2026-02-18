import {Routes} from '@angular/router';

import {HomeComponent} from './home/home.component';
import {ServerError} from './core/server-error/server-error';
import {UnAuthenticated} from './core/un-authenticated/un-authenticated';
import {NotFound} from './core/not-found/not-found';


export const routes: Routes = [
  {

    path: 'auth',
    children: [
      {
        path: 'login',
        loadComponent: () =>
          import('./auth/login/login.component')
            .then(m => m.LoginComponent)
      },
      {
        path: 'register',
        loadComponent: () =>
          import('./auth/register/register.component')
            .then(m => m.RegisterComponent)
      },
      {
        path: 'forgot-password',
        loadComponent: () =>
          import('./auth/forgot-password/forgot-password.component')
            .then(m => m.ForgotPasswordComponent)
      },
      {
        path: 'reset-password',
        loadComponent: () =>
          import('./auth/reset-password/reset-password.component')
            .then(m => m.ResetPasswordComponent)
      }
    ]
  },
  {
    path: 'payment/:id',
    loadComponent: () =>
      import('./store/payment/payment.component').then(m => m.PaymentComponent)
  },

  { path: '', component: HomeComponent },
  { path: 'store', loadChildren: () => import('./store/store-module').then(m => m.StoreModule) },
  { path: 'server-error', component: ServerError },
  { path: 'unauthenticated', component: UnAuthenticated },
  {path:'store/basket',loadComponent:()=>
        import('./store/basket/basket.component').then(m=>m.BasketComponent)},
  { path: '**', component: NotFound }
];
