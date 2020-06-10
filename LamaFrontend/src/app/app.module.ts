import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StartPageComponent } from './components/start-page/start-page.component';
import {MsalAngularConfiguration, MsalModule} from '@azure/msal-angular';
import { ClubsComponent } from './components/clubs/clubs.component';

@NgModule({
  declarations: [
    AppComponent,
    StartPageComponent,
    ClubsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MsalModule.forRoot({
      auth: {
          clientId: 'Enter_the_Application_Id_here', // This is your client ID
          authority: 'https://login.microsoftonline.com/Enter_the_Tenant_Info_Here', // This is your tenant info
          redirectUri: 'Enter_the_Redirect_Uri_Here' // This is your redirect URI
      },
      cache: {
          cacheLocation: 'localStorage',
          storeAuthStateInCookie: false, // set to true for IE 11
      },
   }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
