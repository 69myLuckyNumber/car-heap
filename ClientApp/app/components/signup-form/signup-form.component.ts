import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn } from '@angular/forms';
import { UsernameValidators } from '../../common/validators/username.validators';
import { AccountService } from '../../services/account.service';
import { PasswordValidators } from '../../common/validators/password.validators';
import { BadRequestError } from '../../common/errors/bad-request-error';
import { AppError } from '../../common/errors/app-error';
import { Router } from '@angular/router';

@Component({
	selector: 'app-signup-form',
	templateUrl: './signup-form.component.html',
	styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {
	constructor(private accountService: AccountService, private router: Router){}

	form: FormGroup = new FormGroup({
		firstName: new FormControl('', [
			Validators.required,
			Validators.maxLength(32)
		]),
		lastName: new FormControl('', [
			Validators.maxLength(32)
		]),
		username: new FormControl('', [
			Validators.required,
			Validators.minLength(4),
			Validators.maxLength(32),
			UsernameValidators.cannotContainSpace
		],
		UsernameValidators.shouldBeUnique(this.accountService)),
		email: new FormControl('', [
			Validators.email
		],
		UsernameValidators.shouldBeUnique(this.accountService)),

		password: new FormControl('', [
			Validators.minLength(6),
			Validators.maxLength(32),
			Validators.required,
			PasswordValidators.matchPassword
		]),
		passwordAgain: new FormControl('', [
			Validators.required,
			PasswordValidators.matchPassword
		]),
		phone: new FormControl('', [
			Validators.pattern("^[0-9]{12}$")
		])
	});

	onSubmit() {
		if(this.form.valid) {
			this.accountService
				.register(this.form.value)
				.subscribe(
					res => {
						if(res)
							this.router.navigate(['/login']);
					},
					(error: AppError) => {
						console.log(error);
						if (error instanceof BadRequestError)
							this.form.setErrors({register_failure: "Error occurred. Enter valid data."});
					}
				);
		}
		else this.form.setErrors({register_failure: "Error occurred. Enter valid data."});
	}


	get usernameControl() {
		return this.form.get('username');
	}
	get firstNameControl() {
		return this.form.get('firstName');
	}
	get lastNameControl() {
		return this.form.get('lastName');
	}
	get emailControl() {
		return this.form.get('email');
	}
	get passwordControl() {
		return this.form.get('password');
	}
	get passwordAgainControl() {
		return this.form.get('passwordAgain');
	}

	get phoneControl() {
		return this.form.get('phone');
	}
}

