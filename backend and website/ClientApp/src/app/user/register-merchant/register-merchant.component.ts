import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register-merchant',
  templateUrl: './register-merchant.component.html',
  styleUrls: ['./register-merchant.component.css']
})
export class RegisterMerchantComponent implements OnInit {

  public merchant: FormGroup;
  public errorMsg: string;

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
      ]
    });
  }

  onSubmit() {
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
      this.merchant.value.city)
      .subscribe(
        val => {
          if (val) {
            this.router.navigate(['/']);
          }
        },
        (error: HttpErrorResponse) => {
          this.errorMsg = error.error.error;
        }
      );
  }

}
