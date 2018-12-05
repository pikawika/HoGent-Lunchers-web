import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailDataService } from 'src/app/lunch-detail/detail-data.service';
import { Lunch } from 'src/models/lunch';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MerchantDataService } from '../merchant-data.service';
import { Ingredient } from 'src/models/Ingredient';
import { Tag } from 'src/models/Tag';

@Component({
  selector: 'app-edit-lunch',
  templateUrl: './edit-lunch.component.html',
  styleUrls: ['./edit-lunch.component.css']
})
export class EditLunchComponent implements OnInit {

  public _lunch: Lunch;
  public _baseUrl;
  public lunch: FormGroup;
  public errorMsg: string;
  filesToUpload: Array<File> = [];
  public _ingredientObjects = [];
  public _tagObjects = [];
  public _ingredienten = [];
  public _tags = [];

  constructor(
    private route: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private merchantService: MerchantDataService,
    private router: Router) {
    this._baseUrl = baseUrl;

  }



  addIngredient(ingredient: string) {
    let ing = new Ingredient();
    if(ingredient.length <= 20 && ingredient.length > 0){
      ing.Naam = ingredient;
      this._ingredienten.push(ing);
    }else{
      this.errorMsg = "Er is een fout opgetreden bij het toevoegen van een ingredient(max 20 tekens)"
    }
  }

  addTag(tag: string) {
    let t = new Tag();
    if(tag.length <= 20 && tag.length > 0){
      t.Naam = tag;
    this._tags.push(t);
    }else{
      this.errorMsg = "Er is een fout opgetreden bij het toevoegen van een tag(max 20 tekens)";
    }
  }

  removeIng(ingredient) {
    for (let i = 0; i < this._ingredienten.length; i++) {
      if (this._ingredienten[i].Naam == ingredient.Naam) {
        this._ingredienten.splice(i, 1);
        break;
      }
    }
  }

  removeTag(tag) {
    for (let i = 0; i < this._tags.length; i++) {
      if (this._tags[i].Naam == tag.Naam) {
        this._tags.splice(i, 1);
        break;
      }
    }
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
      ],
      ingredient: [
        ''
      ],
      tag: [
        ''
      ]

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

    if (files.length == 0) {
    } else {
      if (files.length > 1) {
        for (var x = 0; x < files.length; x++) {
          data.append("afbeeldingen", files[x]);
        }
      } else {
        data.append("afbeeldingen", files[0]);
      }
    }

    data.append("naam", (<HTMLInputElement>document.getElementById('name')).value);
    data.append("beschrijving", (<HTMLInputElement>document.getElementById('description')).value);
    data.append("beginDatum", (<HTMLInputElement>document.getElementById('startdate')).value);
    data.append("eindDatum", (<HTMLInputElement>document.getElementById('enddate')).value);
    data.append("prijs", Number((<HTMLInputElement>document.getElementById('price')).value));
    data.append("ingredienten", JSON.stringify(this._ingredienten));
    data.append("tags", JSON.stringify(this._tags));

    if(this._ingredienten.length != 0 && this._tags.length != 0){
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
    }else{
      this.errorMsg = "Gelieve ingredienten of tags in te voeren"
    }
    
  }

  ngAfterViewInit() {
    this.merchantService.getLunchById(this.route.snapshot.paramMap.get('id')).subscribe(lunch => {
      this._lunch = lunch;
      var price = (<HTMLInputElement>document.getElementById('price'));
      price.value = this._lunch.prijs + "";

      (<HTMLInputElement>document.getElementById('name')).value = this._lunch.naam;
      (<HTMLInputElement>document.getElementById('description')).value = this._lunch.beschrijving;
      (<HTMLInputElement>document.getElementById('startdate')).value = this.formatDate(this._lunch.beginDatum);
      (<HTMLInputElement>document.getElementById('enddate')).value = this.formatDate(this._lunch.eindDatum);

      this._ingredientObjects = this._lunch.ingredienten;
      this._tagObjects = this._lunch.tags;

      this._ingredientObjects.forEach(ing => {
        let ingredient = new Ingredient();
        ingredient.Naam = ing.ingredient.naam;
        this._ingredienten.push(ingredient);
      });

      this._tagObjects.forEach(t => {
        let tag = new Tag();
        tag.Naam = t.tag.naam;
        this._tags.push(tag);
      });
    });

  }

  fileChange(event) {
    this.filesToUpload = event.target.files;
  }


}
