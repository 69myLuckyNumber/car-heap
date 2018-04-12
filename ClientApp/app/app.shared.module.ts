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

@NgModule({
    declarations: [
        AppComponent,
        SignupFormComponent,
        HomeComponent,
        NotFoundComponent,
        NavbarComponent,
        LoginFormComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            {  path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home' , component: HomeComponent },
            { path: 'register', component: SignupFormComponent },
            { path: 'login', component: LoginFormComponent },
            { path: '**', component: NotFoundComponent}
        ])
    ],
    providers: [
        AccountService,
        {provide: ErrorHandler, useClass: AppErrorHandler},
        AuthGuard
    ]
})
export class AppModuleShared {
}
