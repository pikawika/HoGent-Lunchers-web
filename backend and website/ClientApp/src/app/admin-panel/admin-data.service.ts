import {Handelaar} from '../../models/handelaar';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Reservatie } from '../../models/reservatie';
import { Lunch } from '../../models/lunch';

@Injectable({
  providedIn: 'root'
})
export class AdminDataService {

  private _baseUrl: String;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this._baseUrl = baseUrl;
  }

  get merchants(): Observable<Handelaar[]> {
    return this.http.get(this._baseUrl+'api/handelaar/').pipe(
      map((list: any[]): Handelaar[]=>
        list.map(Handelaar.fromJSON)
      )
    );
  }

  get reservations(): Observable<Reservatie[]> {
    return this.http.get(this._baseUrl+'api/reservatie/').pipe(
      map((list: any[]): Reservatie[]=>
        list.map(Reservatie.fromJSON)
      )
    );
  }

  get lunches(): Observable<Lunch[]> {
    return this.http.get(this._baseUrl+'api/lunch/').pipe(
      map((list: any[]): Lunch[]=>
        list.map(Lunch.fromJSON)
      )
    );
  }

  removeMerchant(data: FormData): Observable<string> {
    return this.http.post(this._baseUrl+'api/admin/verwijderhandelaar', data, {observe:'response'}).pipe(map((res: any) => {return res}));
  }

}
