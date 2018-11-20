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
  public formData : FormData;

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
        [Validators.required]
      ],
      enddate: [
        '',
        [Validators.required]
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
    this.merchantService.registerMerchant(
      this.lunch.value.name, 
      this.lunch.value.price, 
      this.lunch.value.description,
      this.lunch.value.startdate,
      this.lunch.value.enddate,
      this.formData)
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
    let fileList: FileList = event.target.files;
    if(fileList.length > 0) {
        let file: File = fileList[0];
        this.formData = new FormData();
        this.formData.append('fiel', file, file.name);
        console.log(this.formData);
    }
}
}

