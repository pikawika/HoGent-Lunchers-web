import { Component, OnInit, Inject, inject } from '@angular/core';
import { AuthenticationService } from 'src/app/user/authentication.service';
import { Lunch } from 'src/models/lunch';
import { LunchMerchantDataService } from './lunch-merchant-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lunch-merchant',
  templateUrl: './lunch-merchant.component.html',
  styleUrls: ['./lunch-merchant.component.css']
})
export class LunchMerchantComponent implements OnInit {

  public _handelaar; //public voor html
  public _baseUrl;
  public _lunches;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: LunchMerchantDataService,
    private router: Router) {
      this._baseUrl = baseUrl;
    }

  ngOnInit() {
    this.dataService.getMerchantById(9).subscribe(handelaar => {
      this._handelaar = handelaar;
      this._lunches = handelaar.lunches;
    });
  }

  get handelaar(){
    return this._handelaar;
  }

  get lunches(){
    return this._lunches;
  }

  addLunch(){
    this.router.navigate(['/merchant/addlunch']);
  }

  

  showDetails(lunch:Lunch){
    //ipv id door te geven, en in /details weer op te halen (extra call), complexe data structuur doorgeven (lunch in json formaat)
    this.router.navigate(['/details', lunch.lunchId]);
  }

}
