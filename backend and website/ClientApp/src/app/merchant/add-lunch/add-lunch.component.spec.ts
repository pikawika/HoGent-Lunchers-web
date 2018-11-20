import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLunchComponent } from './add-lunch.component';

describe('AddLunchComponent', () => {
  let component: AddLunchComponent;
  let fixture: ComponentFixture<AddLunchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddLunchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddLunchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
