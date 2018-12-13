import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../user/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private authService: AuthenticationService) {}

  get currentUser(): Observable<string> {
    return this.authService.user$;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  get redirect(){
    if(this.authService.rol$.value == "handelaar"){
      return "/merchant/lunch"
    }else{
      if(this.authService.rol$.value == "admin"){
        return "/admin"
      }else{
        return "/user/reservations"
      }
    }
  }
}
