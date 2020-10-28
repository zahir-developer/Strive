import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WhiteLabellingSectionComponent } from './white-labelling-section.component';

describe('WhiteLabellingSectionComponent', () => {
  let component: WhiteLabellingSectionComponent;
  let fixture: ComponentFixture<WhiteLabellingSectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WhiteLabellingSectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WhiteLabellingSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
