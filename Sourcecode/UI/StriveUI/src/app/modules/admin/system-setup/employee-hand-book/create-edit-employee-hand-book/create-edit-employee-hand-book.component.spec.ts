import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditEmployeeHandBookComponent } from './create-edit-employee-hand-book.component';

describe('CreateEditEmployeeHandBookComponent', () => {
  let component: CreateEditEmployeeHandBookComponent;
  let fixture: ComponentFixture<CreateEditEmployeeHandBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditEmployeeHandBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditEmployeeHandBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
