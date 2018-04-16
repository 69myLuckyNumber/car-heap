import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { IVehicle } from '../../models/vehicle.model';
import { AppError } from '../../common/errors/app-error';
import { NotFoundError } from '../../common/errors/not-found-error';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'app-vehicle-list',
	templateUrl: './vehicle-list.component.html',
	styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent {
	username: string;
	vehicles: IVehicle[];

	constructor(private route: ActivatedRoute, 
		private vehicleService: VehicleService, 
		private router: Router) {
		
		Observable.combineLatest(this.route.params)
			.switchMap(params => {
				this.username = params[0]['username'];
				if(!this.username) {
					router.navigate(['/']);
					return;
				}
				return this.vehicleService.getUserVehicles(this.username);
			}).subscribe(vehicles => this.vehicles = vehicles,
				(error: AppError) => {
					if(error instanceof NotFoundError)
						this.router.navigate(['not-found']);
				});
	}
}
