import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../services/user.service';

export const loggedInGuard: CanActivateFn = (route, state) => {
	return true;
	const userService = inject(UserService);
	const router = inject(Router);

	if (userService.isLoggedIn() === true) {
    router.navigate(['/chat']);
    return false;
	}

	return true;
};
