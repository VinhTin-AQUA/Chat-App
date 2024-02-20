import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MenuChatComponent } from '../shared/components/menu-chat/menu-chat.component';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { jwtDecode } from 'jwt-decode';
import { GET_USER_BY_EMAIL } from '../graphql/queries/userQuery';
import { Apollo } from 'apollo-angular';
import { GroupService } from '../services/group.service';
import { UserStore } from '../shared/stores/user.store';
import { GroupStore } from '../shared/stores/group.store';
import { patchState } from '@ngrx/signals';

@Component({
	selector: 'app-chat',
	imports: [RouterOutlet, MenuChatComponent],
	standalone: true,
	templateUrl: './chat.component.html',
	styleUrl: './chat.component.scss',
})
export class ChatComponent {
	userStore = inject(UserStore);
	groupStore = inject(GroupStore);

	constructor(
		private authService: AuthService,
		private userService: UserService,
		private apollo: Apollo,
		private groupService: GroupService,
		private router: Router
	) {}

	ngOnInit() {
		const token = this.authService.getToken();
		if (token === null || this.userService.isLoggedIn() === false) {
			this.userService.logout();
			this.router.navigateByUrl('/login');
			return;
		}

		var decoded: any = jwtDecode(token);
		this.apollo
			.query({
				query: GET_USER_BY_EMAIL,
				variables: {
					email: decoded.email,
				},
			})
			.subscribe((result: any) => {
				if (result.data.userByEmail === null) {
					console.log(result.data.userByEmail === null);
					this.userService.logout();
					this.router.navigateByUrl('/login');
					return;
				}
				this.userService.setUser(result.data.userByEmail);

				this.groupService
					.getGroupsOfUser(this.userStore.uniqueCodeUser())
					.subscribe((result: any) => {
						patchState(this.groupStore, {
							groups: result.data.groupsOfUser,
							length: result.data.groupsOfUser.length,
						});
					});
			});
	}
}
