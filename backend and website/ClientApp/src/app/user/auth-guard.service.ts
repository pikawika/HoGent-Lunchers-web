import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.user$.getValue()) {
      return true;
    }
    
    // Retain the attempted URL for redirection
    this.authService.redirectUrl = state.url;
    this.router.navigate(['/login']);
    return false;
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceKlant {
  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if(this.authService.rol$.getValue().toLocaleLowerCase() == "klant"){
      return true;
    }else if (this.authService.rol$.getValue().toLocaleLowerCase() != null && this.authService.rol$.getValue().toLocaleLowerCase() != ""){
      this.router.navigate(['404']);
      return false;
    }
    
    if (this.authService.user$.getValue()) {
      return true;
    }
    
    
    // Retain the attempted URL for redirection
    this.authService.redirectUrl = state.url;
    this.router.navigate(['/login']);
    return false;
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceHandelaar {
  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if(this.authService.rol$.getValue().toLocaleLowerCase() == "handelaar"){
      return true;
    }else if (this.authService.rol$.getValue().toLocaleLowerCase() != null && this.authService.rol$.getValue().toLocaleLowerCase() != ""){
      this.router.navigate(['404']);
      return false;
    }
    
    if (this.authService.user$.getValue()) {
      return true;
    }
    
    
    // Retain the attempted URL for redirection
    this.authService.redirectUrl = state.url;
    this.router.navigate(['/login']);
    return false;
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceAdmin {
  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if(this.authService.rol$.getValue().toLocaleLowerCase() == "admin"){
      return true;
    }else if (this.authService.rol$.getValue().toLocaleLowerCase() != null && this.authService.rol$.getValue().toLocaleLowerCase() != ""){
      this.router.navigate(['404']);
      return false;
    }
    
    if (this.authService.user$.getValue()) {
      return true;
    }
    
    
    // Retain the attempted URL for redirection
    this.authService.redirectUrl = state.url;
    this.router.navigate(['/login']);
    return false;
  }
}

