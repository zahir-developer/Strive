import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdleLockoutComponent } from './idle-lockout.component';

describe('IdleLockoutComponent', () => {
  let component: IdleLockoutComponent;
  let fixture: ComponentFixture<IdleLockoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IdleLockoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdleLockoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
