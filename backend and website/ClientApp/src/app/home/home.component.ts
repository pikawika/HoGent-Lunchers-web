import { Component, Inject } from '@angular/core';
import { HomeDataService } from './home-data.service';
import { Router } from '@angular/router';
import { Lunch } from '../../models/lunch';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public _lunches; //public voor html
  public _baseUrl;

  constructor(public dataService: HomeDataService,@Inject('BASE_URL') baseUrl: string, private router: Router){
    this._baseUrl = baseUrl;
  }

  ngOnInit(){
    this.dataService.lunches.subscribe(lunches => {
      this._lunches = lunches;
    });
  }

  get lunches(){
    return this._lunches;
  }

  showDetails(lunch:Lunch){
    //ipv id door te geven, en in /details weer op te halen (extra call), complexe data structuur doorgeven (lunch in json formaat)
    this.router.navigate(['/details', lunch.lunchId]);
  }
}
