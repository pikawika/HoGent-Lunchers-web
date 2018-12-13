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
  public _approvedMerchants = [];
  public _approvableMerchtans = [];

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
      this._merchants.forEach(merchant => {
        if(merchant.login.geactiveerd){
          this._approvedMerchants.push(merchant);
        }else{
          this._approvableMerchtans.push(merchant);
        }
      });
    });
  }

  get merchants() {
    return this._merchants;
  }

  get approvedMerchants(){
    return this._approvedMerchants;
  }

  get approvableMerchants(){
    return this._approvableMerchtans;
  }

  goToMerchantDetail(merchant: Handelaar) {
    this.router.navigate(['admin/merchantdetail', merchant.handelaarId])
  }

  removeMerchant(merchant: Handelaar) {
    const data: any = new FormData();
    data.append("handelaarId", merchant.handelaarId);
    this.dataService.removeMerchant(data).subscribe(receivedData => {
      if (receivedData["status"] == 200) {
        for (var i = this._approvableMerchtans.length - 1; i >= 0; i--) {
          if (this._approvableMerchtans[i].handelaarId === merchant.handelaarId) {
            this._approvableMerchtans.splice(i, 1);
          }
        }
        for (var i = this._approvedMerchants.length - 1; i >= 0; i--) {
          if (this._approvedMerchants[i].handelaarId === merchant.handelaarId) {
            this._approvedMerchants.splice(i, 1);
          }
        }
      }
    });
  }

}
