import { Routes } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ConversationComponent } from './chat/conversation/conversation.component';
import { ChangePasswordConfirmComponent } from './change-password-confirm/change-password-confirm.component';

export const routes: Routes = [
	{ path: '', redirectTo: 'chat', pathMatch: 'full' },
	{ path: 'login', component: LoginComponent, title: 'Login' },
	{ path: 'register', component: RegisterComponent, title: 'Register' },
	{ path: 'forgot-password', component: ForgotPasswordComponent, title: 'Forgot password' },
	{ path: 'chat', loadChildren: () => import('./chat/chat.routes').then(r => r.routes) },
	{ path: 'change-password-confirm', component: ChangePasswordConfirmComponent, title: 'Change password confirm' },
];
