import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPastNotesComponent } from './view-past-notes.component';

describe('ViewPastNotesComponent', () => {
  let component: ViewPastNotesComponent;
  let fixture: ComponentFixture<ViewPastNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewPastNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPastNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
