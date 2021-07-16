import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-view-document',
  templateUrl: './view-document.component.html'
})
export class ViewDocumentComponent implements OnInit {
  passwordForm: FormGroup;
  @Input() employeeId?: any;
  @Input() documentId?: any;
  submitted: boolean;
  contenttype: any;

  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr
    ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.formInitialize();
  }

  formInitialize() {
    this.passwordForm = this.fb.group({
      password: ['', Validators.required]
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  get f() {
    return this.passwordForm.controls;
  }

  viewDocument() {
    this.submitted = true;
    if (this.passwordForm.invalid) {
      return;
    }
    const password = this.passwordForm.value.password;
    this.employeeService.getDocumentById(this.documentId, password).subscribe( res => {
      if (res.status === 'Success' && res.resultData !== 'Invalid Password !!!') {
        const documentDetail = JSON.parse(res.resultData);
        console.log(documentDetail,'hello');
        const base64 = documentDetail.Document.Base64Url;
        const fileGroup = documentDetail.Document.FileType;
        switch (fileGroup) {
          case ".doc":
            this.contenttype = "data:application/msword;base64,";
            break;
          case ".docx":
            this.contenttype = "data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,";
            break;
          case ".xls":
            this.contenttype = "data:application/vnd.ms-excel;base64,";
            break;
          case ".xlsx":
            this.contenttype = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,";
            break;
          case ".pdf":
            this.contenttype = "data:application/pdf;base64,";
            break;
          case ".csv":
            this.contenttype = "data:application/vnd.ms-excel;base64,"
            break;  
        }
        const linkSource = this.contenttype+ base64;
        const downloadLink = document.createElement('a');
        const fileName = 'file'; 
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
        this.activeModal.close();
      } else if (res.status === 'Success' && res.resultData === 'Invalid Password !!!') {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: res.resultData });
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    });
  }

}
