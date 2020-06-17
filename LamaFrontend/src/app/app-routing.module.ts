import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StartPageComponent } from './components/start-page/start-page.component';
import { ClubsComponent } from './components/clubs/clubs.component';
import { MsalGuard, MsalService } from '@azure/msal-angular';
import { ClubComponent } from './components/club/club.component';


const routes: Routes = [
  {path: '', component:StartPageComponent},
  { path: 'home',   component:StartPageComponent },
  {path:'clubs', component:ClubsComponent, canActivate: [MsalGuard]},
  {path:'clubs/:name', component:ClubComponent, canActivate: [MsalGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[MsalGuard, MsalService]
})
export class AppRoutingModule { }
