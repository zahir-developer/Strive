import { Component, OnInit } from '@angular/core';
import { add } from 'ngx-bootstrap/chronos';

@Component({
  selector: 'app-washes-list',
  templateUrl: './washes-list.component.html',
  styleUrls: ['./washes-list.component.css']
})
export class WashesListComponent implements OnInit {
  headerData: string;
  selectedData: any;
  isEdit: boolean;
  showDialog: boolean;
  isView: boolean;

  constructor() { }

  ngOnInit(): void {
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
    }
    this.showDialog = event.isOpenPopup;
  }

  add(data, washDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Service';
      this.selectedData = washDetails;
      this.isEdit = false;
      this.isView = false;
      this.showDialog = true;
    } else if (data === 'edit') {
      this.headerData = 'Edit Service';
      this.selectedData = washDetails;
      this.isEdit = false;
      this.isView = false;
      this.showDialog = true;
    } else {
      this.headerData = 'View Service';
      this.selectedData = washDetails;
      this.isEdit = true;
      this.isView = true;
      this.showDialog = true;
    }
  }

}
