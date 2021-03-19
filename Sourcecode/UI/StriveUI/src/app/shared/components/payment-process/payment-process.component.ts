import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaymentService } from '../../services/data-service/payment.service';

@Component({
  selector: 'app-payment-process',
  templateUrl: './payment-process.component.html',
  styleUrls: ['./payment-process.component.css']
})
export class PaymentProcessComponent implements OnInit {

  constructor(
    private activeModal: NgbActiveModal,
    private paymentService: PaymentService
  ) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  process() {
    const obj = {

    };
    this.paymentService.post(obj).subscribe(res => {
      
    });
  }

}
