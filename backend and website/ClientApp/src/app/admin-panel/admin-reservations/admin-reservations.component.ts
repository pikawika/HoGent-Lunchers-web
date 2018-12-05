import {AuthenticationService} from '../../user/authentication.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';


@Component({
  selector: 'app-admin-reservations',
  templateUrl: './admin-reservations.component.html',
  styleUrls: ['./admin-reservations.component.css']
})
export class AdminReservationsComponent implements OnInit {

  public _baseUrl;
  public _reservations;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: AdminDataService,
    private router: Router,
    private authService: AuthenticationService) {
      this._baseUrl = baseUrl;
    }

    ngOnInit() {
      this.dataService.reservations.subscribe(reservations => {
        this._reservations = reservations;
      });
    }

    get reservations() {
      return this._reservations
    }

}
