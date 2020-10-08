import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-white-labelling-section',
  templateUrl: './white-labelling-section.component.html',
  styleUrls: ['./white-labelling-section.component.css']
})
export class WhiteLabellingSectionComponent implements OnInit {
  sunshineTheme: any;
  constructor() { }

  ngOnInit(): void {
    this.sunshineTheme = {
      primarycolor: '#FEB83F',
      navigationcolor: '#FC6A0C',
      secondarycolor: '#FFF997',
      tertiarycolor: '#FD8B20'
    };
  }

  themeChange() {
    document.documentElement.style.setProperty(`--primary-color`, '#FEB83F');
    document.documentElement.style.setProperty(`--navigation-color`, '#FC6A0C');
    document.documentElement.style.setProperty(`--secondary-color`, '#FFF997');
    document.documentElement.style.setProperty(`--tertiary-color`, '#FD8B20');
  }

  defaultTheme() {
    document.documentElement.style.setProperty(`--primary-color`, '#1DC5B3');
    document.documentElement.style.setProperty(`--navigation-color`, '#24489A');
    document.documentElement.style.setProperty(`--secondary-color`, '#F2FCFE');
    document.documentElement.style.setProperty(`--tertiary-color`, '#10B7A5');
  }

  fontChange(style) {
    document.documentElement.style.setProperty(`--text-font`, style);
  }

}
