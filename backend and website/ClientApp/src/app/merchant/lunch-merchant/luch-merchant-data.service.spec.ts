import { TestBed, inject } from '@angular/core/testing';

import { LunchMerchantDataService } from './lunch-merchant-data.service';

describe('MerchantLunchDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LunchMerchantDataService]
    });
  });

  it('should be created', inject([LunchMerchantDataService], (service: LunchMerchantDataService) => {
    expect(service).toBeTruthy();
  }));
});
