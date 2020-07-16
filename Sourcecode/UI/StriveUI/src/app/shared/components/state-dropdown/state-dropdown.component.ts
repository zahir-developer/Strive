import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { StateService } from '../../services/common-service/state.service';

@Component({
  selector: 'app-state-dropdown',
  templateUrl: './state-dropdown.component.html',
  styleUrls: ['./state-dropdown.component.css']
})
export class StateDropdownComponent implements OnInit {
  stateList = [];
  state = '';
  submitted: boolean;
  @Output() stateId = new EventEmitter();
  constructor(private stateService: StateService) { }

  ngOnInit(): void {
    this.submitted = false;
    this.getstatiesList();
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
    }, (err) => {
    });
  }
  stateSelection(event) {
this.stateId.emit(event);
  }
}
