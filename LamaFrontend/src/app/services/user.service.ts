import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthError, InteractionRequiredAuthError } from 'msal';
import { MsalService } from '@azure/msal-angular';
import Invitation from '../models/invitation';
import InvitationStatus from '../models/invitationStatus';

@Injectable({
  providedIn: 'root'
})
export class UserService {
   apiurl:string = 'https://localhost:5001/api/';
  
  constructor(private httpClient:HttpClient, private authService:MsalService) { }

  getInvitations(): Observable<any>{
    var url = this.apiurl + 'user/invitations';
    return this.httpClient.get<Array<Invitation>>(url);
  }

  updateInvitation(invitationId:string, status:InvitationStatus): Observable<any>{
    var url = this.apiurl + "user/invitations/" + invitationId;
    return this.httpClient.post(url,
      JSON.stringify(status),
      {
        headers: new HttpHeaders().set('Content-Type', 'application/json'),
      });
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
