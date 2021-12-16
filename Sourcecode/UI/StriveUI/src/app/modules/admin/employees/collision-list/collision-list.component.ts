import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeCollisionComponent } from '../../employees/employee-collision/employee-collision.component';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-collision-list',
  templateUrl: './collision-list.component.html'
})
export class CollisionListComponent implements OnInit {
  @Input() employeeId?: any;
  @Input() employeeCollision?: any;
  @Input() actionType?: any;
  @Input() isModal?: any;
  @Output() public reloadCollisionGrid = new EventEmitter();
  isEditCollision: boolean;
  totalAmount: any = 0;
  collisionList: any = [];
  isCollisionCollapsed = false;
  showCloseButton: boolean;
  constructor(
    private employeeService: EmployeeService,
    private modalService: NgbModal,
    private confirmationService: ConfirmationUXBDialogService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.isEditCollision = false;
    if (this.isModal !== undefined) {
      this.showCloseButton = true;
    } else {
      this.showCloseButton = false;
    }
    this.employeeDetail();
  }

  editCollision() {
    this.isEditCollision = true;
  }

  cancelEditCollision() {
    this.isEditCollision = false;
  }

  collistionGrid() {
    this.totalAmount = 0;
    this.collisionList = [];
    if (this.employeeCollision.length > 0) {
      this.collisionList = this.employeeCollision;
      if (this.collisionList.length > 0) {
        this.collisionList.forEach(item => {
          this.totalAmount = this.totalAmount + item.Amount;
        });
      }
    }
  }

  employeeDetail() {
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        this.employeeCollision = [];
        if (employees.Employee.EmployeeCollision !== null) {
          this.employeeCollision = employees.Employee.EmployeeCollision;
        }
        this.collistionGrid();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  deleteCollision(collision) {
    if (!this.isEditCollision && this.actionType === 'view') {
      return;
    }
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
    this.spinner.show();
    this.employeeService.deleteCollision(collisionId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Collision.Delete, 'Success!');
        this.employeeDetail();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  getAllCollision() {
    this.employeeService.getAllCollision(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        this.employeeCollision = [];
        this.collisionList = [];
        if (employeesCollison.Collision.length > 0) {
          this.employeeCollision = employeesCollison.Collision;
          this.collistionGrid();
        }
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  closeModal() {
    this.modalService.dismissAll();
  }

  updateCollision(collision) {
    if (!this.isEditCollision && this.actionType === 'view') {
      return;
    }
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
        this.employeeDetail();
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
        this.employeeDetail();
      }
    });
  }

  collisionCollapsed() {
    this.isCollisionCollapsed = !this.isCollisionCollapsed;
  }

  reloadGrid() {
    this.reloadCollisionGrid.emit();
  }


}
