import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SitehighlighterComponent } from './sitehighlighter.component';

describe('SitehighlighterComponent', () => {
  let component: SitehighlighterComponent;
  let fixture: ComponentFixture<SitehighlighterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SitehighlighterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SitehighlighterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
