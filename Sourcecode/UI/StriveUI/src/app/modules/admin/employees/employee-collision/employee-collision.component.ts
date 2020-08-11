import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import * as moment from 'moment';

@Component({
  selector: 'app-employee-collision',
  templateUrl: './employee-collision.component.html',
  styleUrls: ['./employee-collision.component.css']
})
export class EmployeeCollisionComponent implements OnInit {

  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private employeeService: EmployeeService) { }
  @Input() public employeeId?: any;
  @Input() public collisionId?: any;
  @Input() public mode?: any;
  collisionForm: FormGroup;
  collisionDetail: any;
  ngOnInit(): void {
    console.log(this.employeeId, 'employeeid');
    this.collisionForm = this.fb.group({
      dateOfCollision: [''],
      amount: [''],
      reason: ['']
    });
    if (this.mode === 'edit') {
      this.getCollisionDetail();
    }
  }

  closeModal() {
    this.activeModal.close();
  }

  getCollisionDetail() {
    this.employeeService.getDetailCollision(this.collisionId).subscribe(res => {
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        console.log(employeesCollison.CollisionDetail);
        if (employeesCollison.CollisionDetail.length > 0) {
          const detail = employeesCollison.CollisionDetail[0].LiabilityDetail[0];
          this.collisionDetail = detail;
          this.setValue(detail);
        }
      }
    });
  }

  setValue(detail) {
    this.collisionForm.patchValue({
      dateOfCollision: moment(detail.CreatedDate).toDate(),
      amount: detail.Amount.toFixed(2),
      reason: detail.Description
    });
  }

  saveCollision() {
    const liabilityDetailObj = {
      liabilityDetailId: this.mode === 'edit' ? this.collisionDetail.LiabilityDetailId : 0,
      liabilityId: this.mode === 'edit' ? this.collisionDetail.LiabilityId : 0,
      liabilityDetailType: 1,
      amount: +this.collisionForm.value.amount,
      paymentType: 1,
      documentPath: 'string',
      description: this.collisionForm.value.reason,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const liabilityObj = {
      liabilityId: this.mode === 'edit' ? this.collisionDetail.LiabilityId : 0,
      employeeId: this.employeeId,
      liabilityType: 103,
      liabilityDescription: 'string',
      productId: 2,
      totalAmount: 0,
      status: 0,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const finalObj = {
      employeeLiability: liabilityObj,
      employeeLiabilityDetail: liabilityDetailObj
    };
    if (this.mode === 'create') {
      this.employeeService.saveCollision(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.activeModal.close(true);
        }
      });
    } else {
      this.employeeService.updateCollision(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.activeModal.close(true);
        }
      });
    }
  }

}
