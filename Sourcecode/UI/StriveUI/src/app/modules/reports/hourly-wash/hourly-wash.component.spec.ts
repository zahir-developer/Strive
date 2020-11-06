import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HourlyWashComponent } from './hourly-wash.component';

describe('HourlyWashComponent', () => {
  let component: HourlyWashComponent;
  let fixture: ComponentFixture<HourlyWashComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HourlyWashComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HourlyWashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
