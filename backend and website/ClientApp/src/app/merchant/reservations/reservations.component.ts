import { Component, OnInit, Inject } from '@angular/core';
import { MerchantDataService } from '../merchant-data.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Reservatie } from 'src/models/reservatie';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {

  public errorMsg;
  public _baseUrl;
  public _reservaties;
  public _goedTeKeuren = [];
  public _goedGekeurd = [];

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

      this._reservaties.forEach(res => {
        if (res.status == "In afwachting") {
          this._goedTeKeuren.push(res);
        }
        if (res.status == "Goedgekeurd") {
          this._goedGekeurd.push(res);
        }
      });
    });
  }

  approved(reservatie: Reservatie) {
    this.dataService.approveReservation(reservatie.reservatieId).subscribe(
      receivedData => {
        console.log(receivedData.status)
        if (receivedData.status == 200) {
          for (var i = this._goedTeKeuren.length - 1; i >= 0; i--) {
            if (this._goedTeKeuren[i].reservatieId === reservatie.reservatieId) {
              this._goedTeKeuren.splice(i, 1);
              this._goedGekeurd.push(reservatie);
            }
          }
        }
      },
      (error: HttpErrorResponse) => {
        this.errorMsg = error.error.error;
      }
    );
  }

  declined(reservatie: Reservatie) {
    this.dataService.declineReservation(reservatie.reservatieId).subscribe(
      receivedData => {
        if (receivedData.status == 200) {
          for (var i = this._goedTeKeuren.length - 1; i >= 0; i--) {
            if (this._goedTeKeuren[i].reservatieId === reservatie.reservatieId) {
              this._goedTeKeuren.splice(i, 1);
            }
          }
        }
      },
      (error: HttpErrorResponse) => {
        this.errorMsg = error.error.error;
      }
    )
  }



  get reservations() {
    return this._reservaties;
  }

  get approvedReservations() {
    return this._goedGekeurd;
  }

  get waitingReservations() {
    return this._goedTeKeuren;
  }

}
