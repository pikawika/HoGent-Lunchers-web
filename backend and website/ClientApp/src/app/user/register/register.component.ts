import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormControl } from '@angular/forms';
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
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public user: FormGroup;
  public errorMsg: string;

  get passwordControl(): FormControl {
    return <FormControl>this.user.get('passwordGroup').get('password');
  }

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.user = this.fb.group({
      username: [
        '',
        [Validators.required, Validators.minLength(4)]
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
        [Validators.required, Validators.minLength(4)]
      ],
      achternaam: [
        '',
        [Validators.required, Validators.minLength(4)]
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
    this.authenticationService.registerKlant(this.user.value.username, this.passwordControl.value, this.user.value.tel, this.user.value.email,this.user.value.voornaam,this.user.value.achternaam)
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
