import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { HttpClientModule, HttpHeaders, provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { APOLLO_FLAGS, APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { ApolloLink, HttpLink, InMemoryCache } from '@apollo/client/core';
import { AuthService } from './services/auth.service';
import { JwtModule } from '@auth0/angular-jwt';

export function createApollo() {
	const http = new HttpLink({
		uri: 'https://localhost:7254/graphql/',
	});

	const authLink = new ApolloLink((operation, forward) => {
		// Get the authentication token from local storage if it exists
		const token = localStorage.getItem('idecord-token');

		// Use the setContext method to set the HTTP headers.
		operation.setContext({
			headers: {
				Authorization: token ? `Bearer ${token}` : '',
			},
		});

		// Call the next link in the middleware chain.
		return forward(operation);
	});

	return {
		link: authLink.concat(http),
		cache: new InMemoryCache(),
	};
}

export function tokenGetter() {
	return localStorage.getItem('idecord-token');
}

export const appConfig: ApplicationConfig = {
	providers: [
		provideRouter(routes),
		{
			provide: APOLLO_FLAGS,
			useValue: {
				useInitialLoading: true, // enable it here
			},
		},
		{
			provide: APOLLO_OPTIONS,
			useFactory: createApollo,
			deps: [HttpClientModule],
		},
		provideHttpClient(),
		importProvidersFrom(
			ApolloModule,
			HttpClientModule,
			JwtModule.forRoot({
				config: {
					tokenGetter: tokenGetter,
				},
			})
		),
	],
};
