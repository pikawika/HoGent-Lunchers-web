import {AuthenticationService} from '../../user/authentication.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AdminDataService } from '../admin-data.service';

@Component({
  selector: 'app-admin-lunches',
  templateUrl: './admin-lunches.component.html',
  styleUrls: ['./admin-lunches.component.css']
})
export class AdminLunchesComponent implements OnInit {

  public _baseUrl;
  public _lunches;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public dataService: AdminDataService,
    private router: Router,
    private authService: AuthenticationService) {
      this._baseUrl = baseUrl;
    }

  ngOnInit() {
    this.dataService.lunches.subscribe(lunches => {
      this._lunches = lunches;
    });
  }

  get lunches() {
    return this._lunches;
  }

}
