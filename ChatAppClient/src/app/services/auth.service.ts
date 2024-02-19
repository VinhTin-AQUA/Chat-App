import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { RegisterRequest } from '../shared/models/registerRequest';
import { CREATE_USER } from '../graphql/mutations/userMutation';
import { LoginRequest } from '../shared/models/loginRequest';
import { LOGIN, REFRESH_TOKEN } from '../graphql/queries/authQuery';


@Injectable({
	providedIn: 'root',
})
export class AuthService {
	constructor(private apollo: Apollo) {}

	register(model: RegisterRequest) {
		return this.apollo.mutate({
			mutation: CREATE_USER,
			variables: {
				email: model.email,
				fullName: model.fullName,
				password: model.password,
				reEnterPassword: model.reEnterPassword,
				avatarUrl: model.avatarUrl,
			},
		});
	}

	login(model: LoginRequest) {
		return this.apollo.query({
			query: LOGIN,
			variables: {
				email: model.email,
				password: model.password,
			},
		});
	}

	saveToken(token: string) {
		localStorage.setItem('idecord-token', token);
	}

	getToken() {
		return localStorage.getItem('idecord-token');
	}

	refreskToken(email: string) {
		return this.apollo.query({
			query: REFRESH_TOKEN,
			variables: {
				email: email,
			},
		});
	}
}
