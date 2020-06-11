import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormArray, FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup;
  forbiddenNames = ["John", "Steve"];
  constructor() { }

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      "hobby": new FormArray([], null, [this.ForbiddenHobby]),
      "userName": new FormControl(null, [Validators.required, this.CheckNames.bind(this)]),
      "unValidation": new FormControl(null)
    }),
      this.signupForm.valueChanges.subscribe(
        (value) => console.log(value)
      );
  }

  AddHobby(): void {
    const ctrl = new FormControl(null, Validators.required);
    (<FormArray>this.signupForm.get("hobby")).push(ctrl)
  }

  GetHobbyControls() {
    return (this.signupForm.get('hobby') as FormArray).controls;
  }

  CheckNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenNames.indexOf(control.value) !== -1) {
      return { 'nameIsForbidden': true };
    }
    return null;
  }

  ForbiddenHobby(control: FormControl): Promise<any> | Observable<any> {
    const promise = new Promise<any>((resolve, reject) => {
      setTimeout(() => {
        if (control.value === "reading") {
          resolve({ 'hobbynotallowed': true });
        } else {
          resolve(null);
        }
      }, 1500);
    });
    return promise;
  }



}
