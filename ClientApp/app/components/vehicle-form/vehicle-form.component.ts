import * as _ from 'underscore';

import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';

import { Component, OnInit } from '@angular/core';

import 'rxjs/add/Observable/forkJoin';
import { ISaveVehicle, IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { AppError } from '../../common/errors/app-error';
import { BadRequestError } from '../../common/errors/bad-request-error';
import { AccountService } from '../../services/account.service';
import { FormGroup } from '@angular/forms';

@Component({
	selector: 'app-vehicle-form',
	templateUrl: './vehicle-form.component.html',
	styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
	private isValid: boolean = true;

	makes: any[];
	models: any[];
	features: any[];
	vehicle: ISaveVehicle = {
		id: 0,
		name: '',
		modelId: 0,
		makeId: 0,
		isRegistered: false,
		identityId: '',
		features: []
	};

	constructor(
		private route: ActivatedRoute,
		private router: Router,
		private vehicleService: VehicleService,
		private accountService: AccountService) {

		route.params.subscribe(p => {
			this.vehicle.id = +p['id'] || 0;
		});
	}

	ngOnInit() {
		var sources = [
			this.vehicleService.getMakes(),
			this.vehicleService.getFeatures(),
		];
		if(this.vehicle.id)
			sources.push(this.vehicleService.getVehicle(this.vehicle.id));
		
		Observable.forkJoin(sources).subscribe(data => {
			this.makes = data[0];
			this.features = data[1];

			if (this.vehicle.id) {
				this.setVehicle(data[2]);
				this.populateModels();
			  }
		});
	}

	private setVehicle(v: IVehicle) {
		this.vehicle.id = v.id;
		this.vehicle.name = v.name;
		this.vehicle.makeId = v.make.id;
		this.vehicle.modelId = v.model.id;
		this.vehicle.isRegistered = v.isRegistered;
		this.vehicle.features = _.pluck(v.features, 'id');
	  } 

	onMakeChange() {
		this.populateModels();

		delete this.vehicle.modelId;
	}

	private populateModels() {
		var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
		this.models = selectedMake ? selectedMake.models : [];
	}

	onFeatureToggle(featureId: number, $event: { target: { checked: any; }; }) {
		if ($event.target.checked)
			this.vehicle.features.push(featureId);
		else {
			var index = this.vehicle.features.indexOf(featureId);
			this.vehicle.features.splice(index, 1);
		}
	}

	submit(form: FormGroup) {
		if(form.valid) {
			var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
			this.vehicle.identityId = this.accountService.currentUser.id;
			result$.subscribe(vehicle => {
					this.router.navigate(['/vehicle/', vehicle.id]);
				}, (error: AppError) => {
					console.log(error);
					if(error instanceof BadRequestError) {
						this.router.navigate(['/']);
					} else {
						this.router.navigate(['/']);
					}
				});
		} else {
			this.isValid = false;
		}
	}
}