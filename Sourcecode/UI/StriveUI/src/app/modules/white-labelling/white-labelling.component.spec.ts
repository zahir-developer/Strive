import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WhiteLabellingComponent } from './white-labelling.component';

describe('WhiteLabellingComponent', () => {
  let component: WhiteLabellingComponent;
  let fixture: ComponentFixture<WhiteLabellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WhiteLabellingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WhiteLabellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
