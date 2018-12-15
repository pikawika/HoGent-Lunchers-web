import { Component, Inject } from '@angular/core';
import { HomeDataService } from './home-data.service';
import { Router } from '@angular/router';
import { Lunch } from '../../models/lunch';
import { Tag } from 'src/models/Tag';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  private _all_lunches;
  public _lunches; //public voor html
  public _baseUrl;

  constructor(public dataService: HomeDataService,@Inject('BASE_URL') baseUrl: string, private router: Router){
    this._baseUrl = baseUrl;
  }

  ngOnInit(){
    this.dataService.lunches.subscribe(lunches => {
      this._lunches = lunches;
      this._all_lunches = lunches;
    });
  }

  get lunches(){
    return this._lunches;
  }

  showDetails(lunch:Lunch){
    //ipv id door te geven, en in /details weer op te halen (extra call), complexe data structuur doorgeven (lunch in json formaat)
    this.router.navigate(['/details', lunch.lunchId]);
  }

  onSearchChange(searchValue : string ) {  
    console.log(searchValue);

    this._lunches = []

    this._all_lunches.forEach(lunch => {

      let contain_name = lunch.naam.toLowerCase().indexOf(searchValue) >= 0;
      let contain_tag = false;
      let contain_ingredient = false;
      
      lunch.tags.forEach(tag => {
        if(tag.tag.naam.toLowerCase().indexOf(searchValue) >= 0){
          contain_tag = true;
        }
      });

      lunch.ingredienten.forEach(ingredient => {
        if(ingredient.ingredient.naam.toLowerCase().indexOf(searchValue) >= 0){
          contain_ingredient = true;
        }
      });

      if(contain_name||contain_tag||contain_ingredient){
        this._lunches.push(lunch);        
      }
    });
  }

  scroll(el) {
    el.scrollIntoView({behavior:"smooth", block: 'start'});
  }
}
