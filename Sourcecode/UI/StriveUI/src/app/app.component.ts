import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'StriveUI';
  loginForm: FormGroup;

  ngOnInit(): void {
    this.loginForm = new FormGroup(
      {
        'username': new FormControl(null),
        'password': new FormControl(null)
      });
  }


}
