
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { APOLLO_FLAGS, APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { HttpLink, InMemoryCache } from '@apollo/client/core';



export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(),
    {
      provide: APOLLO_FLAGS,
      useValue: {
        useInitialLoading: true, // enable it here
      },
    },
    {
    provide: APOLLO_OPTIONS,
    useFactory: () => {
      return {
        cache: new InMemoryCache(),
        link: new HttpLink({
          uri: 'https://localhost:7254/graphql/',
        }),
      };
    },
  }, importProvidersFrom(
    ApolloModule
  )]
};
