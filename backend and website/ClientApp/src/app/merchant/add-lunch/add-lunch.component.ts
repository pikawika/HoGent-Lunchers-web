import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MerchantDataService } from '../merchant-data.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-lunch',
  templateUrl: './add-lunch.component.html',
  styleUrls: ['./add-lunch.component.css']
})
export class AddLunchComponent implements OnInit {

  public lunch: FormGroup;
  public errorMsg: string;
  filesToUpload: Array<File> = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private merchantService : MerchantDataService
    ) { }

  ngOnInit() {
    this.lunch = this.fb.group({
      name: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
      price: [
        '',
        [Validators.required]
      ],
      description: [
        '',
        [Validators.required, Validators.minLength(10)]
      ],
      startdate: [
        '',
      ],
      enddate: [
        '',
      ]
      // ,
      // ingredienten: [
      //   '',
      //   [Validators.required]
      // ],
      // tags: [
      //   '',
      //   [Validators.required]
      // ],
      // afb: [
      //   '',
      //   [Validators.required]
      // ]
    });
  }

  onSubmit() {
    const data: any = new FormData();
    const files: File[] = this.filesToUpload;
    

    if(files.length > 1) {
      for(var x = 0; x < files.length; x++) {
          data.append('afbeeldingen', files[x]);    
      }
  } else {
      data.append('afbeeldingen', files[0]);   
  }
      data.append("naam", this.lunch.value.name);
      data.append("prijs", Number(this.lunch.value.price));
      data.append("beschrijving", this.lunch.value.description);
      data.append("beginDatum", (<HTMLInputElement>document.getElementById('startdate')).value);
      data.append("eindDatum", (<HTMLInputElement>document.getElementById('enddate')).value);
      data.append("ingredienten", [
        {
           "naam": "Ui"
        },
        {
           "naam": "Varkensvlees"
        }
      ]);
      data.append("tags", [
        {
           "naam": "Hamburger",
           "kleur": "FF6A6A"
        },
        {
           "naam": "Varkensvlees",
           "kleur": "FF6A6A"
        }
      ]);

    this.merchantService.addLunch(
      data)
      .subscribe(
        val => {
          if (val) {
            this.router.navigate(['/merchant/lunch']);
          }
        },
        (error: HttpErrorResponse) => {
          this.errorMsg = error.error.error;
        }
      );
  }


  fileChange(event) {
      this.filesToUpload = event.target.files;
    }

    ngAfterViewInit(){
      var startdate = (<HTMLInputElement>document.getElementById('startdate'));
      var enddate = (<HTMLInputElement>document.getElementById('enddate'));
      startdate.value = new Date().toISOString().slice(0,10);
      enddate.value = new Date().toISOString().slice(0,10);
    }
}


