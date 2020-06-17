import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Club from 'src/app/models/club';
import { ClubService } from 'src/app/services/club.service';
import ClubDetail from 'src/app/models/clubDetail';

@Component({
  selector: 'app-club',
  templateUrl: './club.component.html',
  styleUrls: ['./club.component.scss']
})
export class ClubComponent implements OnInit {
  public club:ClubDetail;

  constructor(private route:ActivatedRoute, private clubService:ClubService) { }

  ngOnInit(): void {
    var clubName;
    this.route.paramMap.subscribe(params => {
      clubName = params.get('name');
    });
    this.clubService.getClub(clubName).subscribe({
        next: (club:ClubDetail) => {
          this.club = club;
        }
      }
    );
  }

}
