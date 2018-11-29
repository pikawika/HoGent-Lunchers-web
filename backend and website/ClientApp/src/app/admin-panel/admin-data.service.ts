import {Handelaar} from '../../models/handelaar';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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
}
