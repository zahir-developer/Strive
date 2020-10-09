import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintCustomerCopyComponent } from './print-customer-copy.component';

describe('PrintCustomerCopyComponent', () => {
  let component: PrintCustomerCopyComponent;
  let fixture: ComponentFixture<PrintCustomerCopyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrintCustomerCopyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintCustomerCopyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
