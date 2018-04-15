import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import { AccountService } from './services/account.service';
import { UsernameValidators } from './common/validators/username.validators';
import { AppErrorHandler } from './common/errors/app-error-handler';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { AuthGuard } from './services/auth-guard.service';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';

import { AUTH_PROVIDERS } from "angular2-jwt/angular2-jwt";
import { VehicleViewComponent } from './components/vehicle-view/vehicle-view.component';
import { UserViewComponent } from './components/user-view/user-view.component';
import { PhotoService } from './services/photo.service';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';

@NgModule({
    declarations: [
        AppComponent,
        SignupFormComponent,
        HomeComponent,
        NotFoundComponent,
        NavbarComponent,
        LoginFormComponent,
        VehicleFormComponent,
        VehicleViewComponent,
        UserViewComponent,
        VehicleListComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: '/home', pathMatch: 'full' },
            { path: 'user/:username', component: UserViewComponent },
            { path: 'vehicle/new', component: VehicleFormComponent, canActivate: [AuthGuard] },
            { path: 'vehicle/:id', component: VehicleViewComponent },
            { path: 'vehicles/:username', component: VehicleListComponent },
            { path: 'home' , component: HomeComponent },
            { path: 'register', component: SignupFormComponent },
            { path: 'login', component: LoginFormComponent },
            { path: '**', component: NotFoundComponent}
        ])
    ],
    providers: [
        AccountService,
        {provide: ErrorHandler, useClass: AppErrorHandler},
        AuthGuard,
        VehicleService,
        AUTH_PROVIDERS,
        PhotoService
    ]
})
export class AppModuleShared {
}
