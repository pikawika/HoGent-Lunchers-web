import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public redirectUrl: string;
  private readonly _tokenKey = 'currentUser';
  private _user$: BehaviorSubject<string>;
  private _baseUrl: String;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this._baseUrl = baseUrl;
    let jwtHelper = new JwtHelperService();
    let parsedToken = jwtHelper.decodeToken(localStorage.getItem(this._tokenKey));
    console.log(parsedToken);
    
    if (parsedToken) {
      const expires = jwtHelper.isTokenExpired(localStorage.getItem(this._tokenKey));
      if (expires) {
        localStorage.removeItem(this._tokenKey);
        parsedToken = null;
      }
    }
    this._user$ = new BehaviorSubject<string>(parsedToken && parsedToken.gebruikersnaam);
    console.log("halp"+this._user$.value);
        
  }

  login(username: string, password: string): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/gebruiker/login', { 'Gebruikersnaam':username,'Wachtwoord':password }).pipe(
      map((res: any) => {
        const token = res.token;
        if (token) {
          localStorage.setItem(this._tokenKey, token);
          this._user$.next(username);
          return true;
        } else {
          return false;
        }
      })
    );
  }

  registerKlant(username: string, password: string,tel:string, email:string,voornaam:string,achternaam:string): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/gebruiker/registreer', 
    { 
      "Telefoonnummer": tel,
      "Email": email,
      "Voornaam": voornaam,
      "Achternaam": achternaam,
      "Login": {
        "Gebruikersnaam": username,
        "Rol": "klant",
        "Wachtwoord": password
      },
    }).pipe(
      map((res: any) => {
        const token = res.token;
        if (token) {
          localStorage.setItem(this._tokenKey, token);
          this._user$.next(username);
          return true;
        } else {
          return false;
        }
      })
    );
  }

  registerMerchant(username: string, tel:string, email:string,voornaam:string,achternaam:string,
    name:string, website:string, street: string, number:string, code: string, city:string
    ): Observable<boolean> {
    return this.http.post(this._baseUrl+'api/gebruiker/registreer', 
    { 
      "Telefoonnummer": tel,
      "Email": email,
      "Voornaam": voornaam,
      "Achternaam": achternaam,
      "Login": {
        "Gebruikersnaam": username,
        "Rol": "handelaar",
        "Wachtwoord": "voorlopigWachtwoord123"
      },
      "HandelsNaam": name,
      "Website": website,
		  "Locatie": { "Straat" : street,
								"Huisnummer" : number,
								"Postcode" : code,
								"Gemeente" : city,
								"Latitude" : "1",
								"Longitude": "1"}
    }).pipe(
      map((res: any) => { return res }));
  }

  logout() {
    if (this.user$.getValue()) {
      localStorage.removeItem('currentUser');
      setTimeout(() => this._user$.next(null));
    }
  }

  get token(): string {
    const localToken = localStorage.getItem(this._tokenKey);
    return !!localToken ? localToken : '';
  }

  get user$(){    
    return this._user$;
  }
  
}
