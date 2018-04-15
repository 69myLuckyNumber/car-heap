import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { ISaveVehicle } from '../models/vehicle.model';
import { handleError } from '../common/errors/response-error-handler';

@Injectable()
export class VehicleService {
	private baseUrl: string;

	constructor(private http: Http, 
		private authHttp: AuthHttp,
		@Inject('BASE_URL') baseUrl: string) {
			this.baseUrl = baseUrl;
	}

	getFeatures() {
		return this.http.get(this.baseUrl + '/api/features')
			.map(res => res.json());
	}

	getMakes() {
		return this.http.get(this.baseUrl + '/api/makes')
			.map(res => res.json());
	}

	create(vehicle: ISaveVehicle) {
		return this.authHttp.post(this.baseUrl + '/api/vehicles', vehicle)
		.map(res => res.json())
		.catch(handleError);
	}
	getVehicle(id: number) {
		return this.http.get(this.baseUrl + '/api/vehicles/' + id)
			.map(res => res.json())
			.catch(handleError);
	}
	getVehicles() {
		return this.http.get('http://localhost:57255/api/vehicles/')
		.map(res => res.json())
		.catch(handleError);	
		
	}
	deleteVehicle(id:number) {
		return this.authHttp.delete(this.baseUrl + '/api/vehicles/delete/' + id)
			.map(res => res.json())
			.catch(handleError);
	}
}
