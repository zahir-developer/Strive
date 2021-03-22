import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealSetupComponent } from './deal-setup.component';

describe('DealSetupComponent', () => {
  let component: DealSetupComponent;
  let fixture: ComponentFixture<DealSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
