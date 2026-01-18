import {Routes} from '@angular/router';
import {LoginComponent} from './auth/login/loginComponent';
import {RegisterComponent} from './auth/register/register.component';
import {Home} from './home/home';
import {ServerError} from './core/server-error/server-error';
import {UnAuthenticated} from './core/un-authenticated/un-authenticated';
import {NotFound} from './core/not-found/not-found';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent } // оставляем здесь
    ]
  },
  { path: '', component: Home },
  { path: 'store', loadChildren: () => import('./store/store-module').then(m => m.StoreModule) },
  { path: 'server-error', component: ServerError },
  { path: 'unauthenticated', component: UnAuthenticated },
  {path:'store/basket',loadComponent:()=>
        import('./store/basket/basket.component').then(m=>m.BasketComponent)},
  { path: '**', component: NotFound }
];
