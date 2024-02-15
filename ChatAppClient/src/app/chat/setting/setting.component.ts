import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';

@Component({
	selector: 'app-setting',
	standalone: true,
	imports: [RouterOutlet, RouterLink, RouterLinkActive],
	templateUrl: './setting.component.html',
	styleUrl: './setting.component.scss',
})
export class SettingComponent {
	constructor(
		private router: Router,
		private userService: UserService,
		private authService: AuthService
	) {}

	onLogout() {
		this.userService.clearUser();
		this.authService.removeToken();
		this.router.navigateByUrl('/login');
	}
}
