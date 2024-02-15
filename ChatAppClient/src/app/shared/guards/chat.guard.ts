import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../services/user.service';

export const chatGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
	const router = inject(Router);

  if(userService.isLoggedIn() === false) {
    router.navigate(['/login']);
    return false;
  }
  return true;
};
