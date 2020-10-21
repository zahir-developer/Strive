import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutGridComponent } from './checkout-grid.component';

describe('CheckoutGridComponent', () => {
  let component: CheckoutGridComponent;
  let fixture: ComponentFixture<CheckoutGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckoutGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckoutGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
