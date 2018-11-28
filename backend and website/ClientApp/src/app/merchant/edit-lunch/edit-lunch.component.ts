import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailDataService } from 'src/app/lunch-detail/detail-data.service';
import { Lunch } from 'src/models/lunch';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MerchantDataService } from '../merchant-data.service';

@Component({
  selector: 'app-edit-lunch',
  templateUrl: './edit-lunch.component.html',
  styleUrls: ['./edit-lunch.component.css']
})
export class EditLunchComponent implements OnInit {

  public _lunch:Lunch;
  public _baseUrl;
  public lunch: FormGroup;
  public errorMsg: string;
  filesToUpload: Array<File> = [];

  constructor(
    private route: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private merchantService : MerchantDataService,
    private router: Router)
    {
      this._baseUrl = baseUrl;
     }

  ngOnInit() {


    this.lunch = this.fb.group({
      name: [
        '',
      ],
      price: [
        ''
      ],
      description: [
        '',
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
      // ],
      // tags: [
      //   '',
      // ],
      // afb: [
      //   '',
      // ]
    });
  }

  formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

onSubmit() {
  const data: any = new FormData();
  const files: File[] = this.filesToUpload;

  if(files.length == 0){
  }else{
    if(files.length > 1) {
      for(var x = 0; x < files.length; x++) {
          data.append("afbeeldingen", files[x]);    
      }
    } else {
      data.append("afbeeldingen", files);   
    }
  }

  data.append("naam", (<HTMLInputElement>document.getElementById('name')).value);
  data.append("beschrijving", (<HTMLInputElement>document.getElementById('description')).value);
  data.append("beginDatum", (<HTMLInputElement>document.getElementById('startdate')).value);
  data.append("eindDatum", (<HTMLInputElement>document.getElementById('enddate')).value);
  data.append("prijs", Number((<HTMLInputElement>document.getElementById('price')).value));
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

  this.merchantService.editLunch(this._lunch.lunchId,
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

  ngAfterViewInit(){
    this.merchantService.getLunchById(this.route.snapshot.paramMap.get('id')).subscribe(lunch => {
      this._lunch = lunch;
      var price = (<HTMLInputElement>document.getElementById('price'));
      price.value = this._lunch.prijs + "";

      (<HTMLInputElement>document.getElementById('name')).value = this._lunch.naam;
      (<HTMLInputElement>document.getElementById('description')).value = this._lunch.beschrijving;
      (<HTMLInputElement>document.getElementById('startdate')).value = this.formatDate(this._lunch.beginDatum);
      (<HTMLInputElement>document.getElementById('enddate')).value = this.formatDate(this._lunch.eindDatum);
    });
    
  }

  fileChange(event) {
    this.filesToUpload = event.target.files;
  }


}
