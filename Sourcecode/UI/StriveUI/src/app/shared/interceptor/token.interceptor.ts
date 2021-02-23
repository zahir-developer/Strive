import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../services/login.service';
import { AuthService } from '../services/common-service/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private router: Router, private route: ActivatedRoute, private loginService: LoginService,
        private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = localStorage.getItem('authorizationToken');
        const refreshToken = localStorage.getItem('token');
        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${localStorage.getItem('authorizationToken')}`,
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
            }
        });
        return next.handle(request).pipe(tap(event => { }, err => {
            if (err.status === 200) {
                if (accessToken && refreshToken) {
                    this.refreshToken(accessToken, refreshToken);
                }
                // this.authService.logout();
                // this.router.navigate([`/login`], { relativeTo: this.route });
            }
        }));
    }

    refreshToken(accessToken, refreshToken) {
        const obj = {
            token: accessToken,
            refreshToken
        };
        this.authService.refershToken(obj).subscribe( res => {

        });
    }
}
