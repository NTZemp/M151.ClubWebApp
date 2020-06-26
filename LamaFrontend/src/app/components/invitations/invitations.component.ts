import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import Invitation from 'src/app/models/invitation';
import InvitationStatus from 'src/app/models/invitationStatus';
import { Router } from '@angular/router';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.scss']
})
export class InvitationsComponent implements OnInit {
  invitations:Array<Invitation>;
  isLoading:boolean;
  constructor(private userService:UserService, private router:Router) { }

  ngOnInit(): void {
    this.getInvitations();
  }

  accept(invitationId){
    this.userService.updateInvitation(invitationId, InvitationStatus.Accept)
    .subscribe(
      {
        next: () => {this.router.navigate(['/clubs'])}
      }
    );
  }


  reject(invitationId){
    this.userService.updateInvitation(invitationId, InvitationStatus.Reject)
    .subscribe(
      {
        next: () => {this.router.navigate(['/clubs'])}
      }
    );
  }

  getInvitations(){
    this.isLoading = true;
    this.userService.getInvitations().subscribe({
      next: (invitations:Array<Invitation>) => {
        this.invitations = invitations;
      },
      error: (err) => {
        this.userService.handleError(err);
      }
    });
    this.isLoading = false;
  }


}
