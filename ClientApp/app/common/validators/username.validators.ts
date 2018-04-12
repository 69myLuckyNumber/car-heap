import { AbstractControl, ValidationErrors, ValidatorFn, AsyncValidatorFn } from "@angular/forms";
import { AccountService } from "../../services/account.service";
import { Observable } from "rxjs/Observable";
import { AppError } from "../errors/app-error";
import { NotFoundError } from "../errors/not-found-error";

export class UsernameValidators {
    static cannotContainSpace(control: AbstractControl): ValidationErrors | null {
        if((control.value as string).indexOf(" ") >= 0) {
            return { cannotContainSpace: true };
        }
        return null;
    }
    
    static shouldBeUnique(authService: AccountService) : AsyncValidatorFn {
        return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
            return new Promise((resolve, reject)=> {
                if((control.value as string) === '')
                    resolve(null);
                else {
                    authService.getUser(control.value)
                        .subscribe(
                            res => resolve({shouldBeUnique: true}),
                            (error: AppError) => {
                                if(error instanceof NotFoundError) {
                                    resolve(null);
                                } else {
                                    throw error;
                                }
                            });
                }
                
            });
        }
    }
}