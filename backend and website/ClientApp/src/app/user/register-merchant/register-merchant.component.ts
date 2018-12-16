import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, FormControl } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

function comparePasswords(control: AbstractControl): { [key: string]: any } {
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');
 
  return password.value === confirmPassword.value
    ? null
    : { passwordsDiffer: true };
}

@Component({
  selector: 'app-register-merchant',
  templateUrl: './register-merchant.component.html',
  styleUrls: ['./register-merchant.component.css']
})
export class RegisterMerchantComponent implements OnInit {

  public merchant: FormGroup;
  public errorMsg: string;
  public succesMsg: string = "";

  get passwordControl(): FormControl {
    return <FormControl>this.merchant.get('passwordGroup').get('password');
  }

  constructor(
    private authenticationService: AuthenticationService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.merchant = this.fb.group({
      username: [
        '',
        [Validators.required]
      ],
      tel: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
      email: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
      voornaam: [
        '',
        [Validators.required]
      ],
      achternaam: [
        '',
        [Validators.required]
      ],
      merchant: [
        '',
        [Validators.required, Validators.minLength(2)]
      ],
      website: [
        '',
        [Validators.required]
      ],
      street: [
        '',
        [Validators.required]
      ],
      number: [
        '',
        [Validators.required]
      ],
      code: [
        '',
        [Validators.required]
      ],
      city: [
        '',
        [Validators.required]
      ],
      passwordGroup: this.fb.group(
        {
          password: ['', [Validators.required, Validators.minLength(4)]],
          confirmPassword: ['', Validators.required]
        },
        { validator: comparePasswords }
      )
    });
  }

  onSubmit() {
    console.log(this.merchant.value.password);
    this.authenticationService.registerMerchant
    (this.merchant.value.username, 
      this.merchant.value.tel, 
      this.merchant.value.email,
      this.merchant.value.voornaam,
      this.merchant.value.achternaam,
      this.merchant.value.merchant,
      this.merchant.value.website,
      this.merchant.value.street,
      this.merchant.value.number,
      this.merchant.value.code,
      this.merchant.value.city,
      this.passwordControl.value)
      .subscribe(
        val => {
          if (val) {
            this.succesMsg = "Uw registratie was succesvol";
          }
        },
        (error: HttpErrorResponse) => {
          this.errorMsg = error.error.error;
        }
      );
  }

}
