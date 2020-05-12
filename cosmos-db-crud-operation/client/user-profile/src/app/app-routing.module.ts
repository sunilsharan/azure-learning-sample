import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './profile/user-list.component';
import { WelcomeComponent } from './home/welcome.component';
import { UserRegister } from './profile/user-register.component';


const routes: Routes = [
  {path: 'users', component:UserListComponent},
  {path: 'welcome', component:WelcomeComponent},
  {path: 'Register/:id', component:UserRegister},
  {path: 'Register', component:UserRegister},
  { path: '', redirectTo: 'welcome', pathMatch: 'full' },
  { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
