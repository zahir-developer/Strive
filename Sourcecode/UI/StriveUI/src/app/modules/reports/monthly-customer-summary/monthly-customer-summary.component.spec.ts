import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyCustomerSummaryComponent } from './monthly-customer-summary.component';

describe('MonthlyCustomerSummaryComponent', () => {
  let component: MonthlyCustomerSummaryComponent;
  let fixture: ComponentFixture<MonthlyCustomerSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonthlyCustomerSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyCustomerSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
