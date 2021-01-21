import { Component, OnInit, Output, EventEmitter, Input, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { StateService } from '../../services/common-service/state.service';

@Component({
  selector: 'app-state-dropdown',
  templateUrl: './state-dropdown.component.html',
  styleUrls: ['./state-dropdown.component.css']
})
export class StateDropdownComponent implements OnInit, AfterViewChecked {
  stateList = [];
  state = '';
  submitted: boolean;
  @Output() stateId = new EventEmitter();
  @Input() selectedStateId?: any;
  @Input() isView: any;
  constructor(private stateService: StateService, private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.submitted = false;
    this.getstatiesList();
  }
  ngAfterViewChecked(){
    if (this.selectedStateId !== undefined) {
      this.cdRef.detectChanges();
    }
  }
  getstatiesList() {
    this.stateService.getStatesList().subscribe(data => {
      const state = JSON.parse(data.resultData);
      this.stateList = state.Codes.map(item => {
        return {
          name: item.CodeValue,
          value: item.CodeId
        };
      });
      this.setValue();
    }, (err) => {
    });
  }
  stateSelection(event) {
    this.stateId.emit(event);
  }

  setValue() {
    if (this.selectedStateId !== undefined) {
      this.state = this.selectedStateId;
    }
  }
}
