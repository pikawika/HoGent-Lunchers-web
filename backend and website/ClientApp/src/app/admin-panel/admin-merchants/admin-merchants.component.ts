import {AuthenticationService} from '../../user/authentication.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';
import { Handelaar } from 'src/models/handelaar';

@Component({
  selector: 'app-admin-merchants',
  templateUrl: './admin-merchants.component.html',
  styleUrls: ['./admin-merchants.component.css']
})
export class AdminMerchantsComponent implements OnInit {

  public _baseUrl;
  public _merchants;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: AdminDataService,
    private router: Router,
    private authService: AuthenticationService) {
      this._baseUrl = baseUrl;
    }

  ngOnInit() {
    this.dataService.merchants.subscribe(merchants => {
      this._merchants = merchants;
    });
  }

  get merchants() {
    return this._merchants;
  }

  removeMerchant(merchant: Handelaar) {
    const data: any = new FormData();
    data.append("handelaarId", merchant.handelaarId);
    this.dataService.removeMerchant(data).subscribe(receivedData => {
      if (receivedData["status"] == 200) {
        for (var i = this._merchants.length - 1; i >= 0; i--) {
          if (this._merchants[i].handelaarId === merchant.handelaarId) {
            this._merchants.splice(i, 1);
          }
        }
      }
    });
  }

}
