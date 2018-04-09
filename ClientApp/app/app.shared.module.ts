import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import { AuthService } from './services/auth.service';
import { UsernameValidators } from './common/validators/username.validators';
import { AppErrorHandler } from './common/errors/app-error-handler';

@NgModule({
    declarations: [
        AppComponent,
        SignupFormComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            // { path: '', redirectTo: 'home', pathMatch: 'full' },
            // { path: 'home', component: HomeComponent },
            // { path: 'counter', component: CounterComponent },
            // { path: 'fetch-data', component: FetchDataComponent },
            // { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AuthService,
        {provide: ErrorHandler, useClass: AppErrorHandler}
    ]
})
export class AppModuleShared {
}
