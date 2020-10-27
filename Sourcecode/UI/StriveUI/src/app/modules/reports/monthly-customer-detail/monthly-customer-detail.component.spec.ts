import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyCustomerDetailComponent } from './monthly-customer-detail.component';

describe('MonthlyCustomerDetailComponent', () => {
  let component: MonthlyCustomerDetailComponent;
  let fixture: ComponentFixture<MonthlyCustomerDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonthlyCustomerDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyCustomerDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
