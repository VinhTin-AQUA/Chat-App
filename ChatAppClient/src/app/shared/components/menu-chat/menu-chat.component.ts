import { Component, inject } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { UserStore } from '../../stores/user.store';

@Component({
	selector: 'app-menu-chat',
	standalone: true,
	imports: [],
	templateUrl: './menu-chat.component.html',
	styleUrl: './menu-chat.component.scss',
})
export class MenuChatComponent {
	isShowMenuProfile: boolean = false;
	private userStore = inject(UserStore);

	constructor(
		private userService: UserService,
		private authService: AuthService,
		private router: Router
	) {}

	showMenuProfile() {
		this.isShowMenuProfile = !this.isShowMenuProfile;
	}

	onLogout(event: Event) {
		event.stopPropagation();
		this.userService.clearUser();
		this.authService.removeToken();
		this.router.navigateByUrl('/login');
	}

	onNavigateToProfile(event: Event) {
		event.stopPropagation();
	}
}
