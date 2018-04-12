import { Injectable, Injector, Inject } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

import { tokenNotExpired, JwtHelper } from 'angular2-jwt';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Observable } from 'rxjs/Observable';
import { NotFoundError } from '../common/errors/not-found-error';
import { BadRequestError } from '../common/errors/bad-request-error';
import { AppError } from '../common/errors/app-error';
import { handleError } from '../common/errors/response-error-handler';
import { IRegisterModel } from '../models/register.model';
import { ILoginModel } from '../models/login.model';
import { IUser } from '../models/user.model';

@Injectable()
export class AccountService {
	private baseUrl: string;

	constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
		this.baseUrl = baseUrl;
	}

	getUser(username: string) {
		return this.http.get(this.baseUrl + '/api/accounts/' + username)
			.map(res => res.json())
			.catch(handleError);
	}

	register(account: IRegisterModel) {
		let body = JSON.stringify(account);
		let headers = new Headers({ 'Content-Type': 'application/json' });
		let options = new RequestOptions({ headers: headers });
		   
		return this.http.post(this.baseUrl + "/api/accounts", body, options)
			.map(res => res.json())
			.catch(handleError);
	}

	login(credentials: ILoginModel) {
		let headers = new Headers({ 'Content-Type': 'application/json' });
		
		return this.http.post(this.baseUrl + '/api/accounts/login',
			JSON.stringify(credentials), {headers})
			.map(res => {
				let result = res.json();
				console.log(result);
				if(result && result.auth_token) {
					if (typeof window !== 'undefined') {
						localStorage.setItem('token', result.auth_token);
						return true;
					}
				}
				return false;
			})
			.catch(handleError);
	}
	logout() {
		if (typeof window !== 'undefined') 
			localStorage.removeItem('token');
	}

	isLoggedIn() {
		if (typeof window !== 'undefined') {
			return tokenNotExpired();
		}
	}
	get currentUser() {
		let token;
		if (typeof window !== 'undefined')
			token = localStorage.getItem('token');
		
		if(!token) return null;
		return new JwtHelper().decodeToken(token);
	}

}
