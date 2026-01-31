import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecureTest } from './secure-test';

describe('SecureTest', () => {
  let component: SecureTest;
  let fixture: ComponentFixture<SecureTest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SecureTest]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SecureTest);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
