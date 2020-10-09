import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-custom-theme',
  templateUrl: './custom-theme.component.html',
  styleUrls: ['./custom-theme.component.css']
})
export class CustomThemeComponent implements OnInit {
  toggle: boolean;
  color = '#FFFFFF';
  constructor() { }

  ngOnInit(): void {
    this.toggle = false;
  }

}
