import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Handelaar } from 'src/models/handelaar';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LunchMerchantDataService {

  private _baseUrl:string; 

  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this._baseUrl = baseUrl;
   }

   getMerchantById(id):Observable<Handelaar>{
    return this.http.get(this._baseUrl+"api/Handelaar/"+id).pipe(map(Handelaar.fromJSON));
  
  }
}
