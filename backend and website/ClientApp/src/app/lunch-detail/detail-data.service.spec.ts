import { TestBed, inject } from '@angular/core/testing';

import { DetailDataService } from './detail-data.service';

describe('DetailDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DetailDataService]
    });
  });

  it('should be created', inject([DetailDataService], (service: DetailDataService) => {
    expect(service).toBeTruthy();
  }));
});
