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


  addLunch(data: FormData
    ): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/lunch',
    data).pipe(
      map((res: any) => { return res }));
  }
}
