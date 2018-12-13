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
import { AuthGuardService, AuthGuardServiceKlant, AuthGuardServiceHandelaar, AuthGuardServiceAdmin } from './user/auth-guard.service';
import { RegisterMerchantComponent } from './user/register-merchant/register-merchant.component';
import { LunchMerchantComponent } from './merchant/lunch-merchant/lunch-merchant.component';
import { AddLunchComponent } from './merchant/add-lunch/add-lunch.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { EditLunchComponent } from './merchant/edit-lunch/edit-lunch.component';
import { AdminPanelComponent } from './admin-panel/admin-panel/admin-panel.component';
import { AdminMerchantsComponent } from './admin-panel/admin-merchants/admin-merchants.component';
import { AdminReservationsComponent } from './admin-panel/admin-reservations/admin-reservations.component';
import { AdminLunchesComponent } from './admin-panel/admin-lunches/admin-lunches.component';
import { AdminMerchantDetailComponent } from './admin-panel/admin-merchant-detail/admin-merchant-detail.component';
import { ReservationsComponent } from './merchant/reservations/reservations.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UserPanelComponent } from './user/user-panel/user-panel.component';

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
    AdminLunchesComponent,
    AdminMerchantDetailComponent,
    ReservationsComponent,
    PageNotFoundComponent,
    UserPanelComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AngularFontAwesomeModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'details/:id', component:LunchDetailComponent},
      { path: 'register', component:RegisterComponent},
      { path: 'login', component:LoginComponent},
      { path: 'logout', component:LogoutComponent},
      { path: 'merchantregister', component:RegisterMerchantComponent},
      { path: 'merchant/lunch', canActivate: [ AuthGuardServiceHandelaar ], component:LunchMerchantComponent},
      { path: 'merchant/addlunch', canActivate: [ AuthGuardServiceHandelaar ], component:AddLunchComponent},
      { path: 'landingpage', component:LandingpageComponent},                
      { path: 'merchant/editlunch/:id', canActivate: [ AuthGuardServiceHandelaar ], component:EditLunchComponent},
      { path: 'admin', canActivate: [ AuthGuardServiceAdmin ], component:AdminPanelComponent},
      { path: 'admin/merchants', canActivate: [ AuthGuardServiceAdmin ], component:AdminMerchantsComponent},
      { path: 'admin/reservations', canActivate: [ AuthGuardServiceAdmin ], component:AdminReservationsComponent},
      { path: 'admin/lunches', canActivate: [ AuthGuardServiceAdmin ], component:AdminLunchesComponent},
      { path: 'admin/merchantdetail/:id', canActivate: [ AuthGuardServiceAdmin ], component:AdminMerchantDetailComponent},
      { path: 'merchant/reservations', canActivate: [ AuthGuardServiceHandelaar ], component:ReservationsComponent},
      { path: 'user/reservations', canActivate: [ AuthGuardServiceKlant ], component: UserPanelComponent},
      { path: '**', component:PageNotFoundComponent}
    ]),
    
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
