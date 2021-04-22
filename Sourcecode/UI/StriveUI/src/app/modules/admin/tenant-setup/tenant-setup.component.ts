import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tenant-setup',
  templateUrl: './tenant-setup.component.html',
  styleUrls: ['./tenant-setup.component.css']
})
export class TenantSetupComponent implements OnInit {
  search = '';
  showDialog = false;
  constructor() { }

  ngOnInit(): void {
  }

  addTenant() {
    this.showDialog = true;
  }

}
