import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthError, InteractionRequiredAuthError } from 'msal';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-clubs',
  templateUrl: './clubs.component.html',
  styleUrls: ['./clubs.component.scss']
})
export class ClubsComponent implements OnInit {
  clubs;
  constructor(private http: HttpClient,private authService:MsalService) { }

  ngOnInit(): void {
    this.getClubs();
  }

  getClubs(){
    var url = 'https://localhost:5001/api/clubs';
    this.http.get(url).subscribe({
      next: (clubs) => {
        console.log(clubs);
        console.log('yupiiiii');
        this.clubs = clubs;
      },
      error: (err: AuthError) => {
        console.log('auth error');
        // If there is an interaction required error,
        // call one of the interactive methods and then make the request again.
        if (InteractionRequiredAuthError.isInteractionRequiredError(err.errorCode)) {
          this.authService.acquireTokenPopup({
            scopes: this.authService.getScopesForEndpoint(url)
          })
        }
      }
    });
  }

}
