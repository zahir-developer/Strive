import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayrollsGridComponent } from './payrolls-grid.component';

describe('PayrollsGridComponent', () => {
  let component: PayrollsGridComponent;
  let fixture: ComponentFixture<PayrollsGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayrollsGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayrollsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
