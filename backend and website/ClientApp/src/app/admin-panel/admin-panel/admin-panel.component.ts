import { Component, OnInit, Inject } from '@angular/core';
import { AdminDataService } from '../admin-data.service';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/user/authentication.service';
import { Aantal } from 'src/models/aantal';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  public _baseUrl;
  public _aantal: Aantal = new Aantal();

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: AdminDataService,
    private router: Router,
    private authService: AuthenticationService) {
      this._baseUrl = baseUrl;
    }

    ngOnInit() {
      this._aantal.aantalHandelaars = 0;
      this._aantal.aantalLunches = 0;
      this._aantal.aantalReservaties = 0;
      this.dataService.getAantallen().subscribe(aantal => {
        this._aantal = aantal;
      });
    }

    get aantalHandelaars() {
      return this._aantal.aantalHandelaars;
    }

    get aantalLunches() {
      return this._aantal.aantalLunches;
    }

    get aantalReservaties() {
      return this._aantal.aantalReservaties;
    }

}
