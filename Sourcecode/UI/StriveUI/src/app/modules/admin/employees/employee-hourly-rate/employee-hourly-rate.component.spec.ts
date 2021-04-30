import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeHourlyRateComponent } from './employee-hourly-rate.component';

describe('EmployeeHourlyRateComponent', () => {
  let component: EmployeeHourlyRateComponent;
  let fixture: ComponentFixture<EmployeeHourlyRateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeHourlyRateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeHourlyRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
