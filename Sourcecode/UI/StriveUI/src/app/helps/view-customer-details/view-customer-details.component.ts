import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';

@Component({
  selector: 'app-view-customer-details',
  templateUrl: './view-customer-details.component.html',
  styleUrls: ['./view-customer-details.component.css']
})
export class ViewCustomerDetailsComponent implements OnInit {
  cars: any;
  showDialog = false;
  selectedData: any;
  constructor(private crudService: CrudOperationService) { }

  ngOnInit() {
    this.cars = this.crudService.getCustomerDetails();
  }
  addCustomer() {
    this.showDialog = true;
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    const index = this.cars.map(x => x.id).indexOf(data.id);
    if (index > -1) {
      this.cars.splice(index, 1);
    }
  }
}
