import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-custom-theme',
  templateUrl: './custom-theme.component.html',
  styleUrls: ['./custom-theme.component.css']
})
export class CustomThemeComponent implements OnInit {
  bodyToggle: boolean;
  navigationToggle: boolean;
  primaryToggle: boolean;
  secondaryToggle: boolean;
  tertiaryToggle: boolean;
  color = '#FFFFFF';
  tertiaryColor = '#FFFFFF';
  secondaryColor = '#FFFFFF';
  primaryColor = '#FFFFFF';
  navigationColor = '#ffffff';
  bodyColor = '#ffffff';
  themeName: any;
  @Output() closeColorPopup = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
    this.bodyToggle = false;
    this.navigationToggle = false;
    this.primaryToggle = false;
    this.secondaryToggle = false;
    this.tertiaryToggle = false;
  }

  closePopup() {
    this.closeColorPopup.emit();
  }

  saveColor() {
    console.log(this.secondaryColor, 'secndaryClor');
  }

}
