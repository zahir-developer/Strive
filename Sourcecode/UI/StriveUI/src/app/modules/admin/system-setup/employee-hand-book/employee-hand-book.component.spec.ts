import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeHandBookComponent } from './employee-hand-book.component';

describe('EmployeeHandBookComponent', () => {
  let component: EmployeeHandBookComponent;
  let fixture: ComponentFixture<EmployeeHandBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeHandBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeHandBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
