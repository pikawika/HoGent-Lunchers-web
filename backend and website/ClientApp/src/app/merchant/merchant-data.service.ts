import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Lunch } from 'src/models/lunch';
import { Handelaar } from 'src/models/handelaar';
import { Ingredient } from 'src/models/Ingredient';
import { Tag } from 'src/models/Tag';
import { Reservatie } from 'src/models/reservatie';

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

  getIngredienten():Observable<Ingredient[]>{
    return this.http.get(this._baseUrl+"api/ingredient").pipe(
      map((list: any[]): Ingredient[] => list.map(Ingredient.fromJSON))
      );
  }

  getTag(): Observable<Tag[]>{
    return this.http.get(this._baseUrl+"api/tag").pipe(
      map((list: any[]): Tag[] => list.map(Tag.fromJSON))
      );
  }

  addLunch(data: FormData): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/lunch', data).pipe(map((res: any) => { return res }));
  }

  getLunchById(id): Observable<Lunch> {
    return this.http.get(this._baseUrl+'api/lunch/' + id).pipe(map(Lunch.fromJSON));
  }

  editLunch(id, data: FormData): Observable<boolean> {
    return this.http.put(this._baseUrl+'api/lunch/'+id, data).pipe(map((res: any) => {return res}));
  }

  removeLunch(id, data: FormData): Observable<string> {
    console.log("In de data service: "+id);
    return this.http.put(this._baseUrl+'api/lunch/'+id+'?delete=true', data, {observe:'response'}).pipe(map((res: any) => {return res}));
  }

  approveReservation(reservatieId) {
    return this.http.put(this._baseUrl+'api/reservatie', { 'reservatieId':reservatieId, 'status':'Goedgekeurd' },{observe: 'response'});
  }

  declineReservation(reservatieId){
    return this.http.put(this._baseUrl+'api/reservatie', { 'reservatieId':reservatieId, 'status':'Afgekeurd' },{observe:'response'});
  }

  get reservations(): Observable<Reservatie[]> {
    return this.http.get(this._baseUrl+'api/reservatie/').pipe(
      map((list: any[]): Reservatie[]=>
        list.map(Reservatie.fromJSON)
      )
    );
  }
}
