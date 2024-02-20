import { Injectable, inject } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GET_ALL_USER } from '../graphql/queries/userQuery';
import { UserStore } from '../shared/stores/user.store';
import { patchState } from '@ngrx/signals';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';
import { ADD_USER_TO_GROUP } from '../graphql/mutations/userMutation';

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
			email: result.email,
			avatarUrl: result.avatarUrl,
			fullName: result.fullName,
			phoneNumber: result.phoneNumber,
			uniqueCodeUser: result.uniqueCodeUser
		});
	}

	logout() {
		patchState(this.userStore, {
			email: '',
			avatarUrl: '',
			fullName: '',
		});
		localStorage.removeItem('idecord-token');
	}

	isLoggedIn() {
		if (this.jwtHelper.isTokenExpired(this.authService.getToken())) {
			return false;
		} else {
			return true;
		}
	}

	addUserToGroup(uniqueCodeUser: string,uniqueCodeGroup: string) {
		return this.apollo.mutate({
			mutation: ADD_USER_TO_GROUP,
			variables: {
				uniqueCodeUser: uniqueCodeUser,
				uniqueCodeGroup: uniqueCodeGroup
			}
		})
	}
}
