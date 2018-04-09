import { Injectable, Injector, Inject } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Observable } from 'rxjs/Observable';
import { NotFoundError } from '../common/errors/not-found-error';
import { BadRequestError } from '../common/errors/bad-request-error';
import { AppError } from '../common/errors/app-error';

@Injectable()
export class AuthService {
	private baseUrl: string;

	constructor(private http: Http, @Inject('BASE_URL')baseUrl: string) { 
		this.baseUrl = baseUrl;
	}

	getUser(username: string) {
		return this.http.get('http://localhost:57255/api/accounts/' + username)
			.map(res => res.json())
			.catch(this.handleError);
	}
	
	private handleError(error: Response) {
		if (error.status === 404)
			return Observable.throw(new NotFoundError(error))
		if (error.status === 400)
			return Observable.throw(new BadRequestError(error));

		return Observable.throw(new AppError(error))
	}
}
