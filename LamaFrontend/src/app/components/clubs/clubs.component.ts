import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthError, InteractionRequiredAuthError } from 'msal';
import { MsalService } from '@azure/msal-angular';
import Club from 'src/app/models/club';

@Component({
  selector: 'app-clubs',
  templateUrl: './clubs.component.html',
  styleUrls: ['./clubs.component.scss']
})
export class ClubsComponent implements OnInit {
  clubs:Array<Club>;

  clubName:string;
  constructor(private http: HttpClient,private authService:MsalService) { }

  ngOnInit(): void {
    this.getClubs();
  }

  clubNameChanged(event){
    this.clubName = event.target.value;
  }

  createClub(){
    var club = new Club();
    club.clubName = this.clubName;
    var url = 'https://localhost:5001/api/clubs';
    this.http.post(url,
        JSON.stringify(club),
        {
          headers: new HttpHeaders().set('Content-Type', 'application/json'),
        }
      ).subscribe({
      next: result => {
        console.log(result);
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
    this.getClubs();
  }

  getClubs(){
    var url = 'https://localhost:5001/api/clubs';
    this.http.get(url).subscribe({
      next: (clubs:Array<Club>) => {
        console.log(clubs);
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
