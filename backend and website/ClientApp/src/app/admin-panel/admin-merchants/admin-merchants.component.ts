import {AuthenticationService} from '../../user/authentication.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';

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

}
