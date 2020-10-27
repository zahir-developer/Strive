import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyTipComponent } from './monthly-tip.component';

describe('MonthlyTipComponent', () => {
  let component: MonthlyTipComponent;
  let fixture: ComponentFixture<MonthlyTipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MonthlyTipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyTipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
