import { Component, OnInit, Inject } from '@angular/core';
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

  get handelaar() {
    return this._handelaar;
  }

  get lunches() {
    return this._lunches;
  }

  addLunch() {
    this.router.navigate(['/merchant/addlunch']);
  }

  editLunch(lunch: Lunch) {
    this.router.navigate(['merchant/editlunch', lunch.lunchId])
  }

  removeLunch(lunch: Lunch) {
    const data: any = new FormData();
    data.append("naam", lunch.naam);
    data.append("beschrijving", lunch.beschrijving);
    data.append("beginDatum", lunch.beginDatum);
    data.append("eindDatum", lunch.eindDatum);
    data.append("prijs", lunch.prijs);
    data.append("ingredienten",
      [
        {
          "naam": "Ui"
        },
        {
          "naam": "Varkensvlees"
        }
      ]);
    data.append("tags",
      [
        {
          "naam": "Hamburger",
          "kleur": "FF6A6A"
        },
        {
          "naam": "Varkensvlees",
          "kleur": "FF6A6A"
        }
      ]);
    this.dataService.removeLunch(lunch.lunchId, data).subscribe(receivedData => {
      if (receivedData["status"] == 200) {
        for (var i = this._lunches.length - 1; i >= 0; i--) {
          if (this._lunches[i].lunchId === lunch.lunchId) {
            this._lunches.splice(i, 1);
          }
        }
      }
    });
  }
}
