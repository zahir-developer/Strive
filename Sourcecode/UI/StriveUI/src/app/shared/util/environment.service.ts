import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


export let environmentData = {
  production: false,
  name: '',
  appName: '',
  api: { // API Endpoints
    epmsApi:  ''
  }
};
@Injectable({
  providedIn: 'root'
})
export class EnvironmentService   {
 static hostenvironment: string;
 static environment: any;
  constructor(private http: HttpClient) {
  }
 /*
  loading config.json based on ASPNETCORE_ENVIRONMENT
  */
 loadAppConfig (environment: string) {
  return   this.http.get('/assets/config/' + environment.toLocaleLowerCase() + '.json').toPromise()
  .then((data: any) => {
    return   EnvironmentService.environment = data;
  },
  (error: any) => {
    this.loadDefaultAppConfig();
  });
}

/*
Get the ASPNETCORE_ENVIRONMENT from api
*/
loadEnvironmentVariableAndConfig () {
  return   this.http.get('/api/SampleData').toPromise()
     .then((environment: string) => {
       if (environment !== undefined && environment !== null) {
        EnvironmentService.hostenvironment = environment;
        this.loadAppConfig(environment);
       }
      },
      (error: any) => {
        this.loadDefaultAppConfig();
      });
}
/*
loading default config.json
*/
loadDefaultAppConfig (): any  {
  return   this.http.get('/assets/config/config.json').toPromise()
  .then((data: any) => {
    return   EnvironmentService.environment = data;
  },
  (error: any) => {
  });
}
}
