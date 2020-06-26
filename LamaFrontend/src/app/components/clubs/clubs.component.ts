import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import Club from 'src/app/models/club';
import { ClubService } from 'src/app/services/club.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clubs',
  templateUrl: './clubs.component.html',
  styleUrls: ['./clubs.component.scss']
})
export class ClubsComponent implements OnInit {
  clubs:Array<Club>;
  isLoading:boolean;
  clubName:string;
  
  constructor(private clubService:ClubService, private router:Router) { }

  ngOnInit(): void {
    this.clubs = new Array<Club>();
   this.getClubs();
  }

  invitations(){
    this.router.navigate(['/invitations'])
  }
  
  getClubs(){
    this.isLoading = true;
    this.clubService.getClubs().subscribe({
      next: (clubs:Array<Club>) => {
        this.clubs = clubs;
        this.isLoading = false;
      },
      error: (err)=>{
        this.clubService.handleError(err);
      }
    });
  }

  clubDetails(name:string){
    this.router.navigate(['/clubs/'+ name])
  }

  clubNameChanged(event){
    this.clubName = event.target.value;
  }

  createClub(){
    this.clubService.createClub(this.clubName)
    .subscribe({
      next: any =>{
        this.getClubs();
      },
      error: (err)=>{
        this.clubService.handleError(err);
      }
    });
    this.clubName = '';
  }
}
