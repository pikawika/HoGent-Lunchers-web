import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogoutComponent } from './logout/logout.component';
import { RegisterMerchantComponent } from './register-merchant/register-merchant.component';
import { UserPanelComponent } from './user-panel/user-panel.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule, ReactiveFormsModule,
  ],
  declarations: [LoginComponent, RegisterComponent, LogoutComponent, RegisterMerchantComponent, UserPanelComponent]
})
export class UserModule { }
