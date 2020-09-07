import { Component, OnInit } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {
  services: any;
  filteredItem = [];
  constructor(private membershipService: MembershipService) { }
  ItemName = '';
  ngOnInit(): void {
    this.getAllService();
  }
  getAllService() {
    this.membershipService.getMembershipService().subscribe(data => {
      if (data.status === 'Success') {
        const services = JSON.parse(data.resultData);
        if (services.ServicesWithPrice !== null && services.ServicesWithPrice.length > 0) {
          this.services = services.ServicesWithPrice.map(item => {
            return {
              id: item.ServiceId,
              name: item.ServiceName.trim()
            };
          });
          console.log(this.services);
        }
      }
    });
  }
  filterItem(event) {

  }
  selectedItem(event) {

  }
}
