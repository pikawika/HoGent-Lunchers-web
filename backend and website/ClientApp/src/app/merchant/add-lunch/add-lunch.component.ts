import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MerchantDataService } from '../merchant-data.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Ingredient } from 'src/models/Ingredient';
import { Tag } from 'src/models/Tag';

@Component({
  selector: 'app-add-lunch',
  templateUrl: './add-lunch.component.html',
  styleUrls: ['./add-lunch.component.css']
})
export class AddLunchComponent implements OnInit {

  public lunch: FormGroup;
  public errorMsg: string;
  filesToUpload: Array<File> = [];
  public _ingredienten = [];
  public _tags = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private merchantService : MerchantDataService
    ) { }


    addIngredient(ingredient: string){
      let ing = new Ingredient();
      if(ingredient.length <= 20 && ingredient.length > 0){
        ing.Naam = ingredient;
        this._ingredienten.push(ing);
      }else{
        this.errorMsg = "Er is een fout opgetreden bij het toevoegen van een ingredient(max 20 tekens)"
      }
    }

    addTag(tag: string){
      let t = new Tag();
      if(tag.length <= 20 && tag.length > 0){
        t.Naam = tag;
        this._tags.push(t);
      }else{
        this.errorMsg = "Er is een fout opgetreden bij het toevoegen van een tag(max 20 tekens)";
      }
    }

    removeIng(ingredient){
      for(let i = 0; i < this._ingredienten.length;i++){
        if(this._ingredienten[i].Naam == ingredient.Naam){
          this._ingredienten.splice(i, 1);
          break;
        }
      }
    }

    removeTag(tag){
      for(let i = 0; i < this._tags.length;i++){
        if(this._tags[i].Naam == tag.Naam){
          this._tags.splice(i, 1);
          break;
        }
      }
    }

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
        ''
      ],
      enddate: [
        ''
      ],
      ingredient: [
        ''
      ],
      tag: [
        ''
      ]
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
      data.append("ingredienten", JSON.stringify(this._ingredienten));
      data.append("tags", JSON.stringify(this._tags));

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


