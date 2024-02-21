import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { APOLLO_FLAGS, APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { ApolloLink, HttpLink, InMemoryCache, split } from '@apollo/client/core';
import { JwtModule } from '@auth0/angular-jwt';
import { getMainDefinition } from '@apollo/client/utilities';
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';

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

	// ws://localhost:5000/graphql
	const wsClient = new GraphQLWsLink(
		createClient({
			url: 'ws://localhost:7254/graphql',
		})
	);

	const subcriptionLink = split(
		// split based on operation type
		({ query }) => {
			const kind = getMainDefinition(query);
			return kind.kind === 'OperationDefinition' && kind.operation === 'subscription';
		},
		wsClient,
		http
	);

	return {
		subcriptionLink,
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

