import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { UserstorageService } from './userstorage.service';
import { TokenData } from '../interfaces/user.interfaces';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url = 'https://localhost:7099/account';

  constructor(private http: HttpClient, private storageService: UserstorageService) { }


  signIn(username: string, password: string) : Observable<any>{
    let userData = JSON.stringify({
      username : username,
      password : password
    });
    
    return this.http.post(`${this.url}/login`, userData, { withCredentials: true });
  }

  signInWithUserEndpoint(username: string, password: string) : Observable<TokenData>{
    let userData = JSON.stringify({
      email : username,
      password : password
    });

    console.log('Sign In', userData);
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return this.http
      .post<TokenData>(`https://localhost:7099/api/user/signin`, userData, { headers : headers})
      .pipe(
        tap(data =>
          {
             console.log('Token acquired', data);
             this.storageService.token = data;
          })
      );
  }
}
