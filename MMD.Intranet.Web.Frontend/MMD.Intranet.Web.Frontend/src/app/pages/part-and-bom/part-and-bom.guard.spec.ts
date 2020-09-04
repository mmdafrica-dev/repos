import { TestBed, async, inject } from '@angular/core/testing';

import { PartAndBomGuard } from './part-and-bom.guard';

describe('PartAndBomGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PartAndBomGuard]
    });
  });

  it('should ...', inject([PartAndBomGuard], (guard: PartAndBomGuard) => {
    expect(guard).toBeTruthy();
  }));
});
