import { Component, OnInit, Inject } from '@angular/core';
import { Handelaar } from 'src/models/handelaar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';

@Component({
  selector: 'app-admin-merchant-detail',
  templateUrl: './admin-merchant-detail.component.html',
  styleUrls: ['./admin-merchant-detail.component.css']
})
export class AdminMerchantDetailComponent implements OnInit {

  public _merchant: Handelaar;
  public _baseUrl;
  public merchant: FormGroup;

  constructor(
    private route: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private adminService: AdminDataService,
    private router: Router) {
    this._baseUrl = baseUrl;

  }

  ngOnInit() {
    this.merchant = this.fb.group({
      naam: [
        '',
      ],
      voornaam: [
        ''
      ],
      achternaam: [
        '',
      ],
      email: [
        '',
      ],
      telefoonnummer: [
        '',
      ],
      website: [
        ''
      ]
    });
  }

  ngAfterViewInit() {
    this.adminService.getMerchantById(this.route.snapshot.paramMap.get('id')).subscribe(handelaar => {
      this._merchant = handelaar;

      (<HTMLInputElement>document.getElementById('naam')).value = this._merchant.naam;
      (<HTMLInputElement>document.getElementById('voornaam')).value = this._merchant.voornaam;
      (<HTMLInputElement>document.getElementById('achternaam')).value = this._merchant.achternaam;
      (<HTMLInputElement>document.getElementById('email')).value = this._merchant.email;
      (<HTMLInputElement>document.getElementById('telefoonnummer')).value = this._merchant.telefoonnummer;
      (<HTMLInputElement>document.getElementById('website')).value = this._merchant.website;
    });
  }
}
