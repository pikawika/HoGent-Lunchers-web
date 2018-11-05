import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LunchDetailComponent } from './lunch-detail.component';

describe('LunchDetailComponent', () => {
  let component: LunchDetailComponent;
  let fixture: ComponentFixture<LunchDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LunchDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LunchDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
