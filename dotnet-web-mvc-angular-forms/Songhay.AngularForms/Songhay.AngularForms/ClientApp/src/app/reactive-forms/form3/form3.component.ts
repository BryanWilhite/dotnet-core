import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl
} from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

import { FormsDataService } from '../state/forms-data.service';

@Component({
  selector: 'app-form3',
  templateUrl: './form3.component.html',
  styleUrls: ['./form3.component.css']
})
export class Form3Component implements OnInit, OnDestroy {
  componentForm: FormGroup;
  success = false;

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private reactiveFormService: FormsDataService
  ) {}

  ngOnDestroy(): void {
    for (const sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.componentForm = this.fb.group({
      password: [
        '',
        [
          Validators.required,
          Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
        ]
      ],
      age: [
        null,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.min(18),
          Validators.max(65)
        ]
      ]
    });

    const sub = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub);
  }

  get password(): AbstractControl {
    return this.componentForm.get('password');
  }

  get age(): AbstractControl {
    return this.componentForm.get('age');
  }

  next() {
    this.success = true;

    console.log({ model: this.reactiveFormService.getStateOfStore() });
  }

  previous(): void {
    this.router.navigate(['wizard/step-2-of-3']);
  }
}
