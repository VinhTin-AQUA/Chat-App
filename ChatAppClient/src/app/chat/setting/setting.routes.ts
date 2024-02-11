import { Routes } from '@angular/router';
import { SettingComponent } from './setting.component';
import { ProfileComponent } from './profile/profile.component';
import { LanguageComponent } from './language/language.component';

export const routes: Routes = [
	{
		path: '',
		component: SettingComponent,
		children: [
			{ path: '', redirectTo: 'profile', pathMatch: 'full' },
			{ path: 'profile', component: ProfileComponent, title: 'Profile' },
			{ path: 'language', component: LanguageComponent, title: 'Language' },
			{ path: 'change-password', component: LanguageComponent, title: 'Language' },
		],
	},
];
