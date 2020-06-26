import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Club from 'src/app/models/club';
import { ClubService } from 'src/app/services/club.service';
import ClubDetail from 'src/app/models/clubDetail';
import User from 'src/app/models/user';

@Component({
  selector: 'app-club',
  templateUrl: './club.component.html',
  styleUrls: ['./club.component.scss']
})
export class ClubComponent implements OnInit {
  public club:ClubDetail;
  userName:string;

  constructor(private route:ActivatedRoute, private clubService:ClubService) { }

  ngOnInit(): void {
    var clubName;
    this.route.paramMap.subscribe(params => {
      clubName = params.get('name');
    });
    this.getClub(clubName);
  }

  userNameChanged(event){
    this.userName = event.target.value;
  }

  getClub(clubName:string){
    this.clubService.getClub(clubName).subscribe({
      next: (club:ClubDetail) => {
        this.club = club;
      },
      error: (err) => {
        this.clubService.handleError(err);
      }
    }
  );
  }

  addMember(){
    var user = new User();
    user.userName = this.userName;
    this.clubService.addMember(this.club.clubId, this.userName).subscribe({
      next: any => {
        console.log('user successfully added to club')
        this.getClub(this.club.clubName);
      },
      error: (err) => {
        this.clubService.handleError(err);
      }
    });
  }

}
