import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MessengerComponent } from './messenger.component';


const routes: Routes = [
  {path: 'messenger', component: MessengerComponent},
  {path: '', component: MessengerComponent, children: [
    {path: 'messenger', component: MessengerComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MessengerRoutingModule { }
