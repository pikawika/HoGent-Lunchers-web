import { Component, OnInit, Input, EventEmitter, Output, Inject } from '@angular/core';
import { Lunch } from '../../models/lunch';

@Component({
  selector: 'app-lunch',
  templateUrl: './lunch.component.html',
  styleUrls: ['./lunch.component.css']
})
export class LunchComponent implements OnInit {

  @Input() public lunch: Lunch;
  @Output() public details = new EventEmitter<Lunch>();
  
  public _baseUrl:string;

  constructor(@Inject('BASE_URL') baseUrl: string) { this._baseUrl = baseUrl; }

  ngOnInit() {
  }

  showDetails(){
    this.details.emit(this.lunch);
  }
}
