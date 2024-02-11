import { Routes } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

export const routes: Routes = [
  
  { path: '', component: LoginComponent, title: 'Login' },
  { path: 'login', component: LoginComponent, title: 'Login' },
  { path: 'register', component: RegisterComponent, title: 'Register' },
  { path: 'forgot-password', component: ForgotPasswordComponent, title: 'Forgot password'},
  { path: 'chat', component: ChatComponent, title: 'Chat' },
];
