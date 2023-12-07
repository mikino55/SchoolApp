import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { UserstorageService } from './userstorage.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  var storageService = inject(UserstorageService);
  console.log('Intercepting....');
  if (!req.url.startsWith('api/user/signin')) {
    console.log('Adding Authorization header');
    var authReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + storageService.token.accessToken),
    });

    return next(authReq);
  }
  return next(req);
};
