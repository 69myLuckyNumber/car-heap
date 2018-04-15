import * as _ from 'underscore';

import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';

import { Component, OnInit } from '@angular/core';

import 'rxjs/add/Observable/forkJoin';
import { ISaveVehicle } from '../../models/vehicle.model';
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
		this.makes = [], this.models = [], this.features = [];
	}

	ngOnInit() {
		var sources = [
			this.vehicleService.getMakes(),
			this.vehicleService.getFeatures(),
		];

		Observable.forkJoin(sources).subscribe(data => {
			this.makes = data[0];
			this.features = data[1];
		});
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
			this.vehicle.identityId = this.accountService.currentUser.id;
			this.vehicleService.create(this.vehicle)
				.subscribe(vehicle => {
					this.router.navigate(['/vehicle/', vehicle.id]);
				}, (error: AppError) => {
					if(error instanceof BadRequestError) {
						this.router.navigate(['/']);
					}
				});
		} else {
			this.isValid = false;
		}
	}
}