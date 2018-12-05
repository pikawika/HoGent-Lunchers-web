import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminLunchesComponent } from './admin-lunches.component';

describe('AdminLunchesComponent', () => {
  let component: AdminLunchesComponent;
  let fixture: ComponentFixture<AdminLunchesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminLunchesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminLunchesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
