import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public user: FormGroup;
  public errorMsg: string;

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
      password: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
    });
  }

  onSubmit() {
    this.authenticationService.login(this.user.value.username,this.user.value.password)
      .subscribe(
        val => {
          if (val) {
            if (this.authenticationService.redirectUrl) {
              this.router.navigateByUrl(this.authenticationService.redirectUrl);
              this.authenticationService.redirectUrl = undefined;
            }else{
              this.router.navigate(['/']);
            }
          }
        },
        (error: HttpErrorResponse) => {
          this.errorMsg = error.error.error;          
        }
      );
  }

}
