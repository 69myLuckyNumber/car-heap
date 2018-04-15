import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IUser } from '../../models/user.model';
import { AccountService } from '../../services/account.service';
import { AppError } from '../../common/errors/app-error';
import { NotFoundError } from '../../common/errors/not-found-error';

@Component({
	selector: 'app-user-view',
	templateUrl: './user-view.component.html',
	styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {
	userName: string;
	user: IUser;

	constructor(private route: ActivatedRoute,private router: Router, private service: AccountService) {
		route.params.subscribe(p => {
			this.userName = p['username'];
			if(!this.userName) {
				router.navigate(['/']);
				return; 
			}
		})
	 }

	ngOnInit() {
		this.service.getUser(this.userName)
			.subscribe(user => this.user = user,
				(error: AppError) => {
				if(error instanceof NotFoundError) {
					this.router.navigate(['not-found']);
				}
				});
	}

}
