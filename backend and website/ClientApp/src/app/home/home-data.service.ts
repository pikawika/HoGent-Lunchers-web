import {map} from 'rxjs/operators';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Lunch} from '../../models/lunch';

@Injectable({
  providedIn: 'root'
})
export class HomeDataService {

  private _baseUrl:string; 

  constructor(private http:HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl; 
  }

  get lunches():Observable<Lunch[]>{
    
    return this.http.get(this._baseUrl+"api/Lunch").pipe(
      map((list: any[]): Lunch[]=>
        list.map(Lunch.fromJSON)
      )
    );
  }

  getLunchById(id):Observable<Lunch>{
    
    return this.http.get(this._baseUrl+"api/Lunch/"+id).pipe(
      map((lun: any): Lunch=>
        lun.map(Lunch.fromJSON)
      )
    );
  }

  
}
