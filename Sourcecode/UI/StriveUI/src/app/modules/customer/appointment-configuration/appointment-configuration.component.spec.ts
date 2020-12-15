import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentConfigurationComponent } from './appointment-configuration.component';

describe('AppointmentConfigurationComponent', () => {
  let component: AppointmentConfigurationComponent;
  let fixture: ComponentFixture<AppointmentConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppointmentConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppointmentConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
