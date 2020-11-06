import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyMoneyOwnedComponent } from './monthly-money-owned.component';

describe('MonthlyMoneyOwnedComponent', () => {
  let component: MonthlyMoneyOwnedComponent;
  let fixture: ComponentFixture<MonthlyMoneyOwnedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonthlyMoneyOwnedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyMoneyOwnedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
