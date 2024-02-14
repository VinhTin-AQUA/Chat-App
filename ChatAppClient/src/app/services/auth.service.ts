import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { LOGIN } from '../graphql/queries/userQuery';
import { RegisterRequest } from '../shared/models/registerRequest';
import { CREATE_USER } from '../graphql/mutations/userMutation';
import { LoginRequest } from '../shared/models/loginRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private apollo: Apollo) { }

  register(model: RegisterRequest) {
    return this.apollo
    .mutate({
      mutation: CREATE_USER,
      variables: {
        email: model.email,
        fullName: model.fullName,
        password: model.password,
        reEnterPassword: model.reEnterPassword,
        avatarUrl: model.avatarUrl,
      },
    })
  }

  login(model: LoginRequest) {
    return this.apollo
			.query({
				query: LOGIN,
				variables: {
					email: model.email,
					password: model.password,
				},
			})
  }

  saveToken(token: string) {
    localStorage.setItem('idecord-jwt', token)
  }
}
