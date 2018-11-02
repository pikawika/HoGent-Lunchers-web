import { Component, Inject } from '@angular/core';
import { HomeDataService } from './home-data.service';
import { Router } from '@angular/router';

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

  showDetails(id){
    this.router.navigate(['/details', id]);
  }
}
