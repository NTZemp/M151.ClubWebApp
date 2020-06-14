import { Component, OnInit } from '@angular/core';
import {MsalService} from '@azure/msal-angular';
import { msalAngularConfig, tokenRequest } from './app.config';
import { AuthResponse, AuthError } from 'msal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'LamaFrontend';
  isAuthenticated:boolean;
  constructor(private msal:MsalService){
    
  }
  ngOnInit(): void {
    if(this.msal.getAccount() == null){
      this.isAuthenticated = false;
    }else{
      this.isAuthenticated = true;
    }
  }

  profile(){

  }

  login(){
    this.msal.acquireTokenRedirect(tokenRequest);
  }
}
