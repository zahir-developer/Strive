import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewAppointmentDetailComponent } from './preview-appointment-detail.component';

describe('PreviewAppointmentDetailComponent', () => {
  let component: PreviewAppointmentDetailComponent;
  let fixture: ComponentFixture<PreviewAppointmentDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewAppointmentDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewAppointmentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
