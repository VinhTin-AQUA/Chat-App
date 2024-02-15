import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';
import { inject } from '@angular/core';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
	// Inject the current `AuthService` and use it to get an authentication token:
	const authToken = inject(AuthService).getToken();

	// Clone the request to add the authentication header.
	// const newReq = req.clone({
	// 	setHeaders: {
	// 		Authorization: `Bearer ${authToken}`,
	// 	},
	// });
	// return next(newReq);
  
  console.log(req.url);
  return next(req);
};
