import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lunch } from '../../models/lunch';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DetailDataService {

  private _baseUrl:string; 

  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl; 
  }

  getLunchById(id):Observable<Lunch>{
    return this.http.get(this._baseUrl+"api/Lunch/"+id).pipe(map(Lunch.fromJSON));
  }

}
