import { ValidatorFn, AbstractControl, ValidationErrors, FormGroup } from "@angular/forms";

export class PasswordValidators {

    static matchPassword(control: AbstractControl): ValidationErrors | null {

        const formGroup = control.parent;
        if (formGroup) {
            const passwordControl = formGroup.get('password'); // to get value in input tag
            const confirmPasswordControl = formGroup.get('passwordAgain'); // to get value in input tag

            if (passwordControl && confirmPasswordControl) {
                const password = passwordControl.value;
                const confirmPassword = confirmPasswordControl.value;
                if (password !== confirmPassword) {
                    return { mismatch: true };
                } else {
                    if(passwordControl.getError('minlength') || passwordControl.getError('maxlength')) {
                        return null;
                    }
                    passwordControl.setErrors(null);
                    confirmPasswordControl.setErrors(null);
                    
                    return null;
                }
            }
        }

        return null;
    }
}