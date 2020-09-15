import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {
  editItemForm: FormGroup;
  constructor(private fb: FormBuilder, private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.formInit();
  }
  formInit() {
    this.editItemForm = this.fb.group({
itemName: [''],
quantity: [''],
price: ['']
    });
  }
  closeModal() {
this.activeModal.close();
  }
  saveItem() {
  }
}
