export const environment = {
  appVersion: require('../../package.json').version,
  production: true,
  api: { // API Endpoints
    striveUrl:  'http://localhost:60001/',
    //striveUrl:  'https://mammothuatapi.azurewebsites.net/',
    striveCdn: 'https://mammothuatapi-cdn.azurewebsites.net/',
    //signalR:  'https://mammothuatapi.azurewebsites.net/',
    //striveUrl: 'http://14.141.185.75:5006/'
    //striveUrl: 'http://' + location.hostname + ':5001',
    //appUrl: 'http://' + location.hostname + ':5000'
  },
};
