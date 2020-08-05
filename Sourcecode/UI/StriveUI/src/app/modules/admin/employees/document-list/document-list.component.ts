import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CreateDocumentComponent } from '../../employees/create-document/create-document.component';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit {
  isEditDocument: boolean;
  @Input() employeeId?: any;
  @Input() documentList?: any = [];
  constructor(
    private modalService: NgbModal,
    private employeeService: EmployeeService,
    private confirmationService: ConfirmationUXBDialogService,
  ) { }

  ngOnInit(): void {
    this.isEditDocument = false;
  }

  editDocument() {
    this.isEditDocument = true;
  }

  closeEditDocument() {
    this.isEditDocument = false;
  }

  openCreateDocumentModal() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(CreateDocumentComponent, ngbModalOptions);
    modalRef.componentInstance.employeeId = this.employeeId;
    modalRef.result.then((result) => {
      if (result) {
        this.isEditDocument = false;
        this.getAllDocument();
      }
    });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      console.log(res, 'allDocument');
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
        this.documentList = document.GetAllDocuments;
      }
    });
  }

  deleteDocument(document) {
    this.confirmationService.confirm('Delete Document', 'Are you sure you want to delete this Document? All related information will be deleted and the document cannot be retrieved?', 'Delete', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(document);
        }
      })
      .catch(() => { });
  }

  confirmDelete(document) {
    const docId = document.DocumentId;
    this.employeeService.deleteDocument(docId).subscribe( res => {
      if (res.status === 'Success') {
        this.getAllDocument();
      }
    });
  }

}
