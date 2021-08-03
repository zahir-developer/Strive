
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  name: 'Development',
  appName: 'Strive Phase 1',
  api: { // API Endpoints
    striveUrl:  'http://localhost:60001/',
    //striveUrl: 'https://mammothuatapi-qa.azurewebsites.net/',
    striveCdn: 'https://mammothuatapi-cdn.azurewebsites.net/',
    //striveUrl:  'http://40.114.79.101:5006/',
    //striveUrl:  'http://40.114.79.101:5006/',
    signalR:  'https://mammothuatapi-qa.azurewebsites.net/',
    //signalR:  'https://1447af389eed.ngrok.io'
    //striveUrl: 'http://14.141.185.75:5006/'
    //  striveUrl: 'http://' + location.hostname + ':5001',
    //   appUrl: 'http://' + location.hostname + ':5000'
  }
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
