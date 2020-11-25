import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BonusSetupComponent } from './bonus-setup.component';

describe('BonusSetupComponent', () => {
  let component: BonusSetupComponent;
  let fixture: ComponentFixture<BonusSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BonusSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BonusSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
