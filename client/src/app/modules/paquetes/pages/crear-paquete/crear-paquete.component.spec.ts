import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearPaqueteComponent } from './crear-paquete.component';

describe('CrearPaqueteComponent', () => {
  let component: CrearPaqueteComponent;
  let fixture: ComponentFixture<CrearPaqueteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrearPaqueteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrearPaqueteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
