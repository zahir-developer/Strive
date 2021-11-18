import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-filter-dashboard',
  templateUrl: './filter-dashboard.component.html'
})
export class FilterDashboardComponent implements OnInit {
  filterForm: FormGroup;
  @Output() filterDashboard = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required]
    });
    this.patchValue();
  }

  patchValue() {
    const currentDate = new Date();
    this.filterForm.patchValue({
      fromDate: currentDate,
      toDate: currentDate
    });
  }

  closeFilterModal() {
    this.activeModal.close();
  }

  apply() {
    const filterObj = {
      fromDate: this.filterForm.value.fromDate,
      toDate: this.filterForm.value.toDate
    };
    this.filterDashboard.emit(filterObj);
    this.activeModal.close();
  }

}
