import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LunchDetailComponent } from './lunch-detail/lunch-detail.component';
import { LunchComponent } from './lunch/lunch.component';
import { RegisterComponent } from './user/register/register.component';
import { httpInterceptorProviders } from './http-interceptors';
import { LoginComponent } from './user/login/login.component';
import { LogoutComponent } from './user/logout/logout.component';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthGuardService } from './user/auth-guard.service';
import { RegisterMerchantComponent } from './user/register-merchant/register-merchant.component';
import { LunchMerchantComponent } from './merchant/lunch-merchant/lunch-merchant.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LunchDetailComponent,
    LunchComponent,
    RegisterComponent,
    LoginComponent,
    LogoutComponent,
    RegisterMerchantComponent,
    LunchMerchantComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'details/:id',canActivate: [ AuthGuardService ], component:LunchDetailComponent},
      { path: 'register', component:RegisterComponent},
      { path: 'login', component:LoginComponent},
      { path: 'logout', component:LogoutComponent},
      { path: 'merchantregister', component:RegisterMerchantComponent},
      { path: 'merchant/lunch', component:LunchMerchantComponent},
    ]),
    
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
