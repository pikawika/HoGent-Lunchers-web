import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMerchantDetailComponent } from './admin-merchant-detail.component';

describe('AdminMerchantDetailComponent', () => {
  let component: AdminMerchantDetailComponent;
  let fixture: ComponentFixture<AdminMerchantDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminMerchantDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminMerchantDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
