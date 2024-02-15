import { Routes } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ConversationComponent } from './chat/conversation/conversation.component';
import { ChangePasswordConfirmComponent } from './change-password-confirm/change-password-confirm.component';
import { loggedInGuard } from './shared/guards/logged-in.guard';
import { chatGuard } from './shared/guards/chat.guard';

export const routes: Routes = [
	{ path: '', redirectTo: 'login', pathMatch: 'full' },

	{ path: 'login', component: LoginComponent, title: 'Login',canActivate: [loggedInGuard] },

	{
		path: 'register',
		component: RegisterComponent,
		title: 'Register',
		canActivate: [loggedInGuard]
	},
	{
		path: 'forgot-password',
		component: ForgotPasswordComponent,
		title: 'Forgot password',
	},
	{
		path: 'chat',
		loadChildren: () => import('./chat/chat.routes').then(r => r.routes),
		canActivate: [chatGuard]
	},
	{
		path: 'change-password-confirm',
		component: ChangePasswordConfirmComponent,
		title: 'Change password confirm',
	},
];
