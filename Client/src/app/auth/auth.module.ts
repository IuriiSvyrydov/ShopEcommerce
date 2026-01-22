import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    RouterModule,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent

  ]
})
export class AuthModule { }
