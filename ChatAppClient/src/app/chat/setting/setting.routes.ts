import { Routes } from '@angular/router';
import { SettingComponent } from './setting.component';
import { ProfileComponent } from './profile/profile.component';
import { LanguageComponent } from './language/language.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

export const routes: Routes = [
	{
		path: '',
		component: SettingComponent,
		children: [
			{ path: '', redirectTo: 'profile', pathMatch: 'full' },
			{ path: 'profile', component: ProfileComponent, title: 'Profile' },
			{ path: 'language', component: LanguageComponent, title: 'Language' },
			{ path: 'change-password', component: ChangePasswordComponent, title: 'Change password' },
		],
	},
];
