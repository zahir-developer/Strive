import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WashesListComponent } from './washes-list.component';

describe('WashesListComponent', () => {
  let component: WashesListComponent;
  let fixture: ComponentFixture<WashesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WashesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WashesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
