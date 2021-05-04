import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleGiftCardComponent } from './sale-gift-card.component';

describe('SaleGiftCardComponent', () => {
  let component: SaleGiftCardComponent;
  let fixture: ComponentFixture<SaleGiftCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaleGiftCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleGiftCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
