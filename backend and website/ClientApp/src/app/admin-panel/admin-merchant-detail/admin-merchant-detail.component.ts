import { Component, OnInit, Inject } from '@angular/core';
import { Handelaar } from 'src/models/handelaar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';
import { Observable, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-admin-merchant-detail',
  templateUrl: './admin-merchant-detail.component.html',
  styleUrls: ['./admin-merchant-detail.component.css']
})
export class AdminMerchantDetailComponent implements OnInit {

  public _merchant: BehaviorSubject<Handelaar>;
  public _baseUrl;
  public merchant: FormGroup;
  public geactiveerd: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(true);

  constructor(
    private route: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private adminService: AdminDataService,
    private router: Router) {
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    this.adminService.getMerchantById(this.route.snapshot.paramMap.get('id')).subscribe(handelaar => {
      this._merchant = new BehaviorSubject<Handelaar>(handelaar); 
      this.geactiveerd = new BehaviorSubject<Boolean>(handelaar.login.geactiveerd);
    });

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
      ],
      message: [
        ''
      ]
    });
  }

  get mGeactiveerd(): Boolean {
    return this.geactiveerd.value;
  }

  keurHandelaarGoed() {
    const data: any = new FormData();
    data.append("handelaarId", this._merchant.value.handelaarId);
    this.adminService.keurHandelaarGoed(data).subscribe(receivedData => {
      if (receivedData["status"] == 200) {
        this.router.navigate(['/admin/merchants']);
      }
    });
  }

  keurHandelaarAf() {
    const data: any = new FormData();
    data.append("handelaarId", this._merchant.value.handelaarId);
    this.adminService.keurHandelaarAf(data).subscribe(receivedData => {
      if (receivedData["status"] == 200) {
        this.router.navigate(['/admin/merchants']);
      }
    });
  }

  ngAfterViewInit() {
    this.adminService.getMerchantById(this.route.snapshot.paramMap.get('id')).subscribe(handelaar => {
      this._merchant = new BehaviorSubject<Handelaar>(handelaar);

      (<HTMLInputElement>document.getElementById('naam')).value = this._merchant.value.naam;
      (<HTMLInputElement>document.getElementById('voornaam')).value = this._merchant.value.voornaam;
      (<HTMLInputElement>document.getElementById('achternaam')).value = this._merchant.value.achternaam;
      (<HTMLInputElement>document.getElementById('email')).value = this._merchant.value.email;
      (<HTMLInputElement>document.getElementById('telefoonnummer')).value = this._merchant.value.telefoonnummer;
      (<HTMLInputElement>document.getElementById('website')).value = this._merchant.value.website;
    });
  }
}
