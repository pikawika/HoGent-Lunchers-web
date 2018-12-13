import { Component, OnInit, Inject } from '@angular/core';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {

  public _baseUrl;
  public _reservaties;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: AuthenticationService
  ) 
  { 
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    this.dataService.reservations.subscribe(reservaties => {
      this._reservaties = reservaties;
    });
  }

  get reservations() {
    return this._reservaties;
  }

}
