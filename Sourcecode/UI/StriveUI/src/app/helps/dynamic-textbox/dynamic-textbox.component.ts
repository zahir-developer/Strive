import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dynamic-textbox',
  templateUrl: './dynamic-textbox.component.html',
  styleUrls: ['./dynamic-textbox.component.css']
})
export class DynamicTextboxComponent implements OnInit {
  address = [];
  arrayInputs = [{ controlerInputName1: '' }];
  formName = this.fb.group({
    controllerArray: this.fb.array([])
  });
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.setArrayInputs(this.arrayInputs);
  }
  setArrayInputs(arrayInputs) {
    const arrayFG = arrayInputs.map(address => this.fb.group(address));
    const formArray = this.fb.array(arrayFG);
    this.formName.setControl('controllerArray', formArray);
  }
  addInput() { (this.formName.get('controllerArray') as FormArray).push(this.fb.group({ controlerInputName1: '' })) }

  removeInput(index) {
    console.log(index);
    this.formName.controls.controllerArray['controls'].splice(index, 1);
  }
  submit() {
    this.address = [];
    console.log(this.formName.value);
    this.formName.controls.controllerArray['controls'].forEach(i => {
      this.address.push(i.controls.controlerInputName1.value);
    });
    console.log(this.address);
  }
  // get controllerArray { return this.formName.get('controllerArray'); }

}
