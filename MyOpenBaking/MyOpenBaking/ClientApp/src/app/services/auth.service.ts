import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const AUTH_API = 'http://localhost:5000/api/account/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

export interface User {
  UserName : string;
  Password: string;
  Email? : string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  login(user: User): Observable<any> {
    return this.http.post(AUTH_API + 'signin', user, httpOptions);
  }

  register(user: User): Observable<any> {
    return this.http.post(AUTH_API + 'signup', user, httpOptions);
  }
}