import { Component, OnInit, Inject } from '@angular/core';
import { MerchantDataService } from '../merchant-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {

  public _baseUrl;
  public _reservaties;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: MerchantDataService,
    private router: Router,
  ) { 
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
