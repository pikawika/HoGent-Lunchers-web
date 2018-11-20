import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LunchMerchantComponent } from './lunch-merchant.component';

describe('LunchMerchantComponent', () => {
  let component: LunchMerchantComponent;
  let fixture: ComponentFixture<LunchMerchantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LunchMerchantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LunchMerchantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
