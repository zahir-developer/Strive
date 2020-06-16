import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private router: Router, private route: ActivatedRoute) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
        setHeaders: {
            Authorization: `Bearer ${localStorage.getItem('authorizationToken')}`,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
            // 'Accept': 'application/json'
        }
    });
    // return next.handle(request);
    return next.handle(request).pipe(tap(event => { }, err => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
            // handle 401 errors
            // we need to logout the session and navigate to Login page
            this.router.navigate([`/login`], { relativeTo: this.route });
        }
        if (err instanceof HttpErrorResponse && err.status === 500) {
            // handle 500 errors
        }
    }));
}
}
