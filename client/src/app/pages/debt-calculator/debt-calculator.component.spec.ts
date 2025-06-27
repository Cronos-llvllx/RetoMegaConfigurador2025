import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebtCalculator } from './debt-calculator.component';

describe('DebtCalculatorComponent', () => {
  let component: DebtCalculator;
  let fixture: ComponentFixture<DebtCalculator>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DebtCalculator]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DebtCalculator);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
