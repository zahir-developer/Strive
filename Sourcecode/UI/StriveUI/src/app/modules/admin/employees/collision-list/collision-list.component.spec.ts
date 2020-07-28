import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollisionListComponent } from './collision-list.component';

describe('CollisionListComponent', () => {
  let component: CollisionListComponent;
  let fixture: ComponentFixture<CollisionListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollisionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollisionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
