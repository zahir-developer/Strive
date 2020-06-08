import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormArray, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup

  constructor() { }

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      "hobby": new FormArray([])
    });
  }

  AddHobby(): void {
    const ctrl = new FormControl(null, Validators.required);
    (<FormArray>this.signupForm.get("hobby")).push(ctrl)
  }

  GetHobbyControls() {
    return (this.signupForm.get('hobby') as FormArray).controls;
  }

}
