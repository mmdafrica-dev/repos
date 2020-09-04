import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BomDetailComponent } from './detail.component';

describe('DetailComponent', () => {
    let component: BomDetailComponent;
    let fixture: ComponentFixture<BomDetailComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [BomDetailComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BomDetailComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
