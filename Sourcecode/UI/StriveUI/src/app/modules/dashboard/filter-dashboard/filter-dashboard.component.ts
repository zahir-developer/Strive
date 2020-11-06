import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-filter-dashboard',
  templateUrl: './filter-dashboard.component.html',
  styleUrls: ['./filter-dashboard.component.css']
})
export class FilterDashboardComponent implements OnInit {
  filterForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required]
    });
  }

  closeFilterModal() {
    this.activeModal.close();
  }

}
