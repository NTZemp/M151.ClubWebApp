import { Component, OnInit } from '@angular/core';
import {MsalService} from '@azure/msal-angular';
import { msalAngularConfig, tokenRequest } from './app.config';
import { AuthResponse, AuthError } from 'msal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'LamaFrontend';
  isAuthenticated:boolean;
  
  constructor(private msal:MsalService, public router:Router){
    
  }
  ngOnInit(): void {
    if(this.msal.getAccount() == null){
      this.isAuthenticated = false;
    }else{
      this.isAuthenticated = true;
    }
  }

  login(){
    this.msal.loginRedirect(tokenRequest);
  }
}
