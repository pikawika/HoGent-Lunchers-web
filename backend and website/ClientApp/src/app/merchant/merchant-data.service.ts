import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Lunch } from 'src/models/lunch';
import { Handelaar } from 'src/models/handelaar';

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

  getMerchantById(id):Observable<Handelaar>{
    return this.http.get(this._baseUrl+"api/Handelaar/"+id).pipe(map(Handelaar.fromJSON));
  
  }

  addLunch(data: FormData): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/lunch', data).pipe(map((res: any) => { return res }));
  }

  getLunchById(id : number): Observable<Lunch> {
    return this.http.get(this._baseUrl+'api/lunch/' + id).pipe(map(Lunch.fromJSON));
  }

  editLunch(id: number, data: FormData): Observable<boolean> {
    return this.http.put(this._baseUrl+'api/lunch'+id, data).pipe(map((res: any) => {return res}));
  }


}
