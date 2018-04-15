import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { IVehicle } from '../../models/vehicle.model';
import { AppError } from '../../common/errors/app-error';
import { NotFoundError } from '../../common/errors/not-found-error';
import { AccountService } from '../../services/account.service';

@Component({
	selector: 'app-vehicle-view',
	templateUrl: './vehicle-view.component.html',
	styleUrls: ['./vehicle-view.component.css']
})
export class VehicleViewComponent implements OnInit{
	vehicleId: number;
	vehicle: IVehicle;

	constructor(private route: ActivatedRoute, 
		private router: Router, 
		private service: VehicleService,
		private accountService: AccountService) {

		route.params.subscribe(p => {
			this.vehicleId = +p['id'];
			if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
				router.navigate(['/']);
				return; 
			  }
		})
	}

	ngOnInit(){
		this.service.getVehicle(this.vehicleId)
			.subscribe(vehicle => this.vehicle = vehicle,
				(error: AppError) => {
					console.log(error);
					if(error instanceof NotFoundError) {
						this.router.navigate(['not-found']);
					}
				})
	}

	deleteVehicle(id: number) {
		this.service.deleteVehicle(id)
			.subscribe(vehicle => {
				if(vehicle) {
					this.router.navigate(['/']);
				}
			},
			(error: AppError) => {
				if(error instanceof NotFoundError)
					this.router.navigate(['/not-found']);
			});
	}
}
