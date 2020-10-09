import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CustomThemeComponent } from '../custom-theme/custom-theme.component';

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
  constructor(
    private modalService: NgbModal
  ) { }

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
  nightThemeChange( ) {
    document.documentElement.style.setProperty(`--primary-color`, '#632b6c');
    document.documentElement.style.setProperty(`--navigation-color`, '#280f36');
    document.documentElement.style.setProperty(`--secondary-color`, '#c86b98');
    document.documentElement.style.setProperty(`--tertiary-color`, '#f09f9c');
  }
  customTheme() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(CustomThemeComponent, ngbModalOptions);
  }

  fontChange(style) {
    document.documentElement.style.setProperty(`--text-font`, style);
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
  }
}
