import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailDataService } from './detail-data.service';
import { Lunch } from '../../models/lunch';
import { FormGroup, FormControl,ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-lunch-detail',
  templateUrl: './lunch-detail.component.html',
  styleUrls: ['./lunch-detail.component.css']
})
export class LunchDetailComponent implements OnInit {

  
  public _lunch:Lunch;
  public _baseUrl;
  public reservatie_message = "";
  public reserveerForm: FormGroup;

  constructor(private route: ActivatedRoute, private dataService:DetailDataService,@Inject('BASE_URL') baseUrl: string) { 
  }

  ngOnInit() {
    this.dataService.getLunchById(this.route.snapshot.paramMap.get('id')).subscribe(lunch => {
      this._lunch = lunch;
    });

    this.reserveerForm = new FormGroup({
      datum: new FormControl("",[
        Validators.required
      ]),
      uur: new FormControl("",[
        Validators.required
      ]),
      aantal: new FormControl("",[
        Validators.required
      ]),
      opmerkingen: new FormControl(),
  });
  }

  onSubmit(){
    if (this.reserveerForm.valid) {
      this.dataService.reserveer(
        this._lunch.lunchId,
        this.reserveerForm.value.datum,
        this.reserveerForm.value.uur,
        this.reserveerForm.value.aantal,
        this.reserveerForm.value.opmerkingen
      ).subscribe(data => {
        if(data.status == 200){
          this.reservatie_message = "Reservatie is geplaatst!";
        }
      });
    }
  }
  
}
