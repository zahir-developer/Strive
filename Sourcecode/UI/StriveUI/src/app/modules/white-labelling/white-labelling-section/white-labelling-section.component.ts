import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CustomThemeComponent } from '../custom-theme/custom-theme.component';
import { WhiteLabelService } from 'src/app/shared/services/data-service/white-label.service';
import { LogoService } from 'src/app/shared/services/common-service/logo.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
  fontFace = '';
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
  constructor(
    private modalService: NgbModal,
    private whiteLabelService: WhiteLabelService,
    private logoService: LogoService,
    private ngxService: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.sunshineTheme = {
      primarycolor: '#FEB83F',
      navigationcolor: '#FC6A0C',
      secondarycolor: '#FFF997',
      tertiarycolor: '#FD8B20'
    };
    this.getAllWhiteLabelDetail();
  }

  themeChange(theme) {
    document.documentElement.style.setProperty(`--primary-color`, theme.PrimaryColor);
    document.documentElement.style.setProperty(`--navigation-color`, theme.NavigationColor);
    document.documentElement.style.setProperty(`--secondary-color`, theme.SecondaryColor);
    document.documentElement.style.setProperty(`--tertiary-color`, theme.TertiaryColor);
    document.documentElement.style.setProperty(`--body-color`, theme.BodyColor);
  }

  defaultTheme() {
    document.documentElement.style.setProperty(`--primary-color`, '#1DC5B3');
    document.documentElement.style.setProperty(`--navigation-color`, '#24489A');
    document.documentElement.style.setProperty(`--secondary-color`, '#F2FCFE');
    document.documentElement.style.setProperty(`--tertiary-color`, '#10B7A5');
    document.documentElement.style.setProperty(`--body-color`, '#F2FCFE');
  }

  customTheme() {
    this.showDialog = true;
  }

  closeColorPopup() {
    this.showDialog = false;
  }

  fontChange(style) {
    this.fontFace = style;
    document.documentElement.style.setProperty(`--text-font`, style);
  }

  getAllWhiteLabelDetail() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
      if (res.status === 'Success') {
        const label = JSON.parse(res.resultData);
        this.colorTheme = label.WhiteLabelling.Theme;
        if (label?.WhiteLabelling?.WhiteLabel !== undefined) {
          this.whiteLabelId = label.WhiteLabelling.WhiteLabel?.WhiteLabelId;
          this.fontFace = label.WhiteLabelling.WhiteLabel?.FontFace;
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          this.logoPath = label.WhiteLabelling.WhiteLabel?.LogoPath;
          this.fontChange(label.WhiteLabelling.WhiteLabel?.FontFace);
        }
      }
    });
  }

  handleInputChange(e) {
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
        logoPath: this.logoPath !== '' ? this.logoPath : null,
        fileName: this.fileName ? this.fileName : null, // LogoPath if image already uploaded
        thumbFileName: null,
        base64: selectedLogo ? selectedLogo : '', // empty string if update
        title: this.title ? this.title : '',
        themeId: 1,
        fontFace: this.fontFace !== '' ? this.fontFace : null,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      }
    };
    this.ngxService.show();
    this.whiteLabelService.uploadWhiteLabel(uploadObj).subscribe(data => {
      this.ngxService.hide();
      if (data.status === 'Success') {
        this.getAllWhiteLabelDetail();
      } else {
        this.ngxService.hide();
      }
    }, (err) => {
      this.ngxService.show();
    });
  }
}
