import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SalesComponent implements OnInit {
  services: any;
  filteredItem = [];
  constructor(private membershipService: MembershipService, private salesService: SalesService) { }
  ItemName = '';
  ticketNumber = '';
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
  
  selectedItem(event) {
console.log(event.name)
  }
  getDetailByTicket(){
if (this.ticketNumber !== undefined || this.ticketNumber !== '') {
this.salesService.getItemByTicketNumber(+this.ticketNumber).subscribe(data => {
   console.log(data, 'ticket');
});
}
  }
  filterItem(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.services) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredItem = filtered;
  }
}
