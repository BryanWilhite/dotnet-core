import { Subscription } from 'rxjs/internal/Subscription';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { FormlyFieldConfig } from '@ngx-formly/core';

import { FormsDataService, ReactiveFormModel } from '../state/forms-data.service';

@Component({
  selector: 'app-form1',
  templateUrl: './form1.component.html',
  styleUrls: ['./form1.component.css']
})
export class Form1Component implements OnInit, OnDestroy {
  componentForm = new FormGroup({});

  fields: FormlyFieldConfig[];
  model: Partial<ReactiveFormModel>;
  success = false;

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private reactiveFormService: FormsDataService
  ) { }

  ngOnDestroy(): void {
    for (const sub of this.subscriptions) {
      sub.unsubscribe();
    }
  }

  ngOnInit() {

    const sub = this.reactiveFormService.loadFormlyData().subscribe(() => {
      this.setUpFormly();
    });

    this.subscriptions.push(sub);
  }

  next() {
    this.success = true;

    console.log('next', { model: this.reactiveFormService.getStateOfStore() });

    this.router.navigate(['wizard/step-2-of-3']);
  }

  private setUpFormly() {

    this.fields = this.reactiveFormService.getFormlyConfig('form1');

    const emailField = this.fields.find(field => field.key === 'email');
    if (!emailField) {
      console.error('The expected formly field is not here.');
      return;
    }

    emailField.validators = {
      email: {
        expression: (c: AbstractControl) => {
          const errors = Validators.email(c) as { email: boolean };
          return !errors?.email ?? false;
        },
        message: (error: any, field: FormlyFieldConfig) => `"${field.formControl.value}" is not a valid email address`,
      },
    };

    const state = this.reactiveFormService.getStateOfStore();
    this.model = { email: state?.email };

    const sub2 = this.componentForm.valueChanges.subscribe(change => {
      if (!this.componentForm.valid) {
        return;
      }

      this.reactiveFormService.updateBackingStore(change);
    });

    this.subscriptions.push(sub2);
  }
}
