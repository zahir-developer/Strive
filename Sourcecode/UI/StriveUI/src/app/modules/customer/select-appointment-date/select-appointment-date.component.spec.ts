import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectAppointmentDateComponent } from './select-appointment-date.component';

describe('SelectAppointmentDateComponent', () => {
  let component: SelectAppointmentDateComponent;
  let fixture: ComponentFixture<SelectAppointmentDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectAppointmentDateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectAppointmentDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
