import { Injectable, inject } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GET_ALL_USER, SEARCH_FRIENDS } from '../graphql/queries/userQuery';
import { UserStore } from '../shared/stores/user.store';
import { patchState } from '@ngrx/signals';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';

@Injectable({
	providedIn: 'root',
})
export class UserService {
	private userStore = inject(UserStore);
	constructor(
		private apollo: Apollo,
		private jwtHelper: JwtHelperService,
		private authService: AuthService
	) {}

	getAllUsers() {
		return this.apollo.query({
			query: GET_ALL_USER,
		});
	}

	setUser(result: any) {
		patchState(this.userStore, {
			email: result.data.email,
			avatarUrl: result.data.avatarUrl,
			fullName: result.data.fullName,
		});
	}

	clearUser() {
		patchState(this.userStore, {
			email: '',
			avatarUrl: '',
			fullName: '',
		});
	}

	isLoggedIn() {
		if (this.jwtHelper.isTokenExpired(this.authService.getToken())) {
			return false;
		} else {
			return true;
		}
	}

	searchFriends(name: string) {
		return this.apollo.query({
			query: SEARCH_FRIENDS,
			variables: {
				name: name
			}
		})
	}
}
