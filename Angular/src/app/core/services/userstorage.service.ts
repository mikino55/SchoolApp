import { Injectable } from '@angular/core';
import { TokenData } from '../interfaces/user.interfaces';

@Injectable({
  providedIn: 'root'
})
export class UserstorageService {

  private tokenStorageName = 'schoolapp.tokendata';

  constructor() { }

  set token(token: TokenData) {
    localStorage.setItem(this.tokenStorageName, JSON.stringify(token));
  }

  /**
   * Gets user token from storage.
   * @returns {TokenData}
   */
  get token() : TokenData {
    const tokenData: TokenData = JSON.parse(localStorage.getItem(this.tokenStorageName)!);
    return tokenData;
  }
}
