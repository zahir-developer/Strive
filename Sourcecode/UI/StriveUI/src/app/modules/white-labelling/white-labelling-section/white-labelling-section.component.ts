import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CustomThemeComponent } from '../custom-theme/custom-theme.component';
import { WhiteLabelService } from 'src/app/shared/services/data-service/white-label.service';
import { LogoService } from 'src/app/shared/services/common-service/logo.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-white-labelling-section',
  templateUrl: './white-labelling-section.component.html',
  styleUrls: ['./white-labelling-section.component.css']
})
export class WhiteLabellingSectionComponent implements OnInit {
  sunshineTheme: any;
  title = '';
  fileName = '';
  logoPath = '';
  activeColor = 'green';
  baseColor = '#ccc';
  overlayColor = 'rgba(255,255,255,0.5)';
  dragging = false;
  loaded = false;
  imageLoaded = false;
  imageSrc = '';
  whiteLabelId: any;
  showDialog: boolean;
  colorTheme: any = [];
  colorSelection: any;
  themeId: any;
  customColor: any;
  whiteLabelDetail: any;
  fontName = '';
  msgType: string;
  constructor(
    private modalService: NgbModal,
    private whiteLabelService: WhiteLabelService,
    private toastr: ToastrService,
    private logoService: LogoService,
    private ngxService: NgxSpinnerService
    ,private landingservice:LandingService
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.colorSelection = {
      bodyColor: '#F2FCFE',
      primaryColor: '#1DC5B3',
      secondaryColor: '#F2FCFE',
      tertiaryColor: '#10B7A5',
      navigationColor: '#24489A'
    };
    this.fontName = 'Open Sans';
    this.getAllWhiteLabelDetail();
  }
  landing(){
    this.landingservice.loadTheLandingPage()
  }
  themeChange(theme) {
    this.msgType = 'Theme';
    if (theme.ThemeName === 'Custom') {
      this.showDialog = true;
      this.themeId = theme.ThemeId;
      this.customColor = theme;
    } else {
      document.documentElement.style.setProperty(`--primary-color`, theme.PrimaryColor);
      document.documentElement.style.setProperty(`--navigation-color`, theme.NavigationColor);
      document.documentElement.style.setProperty(`--secondary-color`, theme.SecondaryColor);
      document.documentElement.style.setProperty(`--tertiary-color`, theme.TertiaryColor);
      document.documentElement.style.setProperty(`--body-color`, theme.BodyColor);
      this.themeId = theme.ThemeId;
      this.colorSelection = {
        bodyColor: theme.BodyColor,
        primaryColor: theme.PrimaryColor,
        secondaryColor: theme.SecondaryColor,
        tertiaryColor: theme.TertiaryColor,
        navigationColor: theme.NavigationColor
      };
    }

  }

  defaultTheme() {
    document.documentElement.style.setProperty(`--primary-color`, '#1DC5B3');
    document.documentElement.style.setProperty(`--navigation-color`, '#24489A');
    document.documentElement.style.setProperty(`--secondary-color`, '#F2FCFE');
    document.documentElement.style.setProperty(`--tertiary-color`, '#10B7A5');
    document.documentElement.style.setProperty(`--body-color`, '#F2FCFE');
    this.colorSelection = {
      bodyColor: '#F2FCFE',
      primaryColor: '#1DC5B3',
      secondaryColor: '#F2FCFE',
      tertiaryColor: '#10B7A5',
      navigationColor: '#24489A'
    };
  }
  nightThemeChange( ) {
    document.documentElement.style.setProperty(`--primary-color`, '#632b6c');
    document.documentElement.style.setProperty(`--navigation-color`, '#280f36');
    document.documentElement.style.setProperty(`--secondary-color`, '#c86b98');
    document.documentElement.style.setProperty(`--tertiary-color`, '#f09f9c');
  }
  customTheme() {
    this.showDialog = true;
  }

  closeColorPopup() {
    this.showDialog = false;
  }

  fontChange(style) {
    this.msgType = 'Font';
    this.fontName = style;
    document.documentElement.style.setProperty(`--text-font`, style);
  }

  getAllWhiteLabelDetail() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
      if (res.status === 'Success') {
        this.title = '';
        this.imageSrc = '';
        const label = JSON.parse(res.resultData);
        this.colorTheme = label.WhiteLabelling.Theme;
        this.whiteLabelDetail = label.WhiteLabelling.WhiteLabel;
        this.fontName = this.whiteLabelDetail.FontFace;
        this.themeId = this.whiteLabelDetail.ThemeId;
        this.colorTheme.forEach(item => {
          if (this.whiteLabelDetail.ThemeId === item.ThemeId) {
            document.documentElement.style.setProperty(`--primary-color`, item.PrimaryColor);
            document.documentElement.style.setProperty(`--navigation-color`, item.NavigationColor);
            document.documentElement.style.setProperty(`--secondary-color`, item.SecondaryColor);
            document.documentElement.style.setProperty(`--tertiary-color`, item.TertiaryColor);
            document.documentElement.style.setProperty(`--body-color`, item.BodyColor);
          }
        });
        document.documentElement.style.setProperty(`--text-font`, this.whiteLabelDetail.FontFace);
        if (label?.WhiteLabelling?.WhiteLabel !== undefined) {
          this.whiteLabelId = label.WhiteLabelling.WhiteLabel?.WhiteLabelId;
          this.title = label.WhiteLabelling.WhiteLabel?.Title;
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          this.logoService.setTitle(label.WhiteLabelling.WhiteLabel?.Title);
          this.logoPath = label.WhiteLabelling.WhiteLabel?.LogoPath;
          this.fileName = label.WhiteLabelling.WhiteLabel?.LogoPath;
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  CancelChanges() {
    this.toastr.success(this.msgType + MessageConfig.Reset, 'Success!');
    this.getAllWhiteLabelDetail();
  }

  handleInputChange(e) {
    this.msgType = 'Logo';
    this.fileName = '';
    const file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
    this.fileName = file ? file.name : '';
    const pattern = /image-*/;
    const reader = new FileReader();
    if (!file.type.match(pattern)) {
      alert('invalid format');
      return;
    }

    this.loaded = false;

    reader.onload = this._handleReaderLoaded.bind(this);
    reader.readAsDataURL(file);
  }
  _handleReaderLoaded(e) {
    const reader = e.target;
    this.imageSrc = reader.result;
    this.loaded = true;
  }
  handleImageLoad() {
    this.imageLoaded = true;
  }
  handleDrop(e) {
    e.preventDefault();
    this.dragging = false;
    this.handleInputChange(e);
  }
  handleDragLeave() {
    this.dragging = false;
  }
  handleDragEnter() {
    this.dragging = true;
  }
  save() {
    const base64 = this.imageSrc.indexOf(',');
    const selectedLogo = this.imageSrc.toString().substring(base64 + 1, this.imageSrc.length);

    const uploadObj = {
      whiteLabel: {
        whiteLabelId: this.whiteLabelId ? this.whiteLabelId : 0,
        fileName: this.fileName ? this.fileName : this.logoPath, // LogoPath if image already uploaded
        thumbFileName: null,
        base64: selectedLogo ? selectedLogo : '', // empty string if update
        title: this.title ? this.title : '',
        themeId: this.themeId,
        fontFace: this.fontName !== '' ? this.fontName : null,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      }
    };
    this.ngxService.show();
    this.whiteLabelService.updateWhiteLabelDetail(uploadObj).subscribe(data => {
      this.ngxService.hide();
      if (data.status === 'Success') {
        this.toastr.success(this.msgType + MessageConfig.Save, 'Success!');
        this.getAllWhiteLabelDetail();
      } else {
        this.ngxService.hide();
      }
    }, (err) => {
      this.ngxService.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
