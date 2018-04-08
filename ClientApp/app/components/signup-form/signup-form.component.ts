import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {
  form = new FormGroup({
    firstName: new FormControl('',[
      Validators.required,
      Validators.maxLength(32)
    ]),
    lastName: new FormControl('',[
      Validators.maxLength(32)
    ]),
    username: new FormControl('',[
      Validators.minLength(4),
      Validators.maxLength(32)
    ]),
    email: new FormControl('',[
      Validators.email
    ]),
    password: new FormControl('',[
      Validators.minLength(6),
      Validators.maxLength(32),
      Validators.required
    ]),
    passwordAgain: new FormControl('',[
      Validators.minLength(6),
      Validators.maxLength(32),
      Validators.required
    ]),
    phone: new FormControl('',[
      Validators.pattern("^[0-9]{12}$")
    ])
  });
  
  get usernameControl(){
    return this.form.get('username');
  }
  get firstNameControl(){
    return this.form.get('firstName');
  }
  get lastNameControl(){
    return this.form.get('lastName');
  }
  get emailControl(){
    return this.form.get('email');
  }
  get passwordControl(){
    return this.form.get('password');
  }
  get phoneControl(){
    return this.form.get('phone');
  }
}
