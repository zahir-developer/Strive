import { Injectable } from '@angular/core';
import { tap} from 'rxjs/operators';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private router: Router, private route: ActivatedRoute, private loginService: LoginService) { }

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
            console.log(err);
            if (err.status === 401) {
                console.log(err, 'token expired');
                const params = {
                    token: localStorage.getItem('authorizationToken'),
                    refreshToken: localStorage.getItem('refreshToken'),
                };
                return this.loginService.refreshToken(params).subscribe((data: any) => {
                    const token = JSON.parse(data);
                    if (data.statusCode === 200) {
                        localStorage.setItem('authorizationToken', token.Token);
                        localStorage.setItem('refreshToken', token.RefreshToken);
                        request = request.clone({
                            setHeaders: {
                                Authorization: `Bearer ${localStorage.getItem('authorizationToken')}`,
                                'Access-Control-Allow-Origin': '*',
                                'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
                                // 'Accept': 'application/json'
                            }
                        });
                        return next.handle(request);
                    }
                });

            } else {
                this.router.navigate([`/login`], { relativeTo: this.route });
            }
        }));
    }
}
