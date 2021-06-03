import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../services/login.service';
import { AuthService } from '../services/common-service/auth.service';
import { MessageServiceToastr } from '../services/common-service/message.service';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    private refreshingInProgress: boolean;
    private accessTokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private loginService: LoginService,
        private authService: AuthService,
        private messageService: MessageServiceToastr
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = localStorage.getItem('authorizationToken');

        return next.handle(this.addAuthorizationHeader(req, accessToken)).pipe(
            catchError(err => {
                // in case of 401 http error 
                console.log(err, 'error');
                if (err instanceof HttpErrorResponse && err.status === 401) {
                    // get refresh tokens
                    const refreshToken = localStorage.getItem('refreshToken');

                    // if there are tokens then send refresh token request
                    if (refreshToken && accessToken) {
                       return this.refreshToken(req, next);
                    }
                    // otherwise logout and redirect to login page
                    return this.logoutAndRedirect(err);
                }

                // in case of 403 http error (refresh token failed)
                if (err instanceof HttpErrorResponse && err.status === 403) {

                    // logout and redirect to login page
                    return this.logoutAndRedirect(err);
                }
                // if error has status neither 401 nor 403 then just return this error
                return throwError(err);
            })
        );
    }

    private addAuthorizationHeader(request: HttpRequest<any>, token: string): HttpRequest<any> {
        if (token) {
            return request.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
        }

        return request;
    }

    private logoutAndRedirect(err): Observable<HttpEvent<any>> {

        if (err.status === 404) {
            this.messageService.showMessage({ severity: 'error', title: 'Authentication refresh token failed.', body: 'Please relogin and try again.!' });
        }
        else if (err.status === 401) {
            this.messageService.showMessage({ severity: 'error', title: 'Access Denied.', body: 'UnAuthenticated access. Please relogin and try again.!' });
        }
        this.authService.logout();
        // this.router.navigateByUrl('/login');

        return throwError(err);
    }

    private refreshToken(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!this.refreshingInProgress) {
            this.refreshingInProgress = true;
            this.accessTokenSubject.next(null);
            return this.authService.refreshToken().pipe(
                switchMap((res) => {
                    console.log(res, 'refresh')
                    this.refreshingInProgress = false;
                    const token = JSON.parse(res.resultData);
                    this.accessTokenSubject.next(token.Token);
                    // repeat failed request with new token
                    return next.handle(this.addAuthorizationHeader(request, token.Token));
                })
            );
        } else {
            // wait while getting new token
            return this.accessTokenSubject.pipe(
                filter(token => token !== null),
                take(1),
                switchMap(token => {
                    // repeat failed request with new token
                    return next.handle(this.addAuthorizationHeader(request, token));
                }));
        }
    }

}
