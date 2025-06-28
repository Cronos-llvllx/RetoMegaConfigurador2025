import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxElementsComponent } from './box-elements.component';

describe('BoxElementsComponent', () => {
  let component: BoxElementsComponent;
  let fixture: ComponentFixture<BoxElementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoxElementsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoxElementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
