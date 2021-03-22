import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionLogoutComponent } from './session-logout.component';

describe('SessionLogoutComponent', () => {
  let component: SessionLogoutComponent;
  let fixture: ComponentFixture<SessionLogoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SessionLogoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SessionLogoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
