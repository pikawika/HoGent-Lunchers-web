import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MerchantDataService {

  private _baseUrl: String;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this._baseUrl = baseUrl;
  }


  registerMerchant(naam: string, prijs : number, beschrijving: string, beginDatum : Date, eindDatum : Date, afbeeldingen : FormData
    ): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/lunch',
    { 
      "naam": naam,
      "prijs": prijs,
      "beschrijving": beschrijving,
      "beginDatum": beginDatum,
      "eindDatum": eindDatum,
      "ingredienten": [
        {
           "naam": "Ui"
        },
        {
           "naam": "Varkensvlees"
        }
      ],
      "tags": [
        {
           "naam": "Hamburger",
           "kleur": "FF6A6A"
        },
        {
           "naam": "Varkensvlees",
           "kleur": "FF6A6A"
        }
      ],
      "afbeeldingen": afbeeldingen
    }).pipe(
      map((res: any) => { return res }));
  }
}
