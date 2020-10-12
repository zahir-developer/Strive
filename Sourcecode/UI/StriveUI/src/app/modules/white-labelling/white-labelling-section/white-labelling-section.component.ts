import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CustomThemeComponent } from '../custom-theme/custom-theme.component';
import { WhiteLabelService } from 'src/app/shared/services/data-service/white-label.service';

@Component({
  selector: 'app-white-labelling-section',
  templateUrl: './white-labelling-section.component.html',
  styleUrls: ['./white-labelling-section.component.css']
})
export class WhiteLabellingSectionComponent implements OnInit {
  sunshineTheme: any;
  title = '';
  activeColor = 'green';
  baseColor = '#ccc';
  overlayColor = 'rgba(255,255,255,0.5)';
  dragging = false;
  loaded = false;
  imageLoaded = false;
  imageSrc = '';
  showDialog: boolean;
  colorTheme: any = [];
  colorSelection: any;
  themeId: any;
  constructor(
    private modalService: NgbModal,
    private whiteLabelService: WhiteLabelService
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
    this.getAllWhiteLabelDetail();
  }

  themeChange(theme) {
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

  customTheme() {
    this.showDialog = true;
  }

  closeColorPopup() {
    this.showDialog = false;
  }

  fontChange(style) {
    document.documentElement.style.setProperty(`--text-font`, style);
  }

  getAllWhiteLabelDetail() {
    this.whiteLabelService.getAllWhiteLabelDetail().subscribe( res => {
      if (res.status === 'Success') {
        const label = JSON.parse(res.resultData);
        console.log(label, 'white');
        this.colorTheme = label.WhiteLabelling.Theme;
      }
    });
  }

  handleInputChange(e) {
    const file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
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
    console.log(this.title, 'title');
    console.log(this.imageSrc, 'base64');
    const themeObj = {
      whiteLabelId: 0,
      logoPath: '',
      fileName: '',
      thumbFileName: '',
      base64: '',
      title: '',
      themeId: this.themeId,
      fontFace: '',
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    };
    this.whiteLabelService.addWhiteLabelDetail(themeObj).subscribe( res => {
      if (res.status === 'Success') {
        
      }
    });
  }
}
