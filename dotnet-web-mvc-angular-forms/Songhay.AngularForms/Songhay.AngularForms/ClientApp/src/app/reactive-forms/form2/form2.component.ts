import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormGroup,
  FormBuilder,
  FormArray,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

import { FormsDataService } from '../state/forms-data.service';

@Component({
  selector: 'app-form2',
  templateUrl: './form2.component.html',
  styleUrls: ['./form2.component.css']
})
export class Form2Component implements OnInit, OnDestroy {
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

  ngOnInit() {
    const state = this.reactiveFormService.getStateOfStore();
    const phones = state.phones
      ? state.phones.map(i =>
          this.fb.group({
            area: [i.area, Validators.required],
            prefix: [i.prefix, Validators.required],
            line: [i.line, Validators.required]
          })
        )
      : [];

    this.componentForm = this.fb.group({
      phones: this.fb.array(phones)
    });

    const sub = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub);
  }

  get phoneForms(): FormArray {
    return this.componentForm.get('phones') as FormArray;
  }

  addPhone(): void {
    const phone: FormGroup = this.fb.group({
      area: [null, Validators.required],
      prefix: [null, Validators.required],
      line: [null, Validators.required]
    });

    this.phoneForms.push(phone);
  }

  getPhone(
    i: number,
    propertyName: 'area' | 'prefix' | 'line'
  ): AbstractControl | null {
    if (!this.phoneForms || !this.phoneForms.length) {
      return null;
    }
    if (this.phoneForms.length < i - 1) {
      return null;
    }
    const group = this.phoneForms.at(i) as FormGroup;
    return group.get(propertyName);
  }

  deletePhone(i: number) {
    this.phoneForms.removeAt(i);
  }

  next(): void {
    this.success = true;

    console.log({ model: this.reactiveFormService.getStateOfStore() });

    this.router.navigate(['wizard/step-3-of-3']);
  }

  previous(): void {
    this.router.navigate(['wizard']);
  }
}
