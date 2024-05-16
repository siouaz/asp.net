import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import {GoogleLoginProvider,} from '@abacritt/angularx-social-login';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(),
    {
    provide: 'SocialAuthServiceConfig',
    useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider(
            'clientId'
          )
        },
      ],
      onError: (err) => {
        console.error(err);
      }
    } as SocialAuthServiceConfig,
    }, provideAnimationsAsync(), provideAnimationsAsync()
  ]
};

