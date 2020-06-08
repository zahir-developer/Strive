import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms'
import { SharedModule } from './shared/shared.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'StriveUI';
}
