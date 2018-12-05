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
import { AddLunchComponent } from './merchant/add-lunch/add-lunch.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { EditLunchComponent } from './merchant/edit-lunch/edit-lunch.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { AdminMerchantsComponent } from './admin-panel/admin-merchants/admin-merchants.component';
import { AdminReservationsComponent } from './admin-panel/admin-reservations/admin-reservations.component';
import { AdminLunchesComponent } from './admin-panel/admin-lunches/admin-lunches.component';


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
    LunchMerchantComponent,
    AddLunchComponent,
    LandingpageComponent,
    EditLunchComponent,
    AdminPanelComponent,
    AdminMerchantsComponent,
    AdminReservationsComponent,
    AdminLunchesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AngularFontAwesomeModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'details/:id',canActivate: [ AuthGuardService ], component:LunchDetailComponent},
      { path: 'register', component:RegisterComponent},
      { path: 'login', component:LoginComponent},
      { path: 'logout', component:LogoutComponent},
      { path: 'merchantregister', component:RegisterMerchantComponent},
      { path: 'merchant/lunch', canActivate: [ AuthGuardService ], component:LunchMerchantComponent},
      { path: 'merchant/addlunch', canActivate: [ AuthGuardService ], component:AddLunchComponent},
      { path: 'landingpage', component:LandingpageComponent},
      { path: 'merchant/editlunch/:id', canActivate: [ AuthGuardService ], component:EditLunchComponent},
      { path: 'admin', canActivate: [ AuthGuardService ], component:AdminPanelComponent},
      { path: 'admin/merchants', canActivate: [ AuthGuardService ], component:AdminMerchantsComponent},
      { path: 'admin/reservations', canActivate: [ AuthGuardService ], component:AdminReservationsComponent},
      { path: 'admin/lunches', canActivate: [ AuthGuardService ], component:AdminLunchesComponent},
    ]),
    
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
