import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AccountService } from './account.service';

@Injectable()
export class AuthGuard implements CanActivate {

	constructor(private router: Router, private service: AccountService) { }

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		if (this.service.isLoggedIn()) return true;

		this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
		return false;
	}
}
