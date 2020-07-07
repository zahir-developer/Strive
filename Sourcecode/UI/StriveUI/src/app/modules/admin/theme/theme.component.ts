import { Component, OnInit } from '@angular/core';
import { ThemeService } from 'src/app/shared/common-service/theme.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-theme',
  templateUrl: './theme.component.html',
  styleUrls: ['./theme.component.css']
})
export class ThemeComponent implements OnInit {


  ngOnInit(): void {
  }
  darkTheme =  new FormControl(false);

  constructor(private themeService: ThemeService) {
    this.darkTheme.valueChanges.subscribe(value => {
      if (value) {
        this.themeService.toggleDark();
      } else {
        this.themeService.toggleLight();
      }
    });
  }
}

