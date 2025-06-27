import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputbarComponent } from './inputbar.component';

describe('InputBarComponent', () => {
  let component: InputbarComponent;
  let fixture: ComponentFixture<InputbarComponent>;
  let currentValue: string | undefined = '';

  beforeEach(async () => {
    // Inicializa valores.
    currentValue = '';

    await TestBed.configureTestingModule({
      imports: [InputbarComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(InputbarComponent);
    component = fixture.componentInstance;

    // Asigna valores por defecto al componente.
    component.onChange = (input?: string) => currentValue = input;
    component.minLength = 1;
    component.maxLength = 100;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should detect changes', () => {
    const newValue = 'nuevo valor';
    // Mock del evento;
    const e = { target: { value: newValue } } as unknown as Event;

    component.onInputChange(e);

    expect(currentValue).toEqual(newValue);
  });

  it('should not detect changes since component is disabled', () => {
    component.disabled = true;

    const newValue = 'nuevo valor';
    // Mock del evento;
    const e = { target: { value: newValue }, preventDefault: () => { } } as unknown as Event;

    component.onInputChange(e);

    expect(currentValue).not.toEqual(newValue);
  });
});
