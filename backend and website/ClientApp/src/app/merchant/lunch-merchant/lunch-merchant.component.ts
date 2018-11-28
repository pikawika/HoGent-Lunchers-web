import { Component, OnInit, Inject, inject } from '@angular/core';
import { AuthenticationService } from 'src/app/user/authentication.service';
import { Router } from '@angular/router';
import { MerchantDataService } from '../merchant-data.service';
import { Lunch } from 'src/models/lunch';

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
    public dataService: MerchantDataService,
    private router: Router,
    private authService: AuthenticationService) {
      this._baseUrl = baseUrl;
    }

  ngOnInit() {
    this.dataService.getMerchantById(this.authService.id$.value).subscribe(handelaar => {
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

  editLunch(lunch: Lunch){
    this.router.navigate(['merchant/editlunch', lunch.lunchId])
  }

  removeLunch(lunch:Lunch){
    this.dataService.removeLunch(lunch.lunchId);
  }
}
