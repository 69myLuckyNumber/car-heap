import { Component, OnInit } from '@angular/core';
import { ILoginModel } from '../../models/login.model';
import { FormGroup } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { AppError } from '../../common/errors/app-error';
import { BadRequestError } from '../../common/errors/bad-request-error';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-login-form',
	templateUrl: './login-form.component.html',
	styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
	invalidCredentials: boolean = false;

	constructor(private accountService: AccountService, 
		private router: Router,
		private route: ActivatedRoute) { }

	login(form :FormGroup) {
		if(form.invalid)
			this.invalidCredentials = true;
		else {
			this.accountService.login(form.value)
				.subscribe(res => {
					if(res) {			
						let returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
						this.router.navigate([returnUrl || '/']);
					}
				},
				(error: AppError) => {
					if(error instanceof BadRequestError) {
						this.invalidCredentials = true;
					}
				})
		}
	}
}
