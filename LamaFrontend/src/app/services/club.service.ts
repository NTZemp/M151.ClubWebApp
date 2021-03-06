import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import Club from '../models/club';
import { AuthError, InteractionRequiredAuthError } from 'msal';
import { throwError, of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import ClubDetail from '../models/clubDetail';
import User from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ClubService {
  apiurl:string  = 'https://localhost:5001/api/';
  constructor(private http: HttpClient,private authService:MsalService) { 

  }

  
  getClub(clubName:string): Observable<any>{
    var url = this.apiurl + 'clubs/' + clubName;
    return this.http.get<ClubDetail>(url);
  }

  getClubs():Observable<any>{
    var url = 'https://localhost:5001/api/clubs';
    return this.http.get<Array<Club>>(url);
  }

  addMember(clubId:string, userName:string):Observable<any>{
    var url = this.apiurl + 'clubs/' + clubId + '/invite'
    return this.http.post(url,
      JSON.stringify(userName),
      {
        headers: new HttpHeaders().set('Content-Type', 'application/json'),
      });
  }

  createClub(clubName:string):Observable<any>{
    var club = new Club();
    club.clubName = clubName;
    var url = 'https://localhost:5001/api/clubs';
    return this.http.post(url,
        JSON.stringify(club),
        {
          headers: new HttpHeaders().set('Content-Type', 'application/json'),
        }
      );
  }



  public handleError(error: HttpErrorResponse):any {
    if(error instanceof AuthError){
      if (InteractionRequiredAuthError.isInteractionRequiredError(error.errorCode)) {
        this.authService.acquireTokenPopup({
          scopes: this.authService.getScopesForEndpoint(this.apiurl)
        });
      }
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }



  


}
