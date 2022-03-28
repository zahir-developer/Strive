import { Component, OnInit, Output, EventEmitter, Input, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { StateService } from '../../services/common-service/state.service';
import { MessageConfig } from '../../services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-state-dropdown',
  templateUrl: './state-dropdown.component.html'
})
export class StateDropdownComponent implements OnInit, AfterViewChecked {
  stateList = [];
  submitted: boolean;
  @Output() stateId = new EventEmitter();
  @Input() selectedStateId?: any;
  @Input() isView: any;
  states: any;
  state: { name: any; value: any; };
  stateValueSelection: boolean = false;
  constructor(private stateService: StateService, private cdRef: ChangeDetectorRef,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.submitted = false;
    this.stateValueSelection = false;
    this.getstatesList();
  }
  ngAfterViewChecked(){
    if (this.selectedStateId !== undefined) {
      this.cdRef.detectChanges();
    }
  }
  getstatesList() {
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
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  stateSelection(event) {
    this.stateId.emit(event.value.value);
    this.stateValueSelection =true;
  }

  setValue() {
    if (this.selectedStateId !== undefined) {
      this.stateValueSelection =true;

      this.stateList.map(item => {
        if(item.value ==  this.selectedStateId){
       this.state = {
            name: item.name,
            value: item.value
          };
        }
       
      });
    }
  }
}
