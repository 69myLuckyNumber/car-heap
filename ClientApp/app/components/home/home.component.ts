import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { AppError } from '../../common/errors/app-error';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
	vehicles: IVehicle[];

	constructor(private vehicleService: VehicleService) { }

	ngOnInit() {
		this.vehicleService.getVehicles()
			.subscribe(vehicles => this.vehicles = vehicles);
	}

}
