import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { IVehicle } from '../../models/vehicle.model';
import { AppError } from '../../common/errors/app-error';
import { NotFoundError } from '../../common/errors/not-found-error';

@Component({
	selector: 'app-vehicle-list',
	templateUrl: './vehicle-list.component.html',
	styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
	username: string;
	vehicles: IVehicle[];

	constructor(private route: ActivatedRoute, 
		private vehicleService: VehicleService, 
		private router: Router) {

		route.params.subscribe(p => {
			this.username = p['username'];
			if(!this.username) {
				router.navigate(['/']);
				return;
			}
		})
	}

	ngOnInit() {
		this.vehicleService.getUserVehicles(this.username)
			.subscribe(vehicles => this.vehicles = vehicles,
				(error: AppError) => {
					console.log(error);
					if(error instanceof NotFoundError)
						this.router.navigate(['not-found']);
				})
	}

}
