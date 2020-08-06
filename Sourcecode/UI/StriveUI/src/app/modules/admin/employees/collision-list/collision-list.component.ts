import { Component, OnInit, Input } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeCollisionComponent } from '../../employees/employee-collision/employee-collision.component';

@Component({
  selector: 'app-collision-list',
  templateUrl: './collision-list.component.html',
  styleUrls: ['./collision-list.component.css']
})
export class CollisionListComponent implements OnInit {
  @Input() employeeId?: any;
  @Input() employeeCollision?: any = [];
  @Input() actionType?: any;
  isEditCollision: boolean;
  totalAmount: any = 0;
  collisionList: any = [];
  constructor(
    private employeeService: EmployeeService,
    private modalService: NgbModal,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit(): void {
    this.isEditCollision = false;
    console.log(this.employeeCollision, 'collision');
    this.getAllCollision();
  }

  editCollision() {
    this.isEditCollision = true;
  }

  cancelEditCollision() {
    this.isEditCollision = false;
  }

  collistionGrid() {
    this.totalAmount = 0;
    if (this.employeeCollision.length > 0) {
      this.collisionList = this.employeeCollision[0].LiabilityDetail;
      if (this.collisionList.length > 0) {
        this.collisionList.forEach(item => {
          this.totalAmount = this.totalAmount + item.Amount;
        });
      }
    }
  }

  deleteCollision(collision) {
    this.confirmationService.confirm('Delete Employee Collision ', 'Are you sure you want to delete this collision? All related information will be deleted and the collision cannot be retrieved?', 'Delete', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(collision);
        }
      })
      .catch(() => { });
  }

  confirmDelete(collision) {
    const collisionId = collision.LiabilityId;
    this.employeeService.deleteCollision(collisionId).subscribe(res => {
      if (res.status === 'Success') {
        this.getAllCollision();
      }
    });
  }

  getAllCollision() {
    this.employeeService.getAllCollision(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        console.log(employeesCollison, 'employeDeatil');
        if (employeesCollison.CollisionDetailOfEmployee.length > 0) {
          this.employeeCollision = employeesCollison.CollisionDetailOfEmployee;
          this.collistionGrid();
        }
      }
    });
  }

  updateCollision(collision) {
    const collisionId = collision.LiabilityId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(EmployeeCollisionComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = this.employeeId;
    modalRef.componentInstance.collisionId = collisionId;
    modalRef.componentInstance.mode = 'edit';
    modalRef.result.then((result) => {
      if (result) {
        this.isEditCollision = false;
        this.getAllCollision();
      }
    });
  }

  openCreateCollisionModal() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(EmployeeCollisionComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = this.employeeId;
    modalRef.componentInstance.mode = 'create';
    modalRef.result.then((result) => {
      if (result) {
        this.isEditCollision = false;
        this.getAllCollision();
      }
    });
  }


}
