import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StartPageComponent } from './components/start-page/start-page.component';
import { ClubsComponent } from './components/clubs/clubs.component';


const routes: Routes = [
  {path: '', component:StartPageComponent},
  { path: 'home',   component:StartPageComponent },
  {path:'clubs', component:ClubsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
