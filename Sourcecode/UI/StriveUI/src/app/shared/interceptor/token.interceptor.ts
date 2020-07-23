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
        // const token = localStorage.getItem('authorizationToken');
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
            // if (err.status === 401) {
            //     this.authService.logout();
            //     this.router.navigate([`/login`], { relativeTo: this.route });
                // const params = {
                //     token: localStorage.getItem('authorizationToken'),
                //     refreshToken: localStorage.getItem('refreshToken'),
                // };
                // return this.loginService.refreshToken(params).subscribe((data: any) => {
                //     const token = JSON.parse(data);
                //     if (data.statusCode === 200) {
                //         localStorage.setItem('authorizationToken', token.Token);
                //         localStorage.setItem('refreshToken', token.RefreshToken);
                //         request = request.clone({
                //             setHeaders: {
                //                 Authorization: `Bearer ${localStorage.getItem('authorizationToken')}`,
                //                 'Access-Control-Allow-Origin': '*',
                //                 'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
                //                 // 'Accept': 'application/json'
                //             }
                //         });
                //         return next.handle(request);
                //     }
                // });

            // } else {
            //     this.authService.logout();
            //     this.router.navigate([`/login`], { relativeTo: this.route });
            // }
        }));
    }
}
