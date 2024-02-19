import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { LoadingStore } from './shared/stores/loading.store';
import { LoggingInLoadingComponent } from './shared/components/logging-in-loading/logging-in-loading.component';
import { UserStore } from './shared/stores/user.store';
import { Apollo } from 'apollo-angular';
import { GET_USER_BY_EMAIL } from './graphql/queries/userQuery';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [CommonModule, RouterOutlet, LoginComponent, LoggingInLoadingComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	loadingStore = inject(LoadingStore);
	userStore = inject(UserStore);

	constructor(
		private apollo: Apollo,
		private authService: AuthService,
		private userService: UserService,
		private router: Router
	) {}

	ngOnInit() {
		const token = this.authService.getToken();
		if (token === null || this.userService.isLoggedIn() === false) {
			this.userService.clearUser();
			this.authService.removeToken();
			this.router.navigateByUrl('/login');
			return;
		}

		var decoded:any = jwtDecode(token);
		this.apollo.query({
			query: GET_USER_BY_EMAIL,
			variables: {
				email: decoded.email,
			},
		}).subscribe((result: any) => {
			this.userService.setUser(result.data.userByEmail)
			this.router.navigateByUrl('/chat');
		});
	}
}
