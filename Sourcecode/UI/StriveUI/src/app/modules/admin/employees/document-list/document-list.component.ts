import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CreateDocumentComponent } from '../../employees/create-document/create-document.component';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ViewDocumentComponent } from '../../employees/view-document/view-document.component';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.css']
})
export class DocumentListComponent implements OnInit {
  isEditDocument: boolean;
  @Input() employeeId?: any;
  @Input() documentList?: any = [];
  @Input() actionType?: any;
  @Input() isModal?: any;
  showCloseButton: boolean;
  documentId: any;
  isDocumentCollapsed = false;
  employeeDocument = [];
  constructor(
    private modalService: NgbModal,
    private employeeService: EmployeeService,
    private confirmationService: ConfirmationUXBDialogService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.isEditDocument = false;
    this.showCloseButton = false;
    this.employeeDetail();
    if (this.isModal !== undefined) {
      this.showCloseButton = true;
    } else {
      this.showCloseButton = false;
    }
  }

  employeeDetail() {
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        this.documentList = [];
        if (employees.Employee.EmployeeDocument !== null) {
          this.documentList = employees.Employee.EmployeeDocument;
        }
      }
    });
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
        this.employeeDetail();
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

  deleteDocument(documentList) {
    if (!this.isEditDocument && this.actionType === 'view') {
      return;
    }
    this.confirmationService.confirm('Delete Document', 'Are you sure you want to delete this Document? All related information will be deleted and the document cannot be retrieved?', 'Delete', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(documentList);
        }
      })
      .catch(() => { });
  }

  confirmDelete(documentList) {
    const docId = documentList.EmployeeDocumentId;
    this.employeeService.deleteDocument(docId).subscribe( res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Document Deleted Successfully!' });
        this.employeeDetail();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  viewDocument(document) {
    if (!this.isEditDocument && this.actionType === 'view') {
      return;
    }
    this.documentId = document.EmployeeDocumentId;
    if (document.IsPasswordProtected) {
      const ngbModalOptions: NgbModalOptions = {
        backdrop: 'static',
        keyboard: false,
        size: 'lg'
      };
      const modalRef = this.modalService.open(ViewDocumentComponent, ngbModalOptions);
      modalRef.componentInstance.employeeId = this.employeeId;
      modalRef.componentInstance.documentId = this.documentId;
    } else  {
      this.downloadDocument(document.EmployeeDocumentId);
    }
  }

  downloadDocument(documentId) {
    this.employeeService.getDocumentById(this.documentId, 'string').subscribe( res => {
      if (res.status === 'Success') {
        const documentDetail = JSON.parse(res.resultData);
        console.log(documentDetail);
        const base64 = documentDetail.Document.Base64Url;
        const linkSource = 'data:application/pdf;base64,' + base64;
        const downloadLink = document.createElement('a');
        const fileName = documentDetail.Document.FileName;
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
      }
    });
  }

  closeModal() {
    this.modalService.dismissAll();
  }

  documentCollapsed() {
    this.isDocumentCollapsed = !this.isDocumentCollapsed;
  }

}
