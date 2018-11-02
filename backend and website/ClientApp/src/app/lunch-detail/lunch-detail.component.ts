import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailDataService } from './detail-data.service';
import { Lunch } from '../../models/lunch';

@Component({
  selector: 'app-lunch-detail',
  templateUrl: './lunch-detail.component.html',
  styleUrls: ['./lunch-detail.component.css']
})
export class LunchDetailComponent implements OnInit {

  public _lunch:Lunch;
  public _baseUrl;

  constructor(private route: ActivatedRoute, private dataService:DetailDataService,@Inject('BASE_URL') baseUrl: string) { 
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    this.dataService.getLunchById(this.route.snapshot.paramMap.get('id')).subscribe(lunch => {
      this._lunch = lunch;
    });
  }
  
}
