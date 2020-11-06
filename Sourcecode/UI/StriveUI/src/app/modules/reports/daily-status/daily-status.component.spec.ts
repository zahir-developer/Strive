import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyStatusComponent } from './daily-status.component';

describe('DailyStatusComponent', () => {
  let component: DailyStatusComponent;
  let fixture: ComponentFixture<DailyStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DailyStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
