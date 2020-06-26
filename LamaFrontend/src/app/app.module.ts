import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StartPageComponent } from './components/start-page/start-page.component';
import {MsalAngularConfiguration, MsalModule, MsalInterceptor, MSAL_CONFIG, MSAL_CONFIG_ANGULAR, MsalService, MsalGuard} from '@azure/msal-angular';
import {Configuration} from 'msal'
import { ClubsComponent } from './components/clubs/clubs.component';
import { msalConfig, msalAngularConfig } from './app.config';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ClubComponent } from './components/club/club.component';
import { InvitationsComponent } from './components/invitations/invitations.component';

function MSALConfigFactory(): Configuration {
  return msalConfig;
}

function MSALAngularConfigFactory(): MsalAngularConfiguration {
  return msalAngularConfig;
}

@NgModule({
  declarations: [
    AppComponent,
    StartPageComponent,
    ClubsComponent,
    ClubComponent,
    InvitationsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MsalModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_CONFIG,
      useFactory: MSALConfigFactory
    },
    {
      provide: MSAL_CONFIG_ANGULAR,
      useFactory: MSALAngularConfigFactory
    },
    MsalGuard,
    MsalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
