import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function changeBaseUrlForJsonServer() {

  let baseUrl = getBaseUrl();

  const ngPort = ':4200'; // default Angular server port
  const jsonPort = ':3000'; // default json-server port

  if (baseUrl?.indexOf(ngPort) !== -1) {
    baseUrl = baseUrl.replace(ngPort, jsonPort);
  }

  return baseUrl;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'BASE_URL_FOR_API', useFactory: changeBaseUrlForJsonServer, deps: [] },
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
