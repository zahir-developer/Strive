import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { WhiteLabelService } from 'src/app/shared/services/data-service/white-label.service';

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
  @Input() customColor?: any;
  constructor(
    private whiteLabelService: WhiteLabelService
  ) { }

  ngOnInit(): void {
    this.bodyToggle = false;
    this.navigationToggle = false;
    this.primaryToggle = false;
    this.secondaryToggle = false;
    this.tertiaryToggle = false;
    this.bodyColor = this.customColor.BodyColor;
    this.navigationColor = this.customColor.NavigationColor;
    this.primaryColor = this.customColor.PrimaryColor;
    this.secondaryColor = this.customColor.SecondaryColor;
    this.tertiaryColor = this.customColor.TertiaryColor;
  }

  closePopup() {
    this.closeColorPopup.emit();
  }

  saveColor() {
    document.documentElement.style.setProperty(`--primary-color`, this.primaryColor);
    document.documentElement.style.setProperty(`--navigation-color`, this.navigationColor);
    document.documentElement.style.setProperty(`--secondary-color`, this.secondaryColor);
    document.documentElement.style.setProperty(`--tertiary-color`, this.tertiaryColor);
    document.documentElement.style.setProperty(`--body-color`, this.bodyColor);
    const customColorObj = {
      themeId: this.customColor.ThemeId,
      themeName: this.customColor.ThemeName,
      fontFace: '',
      primaryColor: this.primaryColor,
      secondaryColor: this.secondaryColor,
      tertiaryColor: this.tertiaryColor,
      navigationColor: this.navigationColor,
      bodyColor: this.bodyColor,
      defaultLogoPath: '',
      defaultTitle: '',
      comments: '',
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    };
    const finalObj = {
      themes: customColorObj
    };
    this.whiteLabelService.saveCustomColor(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.closePopup();
      }
    });
  }

}
