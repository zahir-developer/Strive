import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomThemeComponent } from './custom-theme.component';

describe('CustomThemeComponent', () => {
  let component: CustomThemeComponent;
  let fixture: ComponentFixture<CustomThemeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomThemeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomThemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
