import { TestBed, inject } from '@angular/core/testing';

import { PartAndBomService } from './part-and-bom.service';

describe('PartAndSearchService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PartAndBomService]
    });
  });

  it('should be created', inject([PartAndBomService], (service: PartAndBomService) => {
    expect(service).toBeTruthy();
  }));
});
